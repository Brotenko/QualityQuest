using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;
using Fleck;
using ServerLogic.Model;
using ServerLogic.Properties;

namespace ServerLogic.Control
{
    public class MainServerLogic
    {
        private Dictionary<IWebSocketConnection, ModeratorClientManager> _connectedModeratorClients = new();
        private WebSocketServer _server = new("ws://0.0.0.0:" + Settings.Default.MCWebSocketPort);
        private PlayerAudienceClientAPI _playerAudienceClientAPI;
        private const int MaxRepForRandomGeneration = 16;


        /// <summary>
        /// Contains a WebSocket through which messages are exchanged with the ModeratorClient,
        /// as well as methods needed for the general management of this communication.
        /// </summary>
        public MainServerLogic()
        {
            _playerAudienceClientAPI = new PlayerAudienceClientAPI();
        }

        /// <summary>
        /// Starts and initializes the MainServerLogic.
        /// </summary>
        public void Start()
        {
            _playerAudienceClientAPI.StartServer(Settings.Default.PAWebPagePort);
            StartWebsocket();
            ServerLogger.LogDebug($"Website started on {Settings.Default.PAWebPagePort} and WebSocket on {Settings.Default.MCWebSocketPort}");
        }

        /// <summary>
        /// Stops and disposes all connections.
        /// </summary>
        public void Stop()
        {
            foreach (var connection in _connectedModeratorClients)
            {
                connection.Key.Send(JsonConvert.SerializeObject(new SessionClosedMessage(connection.Value.ModeratorGuid)));
                connection.Key.Close();
            }
            _server.Dispose();
            _playerAudienceClientAPI.StopServer();
        }

        /// <summary>
        /// Starts a secure WebSocket. 
        /// </summary>
        private void StartWebsocket()
        {
            //TODO: this certificate is for testing purposes only and should never ever be used in an actual deployment!
            //server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, "thisIsForTestingOnly");
            _server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    ServerLogger.LogDebug("WebSocket-connection to " + socket.ConnectionInfo.ClientIpAddress + " established.\nHeader: " + socket.ConnectionInfo.Headers +
                                          "\nIP: " + socket.ConnectionInfo.ClientIpAddress + "\nSubProtocol: " + socket.ConnectionInfo.NegotiatedSubProtocol);
                };
                socket.OnClose = () =>
                {
                    ServerLogger.LogDebug("Websocket-connection to " + socket.ConnectionInfo.ClientIpAddress + " was closed.");
                    //necessary?
                    socket.Close();
                };
                socket.OnMessage = message =>
                {
                    socket.Send(CheckStringMessage(message, socket));
                };
                socket.OnBinary = bytes =>
                {
                    ServerLogger.LogDebug($"Received Binary message: {Encoding.UTF8.GetString(bytes)}");
                };
                socket.OnError = exception =>
                {
                    ServerLogger.LogError($"WebSocket-connection failed: {exception.Message}");
                    AddStrike(socket);
                };
                socket.OnPing = bytes =>
                    ServerLogger.LogInformation("OnPing");
                socket.OnPong = bytes =>
                    ServerLogger.LogInformation("OnPong");
            });
        }

        /// <summary>
        /// Checks whether the string passed corresponds to the message types specified in the network protocol and converts it accordingly. If necessary, returns a string for the appropriate response. 
        /// </summary>
        /// <param name="message">The received message string.</param>
        /// <param name="socket">The IWebSocketConnection through which the message was received.</param>
        /// <returns>The corresponding response string.</returns>
        private string CheckStringMessage(string message, IWebSocketConnection socket)
        {
            string response = "";
            try
            {
                MessageContainer messageContainer = JsonConvert.DeserializeObject<MessageContainer>(message);
                switch (messageContainer.Type)
                {
                    //  ######## Initialization  ######## 
                    case MessageType.RequestOpenSession:
                        RequestOpenSessionMessage openSessionMessage =
                            JsonConvert.DeserializeObject<RequestOpenSessionMessage>(message);
                        //if (openSessionMessage.Password.Equals(this._password))
                        if(ServerShell.StringToSHA256Hash(openSessionMessage.Password).Equals(Settings.Default.PWHash))
                        {
                            _connectedModeratorClients.Add(socket, new ModeratorClientManager(openSessionMessage.ModeratorID, GenerateSessionKey(MaxRepForRandomGeneration), socket, _playerAudienceClientAPI));
                            response= JsonConvert.SerializeObject(new SessionOpenedMessage(_connectedModeratorClients[socket].ModeratorGuid, _connectedModeratorClients[socket].SessionKey, new Uri($"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}")/*, GenerateQR()*/));
                        }
                        else
                        {
                            //todo: remove before release
                            ServerLogger.LogDebug($"Socket closed due to wrong RequestOpenSession: Password received: {openSessionMessage.Password}.");
                            socket.Close();
                        }
                        ServerLogger.LogDebug($"Received RequestOpenSession. SessionKey is {_connectedModeratorClients[socket].SessionKey}.");
                        break;

                    //Is sent multiple times after MC lost connection to server
                    case MessageType.RequestServerStatus:
                        RequestServerStatusMessage serverStatusMessage =
                            JsonConvert.DeserializeObject<RequestServerStatusMessage>(message);
                        response = (JsonConvert.SerializeObject(
                            new ServerStatusMessage(_connectedModeratorClients[socket].ModeratorGuid)));
                        ServerLogger.LogDebug("Received RequestServerStatus.");
                        break;

                    //To reestablish a lost connection
                    case MessageType.Reconnect:
                        ReconnectMessage reconnectMessage =
                            JsonConvert.DeserializeObject<ReconnectMessage>(message);
                        //searches for the ModeratorID in the previous connections, and replaces the socket in the entry with the current one.
                        foreach (var (key, currentModeratorClientManager) in _connectedModeratorClients)
                        {
                            if (currentModeratorClientManager.ModeratorGuid.Equals(reconnectMessage.ModeratorID))
                            {
                                _connectedModeratorClients.Add(socket, new ModeratorClientManager(currentModeratorClientManager.ModeratorGuid, currentModeratorClientManager.SessionKey, socket, _playerAudienceClientAPI));
                                _connectedModeratorClients.Remove(key);
                                response = (JsonConvert.SerializeObject(
                                    new ReconnectSuccessfulMessage(currentModeratorClientManager.ModeratorGuid)));
                                ServerLogger.LogDebug("Reconnect successful.");
                            }
                            else
                            {
                                ServerLogger.LogDebug($"Invalid reconnection attempt from {socket.ConnectionInfo}.");
                                socket.Close();
                                //TODO?
                            }
                        }
                        break;

                    //Is sent to request the start of the current Online-Session
                    case MessageType.RequestGameStart:
                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                        _connectedModeratorClients[socket].StopAudienceCountLiveUpdate();
                        response = (JsonConvert.SerializeObject(
                            new GameStartedMessage(gameStartMessage.ModeratorID)));
                        ServerLogger.LogDebug("Received RequestGameStart.");
                        break;

                    // ######## Voting ######## 
                    case MessageType.RequestStartVoting:
                        RequestStartVotingMessage startVotingMessage =
                            JsonConvert.DeserializeObject<RequestStartVotingMessage>(message);
                        Dictionary<KeyValuePair<Guid, string>, int> tempVotingPrompts = new();
                        foreach (var votingOption in startVotingMessage.VotingOptions)
                        {
                            tempVotingPrompts.Add(votingOption,0);
                        }
                        //connectedModeratorClients[socket].statistics.Add(startVotingMessage.VotingPrompt, tempVotingPrompts);
                        _connectedModeratorClients[socket].StartVotingTimer(startVotingMessage);
                        socket.Send(JsonConvert.SerializeObject(
                            new VotingStartedMessage(startVotingMessage.ModeratorID)));
                        //votingEndedMessage is sended via Timer inside ModeratorClientManager-class
                        ServerLogger.LogDebug("Received RequestStartVoting.");
                        break;

                    // ######## Control messages ########
                    case MessageType.RequestGamePausedStatusChange:
                        RequestGamePausedStatusChangeMessage gamePausedStatusChange =
                            JsonConvert.DeserializeObject<RequestGamePausedStatusChangeMessage>(message);
                        _connectedModeratorClients[socket].PauseVotingTimer(gamePausedStatusChange.GamePaused);
                        response = (JsonConvert.SerializeObject(
                            new GamePausedStatusMessage(gamePausedStatusChange.ModeratorID,
                                gamePausedStatusChange.GamePaused)));
                        break;

                    // ######## Postgame ########
                    case MessageType.RequestCloseSession:
                        RequestCloseSessionMessage closeSessionMessage =
                            JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                        response=(
                            JsonConvert.SerializeObject(new SessionClosedMessage(closeSessionMessage.ModeratorID)));
                        _connectedModeratorClients[socket].Stop();
                        _connectedModeratorClients.Remove(socket);
                        ServerLogger.LogDebug("Received RequestCloseSession.");
                        break;

                    // unknown MessageType
                    default:
                        //FR57 'ServerLogic persistence': "The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data."
                        ServerLogger.LogDebug($"Corrupted Messagetype: {typeof(MessageType)}, received from {_connectedModeratorClients[socket].ModeratorGuid}, {socket.ConnectionInfo} within session {_connectedModeratorClients[socket].SessionKey}.");
                        //FR31 'Network protocol violation'
                        AddStrike(socket);
                        return "";
                }

                //reset Strikes to 0, as the connection is only closed after three violations in a row
                if (_connectedModeratorClients.TryGetValue(socket, out ModeratorClientManager currentModeratorClient))
                {
                    currentModeratorClient.Strikes = 0;
                }
            }
            catch (JsonSerializationException jsonSerializationException)
            {
                ServerLogger.LogDebug($"Exception occurred on json-serialization: {jsonSerializationException}.");
            }
            catch (Exception exception)
            {
                ServerLogger.LogWarning($"Unexpected Exception occurred: {exception}.");
            }

            return response;
        }

        /// <summary>
        /// Increases and checks the number of network protocol violations of the passed IWebSocketConnection.
        /// </summary>
        /// <param name="socket"></param>
        private void AddStrike(IWebSocketConnection socket)
        {
            //FR31 'Network protocol violation'
            if (_connectedModeratorClients.TryGetValue(socket, out ModeratorClientManager currentModeratorClient))
            {
                currentModeratorClient.Strikes += 1;
                if (currentModeratorClient.Strikes >= 3)
                {
                    currentModeratorClient.Stop();
                    _connectedModeratorClients.Remove(socket);
                }
            }
        }

        /// <summary>
        /// Generates a random session key and compares it with already recorded sessions and recreates it if necessary.
        /// If no unique SessionKey can be created after several attempts, it is aborted and a SessionKey is returned without a new check,
        /// even at the risk that it is already in use.
        /// </summary>
        /// <param name="maxRecursionCycles">The maximum number of recursions allowed to generate a random unique sessionKey.</param>
        /// <returns>A SessionKey.</returns>
        private string GenerateSessionKey(int maxRecursionCycles)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            string sessionKey = new string(Enumerable.Repeat(chars, 6).Select(s => s[rand.Next(s.Length)]).ToArray());

            //Termination condition, takes effect if, after several runs, no session key can be generated which is not already in use.
            if (maxRecursionCycles == 0)
            {
                ServerLogger.LogWarning($"Couldn't generate unique Session-Key. Session-Key {sessionKey} might be duplicate.");
                return sessionKey;
            }
            //SessionKey already in use?
            foreach (var (key, value) in _connectedModeratorClients)
            {
                if (value.SessionKey.Equals(sessionKey))
                {
                    sessionKey = GenerateSessionKey(maxRecursionCycles - 1);
                }
            }
            return sessionKey;
        }
    }
}
