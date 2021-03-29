using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;
using Fleck;
using ServerLogic.Model;
using ServerLogic.Properties;

namespace ServerLogic.Control
{
    public class MainServerLogic
    {
        //TODO: better name?
        private Dictionary<IWebSocketConnection, ModeratorClientManager> connectedModeratorClients = new();
        private WebSocketServer server = new("ws://0.0.0.0:" + Settings.Default.MCWebSocketPort);
        private PlayerAudienceClientAPI playerAudienceClientAPI;
        private string _password; 
        private const int maxRepForRandomGeneration = 16;

        //TODO move settings to PAClient, same for WebSocket port and Homepage port
        //TODO maybe refactor/rename/both

        //TODO implement AudienceStatusMessage

        public void SetPassword(string password)
        {
            this._password = password;
        }
        

        public MainServerLogic()
        {
            this.playerAudienceClientAPI = new PlayerAudienceClientAPI();
        }

        public void Start(int port)
        {
            playerAudienceClientAPI.StartServer(port);
            StartWebsocket();
        }

        public void Stop()
        {
            foreach (var connection in connectedModeratorClients)
            {
                connection.Key.Send(JsonConvert.SerializeObject(new SessionClosedMessage(connection.Value.moderatorGuid,connection.Value.statistics)));
                connection.Key.Close();
            }
            //todo: necessary?
            //server.Dispose();
            playerAudienceClientAPI.StopServer();
        }
        private void StartWebsocket()
        {
            //TODO: this certificate is for testing purposes only and should never ever be used in an actual deployment!
            //server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, "thisIsForTestingOnly");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    ServerLogger.LogDebug("WebSocket-connection to " + socket.ConnectionInfo.ClientIpAddress + " established.\nHeader: " + socket.ConnectionInfo.Headers+
                                          "\nIP: " + socket.ConnectionInfo.ClientIpAddress+"\nSubProtocol: " + socket.ConnectionInfo.NegotiatedSubProtocol);
                };
                socket.OnClose = () =>
                {
                    ServerLogger.LogDebug("Websocket-connection to "+socket.ConnectionInfo.ClientIpAddress+" was closed.");
                };
                socket.OnMessage = message =>
                {
                    socket.Send(CheckStringMessage(message, socket));
                };
                socket.OnBinary = bytes =>
                {
                    ServerLogger.LogDebug($"OnBinary {Encoding.UTF8.GetString(bytes)}");
                };
                socket.OnError = exception =>
                   ServerLogger.LogInformation($"OnError {exception.Message}");
                socket.OnPing = bytes =>
                    ServerLogger.LogInformation("OnPing");
                socket.OnPong = bytes =>
                    ServerLogger.LogInformation("OnPong");
            });
        }

        private string CheckStringMessage(string message, IWebSocketConnection socket)
        {
            try
            {
                MessageContainer messageContainer = 
                    JsonConvert.DeserializeObject<MessageContainer>(message);
                switch (messageContainer.Type)
                {
                    //  ######## Initialization  ######## 
                    case MessageType.RequestOpenSession:
                        RequestOpenSessionMessage openSessionMessage =
                            JsonConvert.DeserializeObject<RequestOpenSessionMessage>(message);
                        if (openSessionMessage.Password.Equals(this._password))
                        {
                            connectedModeratorClients.Add(socket, new ModeratorClientManager(openSessionMessage.ModeratorID, GenerateSessionKey(maxRepForRandomGeneration), socket));
                            playerAudienceClientAPI.StartNewSession(connectedModeratorClients[socket].sessionkey);
                            connectedModeratorClients[socket].StartAudienceCountLiveUpdate();
                            return JsonConvert.SerializeObject(new SessionOpenedMessage(connectedModeratorClients[socket].moderatorGuid, connectedModeratorClients[socket].sessionkey, new Uri($"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}"), GenerateQR()));
                        }
                        else
                        {
                            socket.Close();
                        }
                        break;
                    //Is sent multiple times after MC lost connection to server
                    case MessageType.RequestServerStatus:
                        RequestServerStatusMessage serverStatusMessage =
                            JsonConvert.DeserializeObject<RequestServerStatusMessage>(message);
                        return (JsonConvert.SerializeObject(new ServerStatusMessage(connectedModeratorClients[socket].moderatorGuid)));

                    //To reestablish a lost connection
                    case MessageType.Reconnect:
                        ReconnectMessage reconnectMessage = 
                            JsonConvert.DeserializeObject<ReconnectMessage>(message);
                        //searches for the ModeratorID in the previous connections, and replaces the socket in the entry with the current one.
                        foreach (var (key, value) in connectedModeratorClients)
                        {
                            if (value.moderatorGuid.Equals(reconnectMessage.ModeratorID))
                            {
                                connectedModeratorClients.Add(socket, new ModeratorClientManager(value.moderatorGuid, value.sessionkey, socket));
                                connectedModeratorClients.Remove(key);
                                return (JsonConvert.SerializeObject(new ReconnectSuccessfulMessage(value.moderatorGuid)));
                            }
                        }
                        break;

                    //Is sent to request the start of the current Online-Session
                    case MessageType.RequestGameStart:
                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                        connectedModeratorClients[socket].PlayerAudienceCountLiveTimer.Stop();
                        return (JsonConvert.SerializeObject(new GameStartedMessage(gameStartMessage.ModeratorID)));
 
                    // ######## Voting ######## 
                    case MessageType.RequestStartVoting:
                        RequestStartVotingMessage startVotingMessage =
                            JsonConvert.DeserializeObject<RequestStartVotingMessage>(message);
                        //TODO start voting process (needs PA-Client)
                        return (JsonConvert.SerializeObject(new VotingStartedMessage(startVotingMessage.ModeratorID)));
                        //wait for voting to end, maybe use await/task or similar
                        //await socket.Send(JsonConvert.SerializeObject(new VotingEndedMessage(,,)));

                    // ######## Control messages ########
                    case MessageType.RequestGamePausedStatusChange:
                        RequestGamePausedStatusChangeMessage gamePausedStatusChange =
                            JsonConvert.DeserializeObject<RequestGamePausedStatusChangeMessage>(message);
                        //TODO: pause game if true, continue if false
                        return (JsonConvert.SerializeObject(
                            new GamePausedStatusMessage(gamePausedStatusChange.ModeratorID,
                                gamePausedStatusChange.GamePaused)));

                    // ######## Postgame ########
                    case MessageType.RequestCloseSession:
                        RequestCloseSessionMessage closeSessionMessage =
                            JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                        //todo: send the statistics back (needs PA-Client)
                        //return(
                        //    JsonConvert.SerializeObject(new SessionClosedMessage(closeSessionMessage.ModeratorID,)));
                        //connectedModeratorClients[socket].Stop();
                        //connectedModeratorClients.Remove(socket);
                        break;

                    // unknown Messagetype
                    default:
                        //FR57 'ServerLogic persistence': "The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data."
                        ServerLogger.LogDebug($"Corrupted Messagetype: {typeof(MessageType)}, received from {connectedModeratorClients[socket].moderatorGuid}, {socket.ConnectionInfo} within session {connectedModeratorClients[socket].sessionkey}.");
                        //FR31 'Network protocol violation'
                        connectedModeratorClients[socket].strikes += 1;
                        if (connectedModeratorClients[socket].strikes >= 3)
                        {
                            connectedModeratorClients[socket].Stop();
                            connectedModeratorClients.Remove(socket);
                        }
                        break;

                }
            }
            catch (JsonSerializationException jsonSerializationException)
            {
                ServerLogger.LogDebug($"Exception occured on json-serialization: {jsonSerializationException}.");
            }
            catch (Exception exception)
            {
                ServerLogger.LogWarning($"Unexpected Exception occured: {exception}.");
            }

            return "";
        }




        /// <summary>
        /// Generates a random session key and compares it with already recorded sessions and recreates it if necessary.
        /// If no unique SessionKey can be created after several attempts, it is aborted and a SessionKey is returned without a new check,
        /// even at the risk that it is already in use.
        /// </summary>
        /// <param name="maxRecursionCycles">The maximum number of recursions allowed to generate a random unique sessionKey.</param>
        /// <returns>A sessionkey.</returns>
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
            //sessionkey already in use?
            foreach (var(key, value) in connectedModeratorClients)
            {
                if (value.sessionkey.Equals(sessionKey))
                {
                    sessionKey = GenerateSessionKey(maxRecursionCycles-1);
                }
            }
            return sessionKey;
        }

        private Bitmap GenerateQR()
        {
        BarcodeGenerator generator = new(EncodeTypes.QR, $"{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}");
            generator.Parameters.Resolution = 800;
            return generator.GenerateBarCodeImage();
        }
    }
}
