using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;
using Fleck;
using ServerLogic.Model;
using ServerLogic.Properties;


namespace ServerLogic.Control
{
    public class MainServerLogic
    {
        public string ActiveConnections;

        private WebSocketServer _server;

        private readonly Dictionary<Guid, ModeratorClientManager> _connectedModeratorClients;
        private readonly Timer _timerForDataDeletion;
        private readonly PlayerAudienceClientAPI _playerAudienceClientApi;
        private const int MaxRepForRandomGeneration = 16;

        //TODO Remove default password from settings
        //TODO Disable Fleck-Logger

        /// <summary>
        /// Contains a WebSocket through which messages are exchanged with the ModeratorClient,
        /// as well as methods needed for the general management of this communication.
        /// </summary>
        public MainServerLogic()
        {
            _playerAudienceClientApi = new PlayerAudienceClientAPI();
            _connectedModeratorClients = new Dictionary<Guid, ModeratorClientManager>();
            //30sec interval
            //_timerForDataDeletion = new Timer(30000); todo
            _timerForDataDeletion = new Timer(30000);

            _timerForDataDeletion.Elapsed += CheckForInactivity;
            _timerForDataDeletion.AutoReset = true;
            _timerForDataDeletion.Enabled = true;
        }

        /// <summary>
        /// Starts and initializes the MainServerLogic.
        /// </summary>
        public void Start()
        {
            //TODO set url by using installer skript & settings file
            _server = new WebSocketServer("ws://0.0.0.0:" + Settings.Default.MCWebSocketPort);
            ServerLogger.LogDebug($"WebSocket secure connection established: {_server.IsSecure}.");
            _playerAudienceClientApi.StartServer(Settings.Default.PAWebPagePort);
            StartWebsocket();
            ServerLogger.LogDebug($"Website started on {Settings.Default.PAWebPagePort} and WebSocket on {Settings.Default.MCWebSocketPort}");
            _timerForDataDeletion.Start();

        }

        /// <summary>
        /// Stops and disposes all connections.
        /// </summary>
        public void Stop()
        {
            foreach (var (_, value) in _connectedModeratorClients)
            {
                value.SocketConnection.Send(JsonConvert.SerializeObject(new SessionClosedMessage(value.ModeratorGuid)));
                value.SocketConnection.Close();
            }
            _timerForDataDeletion.Stop();
            _server.Dispose();
            _playerAudienceClientApi.StopServer();
        }

        /// <summary>
        /// Is periodically triggered by the _timerForDataDeletion. Checks if one of the connected ModeratorClients has been marked as inactive and deletes them from _connectedModeratorClients if necessary. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void CheckForInactivity(object source, ElapsedEventArgs e)
        {
            if (_connectedModeratorClients.Count>0)
            {
                string tempLog = "";
                foreach (var (socket, moderatorClientManager) in _connectedModeratorClients)
                {
                    if (moderatorClientManager.IsInactive) _connectedModeratorClients.Remove(socket);
                    else
                        tempLog +=
                            $"\tMC-{moderatorClientManager.ModeratorGuid} in Session {moderatorClientManager.SessionKey}.\n";
                }

                ActiveConnections = tempLog;
            }
        }

        /// <summary>
        /// Starts a secure WebSocket. 
        /// </summary>
        internal void StartWebsocket()
        {
            //_server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, "thisIsForTestingOnly");
            _server.Start(socketConnection =>
            {
                socketConnection.OnOpen = () =>
                {
                    ServerLogger.LogDebug("WebSocket-connection to " + socketConnection.ConnectionInfo.ClientIpAddress + " established.\nHeader: " + socketConnection.ConnectionInfo.Headers +
                                          "\nIP: " + socketConnection.ConnectionInfo.ClientIpAddress + "\nSubProtocol: " + socketConnection.ConnectionInfo.NegotiatedSubProtocol);
                };
                socketConnection.OnClose = () =>
                {
                    foreach (var (_, moderatorClientManager) in _connectedModeratorClients)
                    {
                        if (socketConnection.Equals(moderatorClientManager.SocketConnection))
                        {
                            //Disposes all timers, except inactivity-timer, and closes the socket. 
                            moderatorClientManager.Stop();
                            ServerLogger.LogDebug("Websocket-connection to " + socketConnection.ConnectionInfo.ClientIpAddress + " was closed. Communication attempts are temporarily paused until a moderator-client reconnect attempt or until the server-side session-relevant data is deleted after 30 minutes of inactivity.");
                        }
                    }
                    //socketConnection.Close();
                };
                socketConnection.OnMessage = message =>
                {
                    try
                    {
                        string response = "";
                        MessageContainer messageContainer = JsonConvert.DeserializeObject<MessageContainer>(message);
                        //ModGuid previously unknown
                        if (!_connectedModeratorClients.ContainsKey(messageContainer.ModeratorID))
                        {
                            //Wants to open session
                            if (messageContainer.Type == MessageType.RequestOpenSession)
                            {
                                _connectedModeratorClients.Add(messageContainer.ModeratorID, new ModeratorClientManager(
                                    messageContainer.ModeratorID, 
                                    socketConnection,
                                    _playerAudienceClientApi));
                                response = CheckStringMessage(message);
                            }
                            // unknown guid
                            else
                            {
                                response= JsonConvert.SerializeObject(
                                    new ErrorMessage(messageContainer.ModeratorID, ErrorType.UnknownGuid, ""));
                            }
                        }

                        //socket already exists and fits the sent modID
                        else if (_connectedModeratorClients[messageContainer.ModeratorID].SocketConnection
                            .Equals(socketConnection))
                        {
                            response = CheckStringMessage(message);
                        }
                        //modId exists, but with different socket
                        else
                        {
                            //Mismatch caused by connection loss and following reconnect -> Guid already exists, but socketconnection has changed
                            if (messageContainer.Type == MessageType.Reconnect)
                            {
                                _connectedModeratorClients[messageContainer.ModeratorID].SocketConnection = socketConnection;
                                _connectedModeratorClients[messageContainer.ModeratorID].ResetInactivity();
                                response = JsonConvert.SerializeObject(new ReconnectSuccessfulMessage(
                                       messageContainer.ModeratorID));
                                //todo? On the server side, it is difficult to recognize when a disconnect has occurred in the game. After GameStart, recovery is possible without any problems, but if the connection is interrupted after OpenSession and before GameStart, no AudienceLiveUpdates are sent even if reconnect is successful, i.e. the server behaves as if a RequestGameStart message had come together with the reconnect. This is not problematic in itself, as long as one can do without the AudienceLiveUpdates in this case.
                                ServerLogger.LogDebug("Reconnect successful.");
                            }
                            //Mismatch, but not a reconnect message;
                            else
                            {
                                //TODO
                            }
                        }

                        socketConnection.Send(response);
                    }
                    catch (Exception e)
                    {
                        ServerLogger.LogDebug("OnOpenException:" + e);
                    }

                };
                socketConnection.OnError = exception =>
                {
                    ServerLogger.LogError($"WebSocket-connection failed: {exception.Message}");
                    //AddStrike(socketConnection);
                };
            });
        }

        /// <summary>
        /// Checks whether the string passed corresponds to the message types specified in the network protocol and converts it accordingly. If necessary, returns a string for the appropriate response. 
        /// </summary>
        /// <param name="message">The received message string.</param>
        /// <param name="socket">The IWebSocketConnection through which the message was received.</param>
        /// <returns>The corresponding response string.</returns>
        internal string CheckStringMessage(string message)
        {
            string response = "";
            try //todo remove try-catch, as this is already ensured in OnMessage
            {
                MessageContainer messageContainer = JsonConvert.DeserializeObject<MessageContainer>(message);
                Guid mcId = messageContainer.ModeratorID;
                switch (messageContainer.Type)
                {
                    //todo try eliminating the usage of the socket-attribute, to enable unit testing this method
                    //  ######## Initialization  ######## 
                    case MessageType.RequestOpenSession:
                        RequestOpenSessionMessage openSessionMessage =
                            JsonConvert.DeserializeObject<RequestOpenSessionMessage>(message);
                        if (ServerShell.StringToSHA256Hash(openSessionMessage.Password).Equals(Settings.Default.PWHash))
                        {
                            if (_connectedModeratorClients[mcId].SessionKey.Equals(""))
                            {
                                _connectedModeratorClients[mcId].InitSession(GenerateSessionKey(MaxRepForRandomGeneration));
                                response = JsonConvert.SerializeObject(new SessionOpenedMessage(
                                    _connectedModeratorClients[mcId].ModeratorGuid,
                                    _connectedModeratorClients[mcId].SessionKey,
                                    new Uri($"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}")));
                                ServerLogger.LogDebug($"Received RequestOpenSession. SessionKey is {_connectedModeratorClients[mcId].SessionKey}.");

                            }
                            else
                            {
                                response = JsonConvert.SerializeObject(new ErrorMessage(
                                    openSessionMessage.ModeratorID,
                                    ErrorType.SessionDoesNotExist,
                                    "Session with this ModeratorGuid already exists."));
                                ServerLogger.LogDebug($"ModeratorClient {openSessionMessage.ModeratorID} tried to open another session.");
                            }
                        }
                        //wrong password
                        else
                        {
                            //todo: remove before release
                            ServerLogger.LogDebug($"SocketConnection closed due to wrong RequestOpenSession: Password received: {openSessionMessage.Password}.");

                            //todo remove entry from list!
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                mcId, ErrorType.WrongPassword, ""));
                            _connectedModeratorClients[mcId].SocketConnection.Close();
                            _connectedModeratorClients.Remove(mcId);

                        }
                        break;

                    //Is sent multiple times after MC lost connection to server
                    case MessageType.RequestServerStatus:

                        response = JsonConvert.SerializeObject(new ServerStatusMessage(
                            _connectedModeratorClients[mcId].ModeratorGuid));
                        ServerLogger.LogDebug("Received RequestServerStatus.");

                        break;

                    //Is sent to request the start of the current Online-Session
                    case MessageType.RequestGameStart:

                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                        _connectedModeratorClients[mcId].StopAudienceCountLiveUpdate();
                        response = JsonConvert.SerializeObject(new GameStartedMessage(
                            gameStartMessage.ModeratorID));
                        ServerLogger.LogDebug("Received RequestGameStart.");

                        break;

                    // ######## Voting ######## 
                    case MessageType.RequestStartVoting:

                        RequestStartVotingMessage startVotingMessage =
                            JsonConvert.DeserializeObject<RequestStartVotingMessage>(message);
                        if (!_connectedModeratorClients[mcId].IsVoting)
                        {
                            _connectedModeratorClients[mcId].StartVotingTimer(startVotingMessage);
                            //votingOptions get extracted inside ModeratorClientManager
                            response = JsonConvert.SerializeObject(new VotingStartedMessage(
                                startVotingMessage.ModeratorID));
                            ServerLogger.LogDebug("Received RequestStartVoting.");
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                messageContainer.ModeratorID,
                                ErrorType.IllegalMessage,
                                "Message out of order: Voting still active."));
                            AddStrike(mcId);
                            ServerLogger.LogDebug("Received RequestStartVoting before current voting was finished.");
                        }

                        break;

                    // ######## Control messages ########
                    case MessageType.RequestGamePausedStatusChange:

                        RequestGamePausedStatusChangeMessage gamePausedStatusChange =
                            JsonConvert.DeserializeObject<RequestGamePausedStatusChangeMessage>(message);
                        if (!gamePausedStatusChange.GamePaused.Equals(_connectedModeratorClients[mcId].IsPaused))
                        {
                            if (_connectedModeratorClients[mcId]
                                .PauseVotingTimer(gamePausedStatusChange.GamePaused))
                            {
                                response = JsonConvert.SerializeObject(new GamePausedStatusMessage(
                                    gamePausedStatusChange.ModeratorID,
                                    gamePausedStatusChange.GamePaused));
                            }
                            else
                            {
                                response = JsonConvert.SerializeObject(new ErrorMessage(
                                    gamePausedStatusChange.ModeratorID,
                                    ErrorType.IllegalPauseAction,
                                    ""));
                            }
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                gamePausedStatusChange.ModeratorID,
                                ErrorType.IllegalPauseAction,
                                ""));
                        }

                        break;

                    // ######## Post-game ########
                    case MessageType.RequestCloseSession:

                        RequestCloseSessionMessage closeSessionMessage =
                            JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                        //if (SessionKeyExists(closeSessionMessage.SessionKey))
                        if(_connectedModeratorClients[mcId].SessionKey.Equals(closeSessionMessage.SessionKey))
                        {
                            response = JsonConvert.SerializeObject(
                                    new SessionClosedMessage(closeSessionMessage.ModeratorID));
                            _connectedModeratorClients[mcId].Stop();
                            _connectedModeratorClients.Remove(mcId);
                            ServerLogger.LogDebug(
                                $"Session {closeSessionMessage.SessionKey} was closed, {_connectedModeratorClients[mcId].SocketConnection.ConnectionInfo} has disconnected.");
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(
                                new ErrorMessage(closeSessionMessage.ModeratorID, ErrorType.SessionDoesNotExist, ""));
                            ServerLogger.LogDebug($"MC-{closeSessionMessage.SessionKey} tried to close Session but failed to due wrong sessionkey. \n\tTransmitted Sessionkey: \t{closeSessionMessage.SessionKey}\n\tActual Sessionkey: \t{_connectedModeratorClients[mcId].SessionKey}");
                        }

                        break;

                    // unknown MessageType
                    default:
                        //FR57 'ServerLogic persistence': "The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data."
                        ServerLogger.LogDebug($"Corrupted MessageType: {typeof(MessageType)}, received from {_connectedModeratorClients[mcId].ModeratorGuid}, {_connectedModeratorClients[mcId].SocketConnection.ConnectionInfo} within session {_connectedModeratorClients[mcId].SessionKey}.");
                        //FR31 'Network protocol violation'
                        response = JsonConvert.SerializeObject(new ErrorMessage(
                            messageContainer.ModeratorID,
                            ErrorType.IllegalMessage,
                            "Message out of order or messageType unknown."));
                        AddStrike(mcId);
                        break;
                }
                _connectedModeratorClients[mcId].ResetInactivity();
                //reset Strikes to 0, as the connection is only closed after three violations in a row
                if (_connectedModeratorClients.TryGetValue(mcId, out ModeratorClientManager currentModeratorClient))
                {
                    //TODO needs to placed somewhere else, as this resets the strikes also in the default case
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
        internal void AddStrike(Guid moderatorId)
        {
            //FR31 'Network protocol violation'
            _connectedModeratorClients[moderatorId].Strikes += 1;
            if (_connectedModeratorClients[moderatorId].Strikes >= 3)
            {
                _connectedModeratorClients[moderatorId].Stop();
                _connectedModeratorClients.Remove(moderatorId);
            }
        }

        /// <summary>
        /// Generates a random session key and compares it with already recorded sessions and recreates it if necessary.
        /// If no unique SessionKey can be created after several attempts, it is aborted and a SessionKey is returned without a new check,
        /// even at the risk that it is already in use.
        /// </summary>
        /// <param name="maxRecursionCycles">The maximum number of recursions allowed to generate a random unique sessionKey.</param>
        /// <returns>A SessionKey.</returns>
        internal string GenerateSessionKey(int maxRecursionCycles)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            string sessionKey = new(Enumerable.Repeat(chars, 6).Select(s => s[rand.Next(s.Length)]).ToArray());

            //Termination condition, takes effect if, after several runs, no session key can be generated which is not already in use.
            if (maxRecursionCycles == 0)
            {
                ServerLogger.LogWarning($"Couldn't generate unique Session-Key. Session-Key {sessionKey} might be duplicate.");
                return sessionKey;
            }

            //SessionKey already in use?
            foreach (var (_, value) in _connectedModeratorClients)
            {
                if (value.SessionKey.Equals(sessionKey)) GenerateSessionKey(maxRecursionCycles - 1);
            }

            return sessionKey;
        }
    }
}
