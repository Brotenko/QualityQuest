﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
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
        
        internal PlayerAudienceClientAPI _playerAudienceClientApi;
        internal readonly Dictionary<Guid, ModeratorClientManager> _connectedModeratorClients;
        
        private WebSocketServer _server;
        private const int MaxRepForRandomGeneration = 16;
        private readonly Timer _checkForInactiveSessionsTimer;

        /// <summary>
        /// Contains a WebSocket through which messages are exchanged with the ModeratorClient,
        /// as well as methods needed for the general management of this communication.
        /// </summary>
        public MainServerLogic()
        {
            _playerAudienceClientApi = new PlayerAudienceClientAPI();
            _connectedModeratorClients = new Dictionary<Guid, ModeratorClientManager>();
            //3sec interval
            _checkForInactiveSessionsTimer = new Timer(3000);
            _checkForInactiveSessionsTimer.Elapsed += CheckForSessionInactivity;
            _checkForInactiveSessionsTimer.AutoReset = true;
            _checkForInactiveSessionsTimer.Enabled = true;
        }

        /// <summary>
        /// Starts and initializes the MainServerLogic.
        /// </summary>
        public void Start()
        {
            FleckLog.Level = LogLevel.Warn;
            _server = new WebSocketServer($"wss:{Settings.Default.DockerUrl}:80");
            _server.EnabledSslProtocols = SslProtocols.Tls12;
            try
            {
                _server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, Settings.Default.CertPW);
                _playerAudienceClientApi.StartServer(443);
                StartWebsocket();
                _checkForInactiveSessionsTimer.Start();
                ServerLogger.LogInformation($"Website started on {Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort} and WebSocket on {Settings.Default.MCWebSocketPort}");
                ServerLogger.LogInformation($"Using wss: {_server.IsSecure}.");
            }
            catch (Exception)
            {
                ServerLogger.LogError("Could not load certificate, please restart and configurate the docker-container and ensure that name and password of the certificate are correct.");
                Environment.Exit(exitCode: -1);
            }
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
            _checkForInactiveSessionsTimer?.Stop();
            _server?.Dispose();
            _playerAudienceClientApi.StopServer();
        }

        /// <summary>
        /// Is periodically triggered by the _checkForInactiveSessionsTimer. Checks if one of the connected ModeratorClients has been marked as inactive and deletes them from _connectedModeratorClients if necessary.
        /// Also sets Global string 'ActiveConnections' which contains all active Sessions. 
        /// </summary>
        /// <param name="source">Parameter used by Timer-Elapsed-Event.</param>
        /// <param name="eventArgs">Parameter used by Timer-Elapsed-Event.</param>
        private void CheckForSessionInactivity(object source, ElapsedEventArgs eventArgs)
        {
            if (_connectedModeratorClients.Count>0)
            {
                string tempLog = "";
                foreach (var (socket, moderatorClientManager) in _connectedModeratorClients)
                {
                    if (moderatorClientManager.IsInactive)
                    {
                        _connectedModeratorClients.Remove(socket);
                    }
                    else
                    {
                        tempLog += $"\tMC-{moderatorClientManager.ModeratorGuid} in Session {moderatorClientManager.SessionKey}.\n";
                    }
                }
                ActiveConnections = tempLog;
            }
        }

        /// <summary>
        /// Starts a secure WebSocket. 
        /// </summary>
        internal void StartWebsocket()
        {
            _server.Start(socketConnection =>
            {
                socketConnection.OnOpen = () =>
                {
                    ServerLogger.LogDebug($"WebSocket-connection to " + socketConnection.ConnectionInfo.ClientIpAddress + " established.");
                };
                socketConnection.OnClose = () =>
                {
                    foreach (var (_, moderatorClientManager) in _connectedModeratorClients)
                    {
                        if (socketConnection.Equals(moderatorClientManager.SocketConnection))
                        {
                            //Disposes all timers, except inactivity-timer, and closes the socket. 
                            moderatorClientManager.Stop(false);
                            moderatorClientManager.IsVoting = false;
                            moderatorClientManager.IsPaused = false;
                            ServerLogger.LogDebug("Websocket-connection to " + socketConnection.ConnectionInfo.ClientIpAddress + " was closed by Moderator-Client.");
                        }
                    }
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
                        else if (_connectedModeratorClients[messageContainer.ModeratorID].SocketConnection.Equals(socketConnection))
                        {
                            response = CheckStringMessage(message);
                        }
                        //modId exists, but with different socket
                        else
                        {
                            //Mismatch caused by connection loss and following reconnect -> Guid already exists, but socketConnection has changed
                            if (messageContainer.Type == MessageType.Reconnect)
                            {
                                _connectedModeratorClients[messageContainer.ModeratorID].SocketConnection = socketConnection;
                                _connectedModeratorClients[messageContainer.ModeratorID].ResetInactivity();
                                response = JsonConvert.SerializeObject(new ReconnectSuccessfulMessage(
                                       messageContainer.ModeratorID));
                                ServerLogger.LogDebug("Reconnect successful.");
                            }
                            //Mismatch, but not a reconnect message;
                            else
                            {
                                response = JsonConvert.SerializeObject(new ErrorMessage(messageContainer.ModeratorID,
                                    ErrorType.GuidAlreadyExists, ""));
                                ServerLogger.LogDebug("MC tried to connect with an already existing Guid.");
                            }
                        }

                        //In case of this two MessageTypes, Connection is closed after sending the response
                        if (JsonConvert.DeserializeObject<MessageContainer>(response).Type.Equals(MessageType.RequestCloseSession) ||
                            (JsonConvert.DeserializeObject<MessageContainer>(response).Type.Equals(MessageType.Error) &&
                            JsonConvert.DeserializeObject<ErrorMessage>(response).ErrorMessageType.Equals(ErrorType.WrongPassword)))
                        {
                            ServerLogger.LogDebug("Connection cancel: \n"+response);
                            socketConnection.Send(response);
                            _connectedModeratorClients[messageContainer.ModeratorID].Stop(true);
                            _connectedModeratorClients[messageContainer.ModeratorID].StopInactivityTimer();
                            _connectedModeratorClients.Remove(messageContainer.ModeratorID);
                        }
                        else
                        {
                            socketConnection.Send(response);
                        }
                    }
                    catch (Exception e)
                    {
                        ServerLogger.LogDebug("OnOpenException:" + e);
                    }

                };
                socketConnection.OnError = exception =>
                {
                    ServerLogger.LogError($"WebSocket-connection failed: {exception.Message}");
                };
            });
        }

        /// <summary>
        /// Checks whether the string passed corresponds to the message types specified in the network protocol and converts it accordingly. Returns a formatted message-string for the response.
        /// </summary>
        /// <param name="message">The received message string.</param>
        /// <returns>The corresponding response string.</returns>
        internal string CheckStringMessage(string message)
        {
            string response = "";
            try
            {
                MessageContainer messageContainer = JsonConvert.DeserializeObject<MessageContainer>(message);
                Guid mcId = messageContainer.ModeratorID;
                switch (messageContainer.Type)
                {
                    // ######## Initialization  ######## 
                    case MessageType.RequestOpenSession:
                        RequestOpenSessionMessage openSessionMessage =
                            JsonConvert.DeserializeObject<RequestOpenSessionMessage>(message);
                        if (ServerShell.StringToSHA256Hash(openSessionMessage.Password).Equals(Settings.Default.PWHash))
                        {
                            if (_connectedModeratorClients[mcId].SessionKey.Equals(""))
                            {
                                _connectedModeratorClients[mcId]
                                    .InitSession(GenerateSessionKey(MaxRepForRandomGeneration));
                                response = JsonConvert.SerializeObject(new SessionOpenedMessage(
                                    _connectedModeratorClients[mcId].ModeratorGuid,
                                    _connectedModeratorClients[mcId].SessionKey,
                                    new Uri(
                                        $"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}/?key={_connectedModeratorClients[mcId].SessionKey}")));
                                ServerLogger.LogDebug(
                                    $"Received RequestOpenSession. Opened Session {_connectedModeratorClients[mcId].SessionKey} for MC-{mcId}.");
                                _connectedModeratorClients[mcId].Strikes = 0;
                            }
                            else
                            {
                                response = JsonConvert.SerializeObject(new ErrorMessage(
                                    openSessionMessage.ModeratorID,
                                    ErrorType.WrongSession,
                                    "Session with this ModeratorGuid already exists."));
                                ServerLogger.LogDebug(
                                    $"ModeratorClient {openSessionMessage.ModeratorID} tried to open another session.");
                                AddStrike(mcId);
                            }
                        }
                        //wrong password
                        else
                        {
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                mcId, ErrorType.WrongPassword, ""));
                        }

                        break;

                    // ######## Start to play ########
                    case MessageType.RequestGameStart:

                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                        _connectedModeratorClients[mcId].StopAudienceCountLiveUpdate();
                        response = JsonConvert.SerializeObject(new GameStartedMessage(
                            gameStartMessage.ModeratorID));
                        ServerLogger.LogDebug("Received RequestGameStart.");
                        _connectedModeratorClients[mcId].Strikes = 0;
                        break;

                    // ######## Start Voting ######## 
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
                            _connectedModeratorClients[mcId].Strikes = 0;
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                messageContainer.ModeratorID,
                                ErrorType.IllegalMessage,
                                "Message out of order: Voting still active."));
                            ServerLogger.LogDebug("Received RequestStartVoting before current voting was finished.");
                            AddStrike(mcId);
                        }

                        break;

                    // ######## Pause/Unpause Voting ########
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
                                _connectedModeratorClients[mcId].Strikes = 0;
                            }
                            else
                            {
                                response = JsonConvert.SerializeObject(new ErrorMessage(
                                    gamePausedStatusChange.ModeratorID,
                                    ErrorType.IllegalPauseAction,
                                    ""));
                                AddStrike(mcId);
                            }
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(new ErrorMessage(
                                gamePausedStatusChange.ModeratorID,
                                ErrorType.IllegalPauseAction,
                                ""));
                            AddStrike(mcId);
                        }

                        break;

                    // ######## Close Session ########
                    case MessageType.RequestCloseSession:

                        RequestCloseSessionMessage closeSessionMessage =
                            JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                        if (_connectedModeratorClients[mcId].SessionKey.Equals(closeSessionMessage.SessionKey))
                        {
                            response = JsonConvert.SerializeObject(
                                new SessionClosedMessage(closeSessionMessage.ModeratorID));
                            ServerLogger.LogDebug(
                                $"Session {closeSessionMessage.SessionKey} has finished, {_connectedModeratorClients[mcId].SocketConnection.ConnectionInfo.ClientIpAddress} has disconnected.");
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(
                                new ErrorMessage(closeSessionMessage.ModeratorID, ErrorType.WrongSession, ""));
                            ServerLogger.LogDebug(
                                $"MC-{closeSessionMessage.SessionKey} tried to close Session but failed to due wrong sessionKey. \n\tTransmitted sessionKey: \t{closeSessionMessage.SessionKey}\n\tActual sessionKey: \t\t{_connectedModeratorClients[mcId].SessionKey}");
                            AddStrike(mcId);
                        }

                        break;

                    // unknown MessageType
                    default:
                        //FR57 'ServerLogic persistence': "The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data."
                        ServerLogger.LogDebug(
                            $"Corrupted MessageType: {typeof(MessageType)}, received from {_connectedModeratorClients[mcId].ModeratorGuid}, {_connectedModeratorClients[mcId].SocketConnection.ConnectionInfo} within session {_connectedModeratorClients[mcId].SessionKey}.");
                        //FR31 'Network protocol violation'
                        response = JsonConvert.SerializeObject(new ErrorMessage(
                            messageContainer.ModeratorID,
                            ErrorType.IllegalMessage,
                            "Message out of order or messageType unknown."));
                        AddStrike(mcId);
                        break;
                }

                _connectedModeratorClients[mcId].ResetInactivity();

            }
            catch (JsonSerializationException jsonSerializationException)
            {
                ServerLogger.LogDebug($"Exception occurred on json-serialization: {jsonSerializationException}.");
            }
            catch (KeyNotFoundException)
            {
                //may be thrown when the MC continues writing after beeing kicked, which is kind of intended, as the MC tries to reconnect after losing connection.
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
        /// <param name="moderatorId">The Guid of the violator.</param>
        internal void AddStrike(Guid moderatorId)
        {
            //FR31 'Network protocol violation'
            ServerLogger.LogDebug($"Strike for {moderatorId}.");
            _connectedModeratorClients[moderatorId].Strikes += 1;
            if (_connectedModeratorClients[moderatorId].Strikes >= 3)
            {
                _connectedModeratorClients[moderatorId].Stop(true);
                _connectedModeratorClients.Remove(moderatorId);
            }
        }

        /// <summary>
        /// Generates a random session key and compares it with already recorded sessions and recreates it if necessary.
        /// If no unique SessionKey can be created after several attempts, all current Sessions also using the generated key are stopped and set inactive.
        /// </summary>
        /// <param name="maxRecursionCycles">The maximum number of recursions allowed to generate a random unique sessionKey.</param>
        /// <returns>A unique SessionKey.</returns>
        internal string GenerateSessionKey(int maxRecursionCycles)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            string sessionKey = new(Enumerable.Repeat(chars, 6).Select(s => s[rand.Next(s.Length)]).ToArray());

            //Termination condition, takes effect if, after several runs, no session key can be generated which is not already in use.
            if (maxRecursionCycles == 0)
            {
                foreach (var (_, value) in _connectedModeratorClients)
                {
                    //stop all session with 
                    if (value.SessionKey.Equals(sessionKey))
                    {
                        value.Stop(true);
                        value.IsInactive = true;
                        ServerLogger.LogWarning($"Couldn't generate unique Session-Key. Session-Key {sessionKey} might be duplicate. Removed session of MC-{value.ModeratorGuid} with identical Session-Key");

                    }
                }
                return sessionKey;
            }

            //SessionKey already in use?
            foreach (var (_, value) in _connectedModeratorClients)
            {
                if (value.SessionKey.Equals(sessionKey))
                {
                    GenerateSessionKey(maxRecursionCycles - 1);
                }
            }

            return sessionKey;
        }
    }
}
