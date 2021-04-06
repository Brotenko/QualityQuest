﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;
using Fleck;
using ServerLogic.Model;
using ServerLogic.Properties;


namespace ServerLogic.Control
{
    public class MainServerLogic
    {
        private Dictionary<IWebSocketConnection, ModeratorClientManager> _connectedModeratorClients;
        private WebSocketServer _server;
        private PlayerAudienceClientAPI _playerAudienceClientAPI;
        private const int MaxRepForRandomGeneration = 16;

        //TODO Remove default password from settings

        /// <summary>
        /// Contains a WebSocket through which messages are exchanged with the ModeratorClient,
        /// as well as methods needed for the general management of this communication.
        /// </summary>
        public MainServerLogic()
        {
            _playerAudienceClientAPI = new PlayerAudienceClientAPI();
            _connectedModeratorClients = new Dictionary<IWebSocketConnection, ModeratorClientManager>();
            _server = new WebSocketServer("wss://0.0.0.0:" + Settings.Default.MCWebSocketPort);
            ServerLogger.LogDebug($"WebSocket secure connection established: {_server.IsSecure}.");
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
        internal void StartWebsocket()
        {
            _server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, "thisIsForTestingOnly");
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
        internal string CheckStringMessage(string message, IWebSocketConnection socket)
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
                        if (ServerShell.StringToSHA256Hash(openSessionMessage.Password).Equals(Settings.Default.PWHash))
                        {
                            _connectedModeratorClients.Add(socket, new ModeratorClientManager(openSessionMessage.ModeratorID, GenerateSessionKey(MaxRepForRandomGeneration), socket, _playerAudienceClientAPI));
                            response = JsonConvert.SerializeObject(new SessionOpenedMessage(_connectedModeratorClients[socket].ModeratorGuid, _connectedModeratorClients[socket].SessionKey, new Uri($"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}")));
                        }
                        else
                        {
                            //todo: remove before release
                            ServerLogger.LogDebug($"Socket closed due to wrong RequestOpenSession: Password received: {openSessionMessage.Password}.");
                            
                            socket.Close();
                            response = JsonConvert.SerializeObject(
                                new ErrorMessage(openSessionMessage.ModeratorID, ErrorType.WrongPassword, ""));
                        }
                        ServerLogger.LogDebug($"Received RequestOpenSession. SessionKey is {_connectedModeratorClients[socket].SessionKey}.");
                        break;

                    //Is sent multiple times after MC lost connection to server
                    case MessageType.RequestServerStatus:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            RequestServerStatusMessage serverStatusMessage =
                                JsonConvert.DeserializeObject<RequestServerStatusMessage>(message);
                            response = (JsonConvert.SerializeObject(
                                new ServerStatusMessage(_connectedModeratorClients[socket].ModeratorGuid)));
                            ServerLogger.LogDebug("Received RequestServerStatus.");
                        }
                        break;

                    //To reestablish a lost connection
                    case MessageType.Reconnect:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            ReconnectMessage reconnectMessage =
                                JsonConvert.DeserializeObject<ReconnectMessage>(message);
                            //searches for the ModeratorID in the previous connections, and replaces the socket in the entry with the current one.
                            foreach (var (key, currentModeratorClientManager) in _connectedModeratorClients)
                            {
                                if (currentModeratorClientManager.ModeratorGuid.Equals(reconnectMessage.ModeratorID))
                                {
                                    _connectedModeratorClients.Add(
                                        socket, new ModeratorClientManager(currentModeratorClientManager.ModeratorGuid,
                                            currentModeratorClientManager.SessionKey, socket, _playerAudienceClientAPI));
                                    _connectedModeratorClients.Remove(key);
                                    response = (JsonConvert.SerializeObject(
                                        new ReconnectSuccessfulMessage(currentModeratorClientManager.ModeratorGuid)));
                                    ServerLogger.LogDebug("Reconnect successful.");
                                }
                            }
                        }
                        break;

                    //Is sent to request the start of the current Online-Session
                    case MessageType.RequestGameStart:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            RequestGameStartMessage gameStartMessage =
                                JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                            _connectedModeratorClients[socket].StopAudienceCountLiveUpdate();
                            response = (JsonConvert.SerializeObject(
                                new GameStartedMessage(gameStartMessage.ModeratorID)));
                            ServerLogger.LogDebug("Received RequestGameStart.");
                        }
                        break;

                    // ######## Voting ######## 
                    case MessageType.RequestStartVoting:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            RequestStartVotingMessage startVotingMessage =
                                JsonConvert.DeserializeObject<RequestStartVotingMessage>(message);
                            _connectedModeratorClients[socket].StartVotingTimer(startVotingMessage);
                            //votingOptions get extracted inside ModeratorClientManager
                            socket.Send(JsonConvert.SerializeObject(
                                new VotingStartedMessage(startVotingMessage.ModeratorID)));
                            ServerLogger.LogDebug("Received RequestStartVoting.");
                        }
                        break;

                    // ######## Control messages ########
                    case MessageType.RequestGamePausedStatusChange:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            RequestGamePausedStatusChangeMessage gamePausedStatusChange =
                                JsonConvert.DeserializeObject<RequestGamePausedStatusChangeMessage>(message);
                            _connectedModeratorClients[socket].PauseVotingTimer(gamePausedStatusChange.GamePaused);
                            response = (JsonConvert.SerializeObject(
                                new GamePausedStatusMessage(gamePausedStatusChange.ModeratorID, gamePausedStatusChange.GamePaused)));
                        }
                        break;

                    // ######## Postgame ########
                    case MessageType.RequestCloseSession:
                        if (SocketExists(socket) && ModeratorGuidExists(messageContainer.ModeratorID, socket))
                        {
                            RequestCloseSessionMessage closeSessionMessage =
                                JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                            if (SessionKeyExists(closeSessionMessage.SessionKey))
                            {
                                response = (JsonConvert.SerializeObject(
                                        new SessionClosedMessage(closeSessionMessage.ModeratorID)));
                                _connectedModeratorClients[socket].Stop();
                                _connectedModeratorClients.Remove(socket);
                                ServerLogger.LogDebug(
                                    $"Session {closeSessionMessage.SessionKey} was closed, {socket.ConnectionInfo} has disconnected.");
                            }
                            else
                            {
                                response = (JsonConvert.SerializeObject(
                                    new ErrorMessage(closeSessionMessage.ModeratorID, ErrorType.SessionDoesNotExist, "")));
                            }
                        }
                        break;

                    // unknown MessageType
                    default:
                        //FR57 'ServerLogic persistence': "The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data."
                        ServerLogger.LogDebug($"Corrupted Messagetype: {typeof(MessageType)}, received from {_connectedModeratorClients[socket].ModeratorGuid}, {socket.ConnectionInfo} within session {_connectedModeratorClients[socket].SessionKey}.");
                        //FR31 'Network protocol violation'
                        socket.Send(JsonConvert.SerializeObject(new ErrorMessage(messageContainer.ModeratorID, ErrorType.IllegalMessage,
                            "Message out of order or messageType unknown.")));
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
        internal void AddStrike(IWebSocketConnection socket)
        {
            //FR31 'Network protocol violation'
            _connectedModeratorClients[socket].Strikes += 1;
            if (_connectedModeratorClients[socket].Strikes >= 3)
            {
                _connectedModeratorClients[socket].Stop();
                _connectedModeratorClients.Remove(socket);
            }
        }

        internal Boolean SessionKeyExists(string sessionKey)
        {
            foreach (var (key, value) in _connectedModeratorClients)
            {
                if (value.SessionKey.Equals(sessionKey)) return true;
            }

            return false;
        }

        internal Boolean ModeratorGuidExists(Guid moderatorGuid, IWebSocketConnection socket)
        {
            foreach (var (key, moderatorClientManager) in _connectedModeratorClients)
            {
                if (moderatorClientManager.ModeratorGuid.Equals(moderatorGuid)) return true;
            }

            socket.Send(JsonConvert.SerializeObject(new ErrorMessage(moderatorGuid, ErrorType.UnknownGuid, "")));
            return false;
        }

        internal Boolean SocketExists(IWebSocketConnection socket)
        {
            return _connectedModeratorClients.TryGetValue(socket, out ModeratorClientManager value);
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
            string sessionKey = new string(Enumerable.Repeat(chars, 6).Select(s => s[rand.Next(s.Length)]).ToArray());

            //Termination condition, takes effect if, after several runs, no session key can be generated which is not already in use.
            if (maxRecursionCycles == 0)
            {
                ServerLogger.LogWarning($"Couldn't generate unique Session-Key. Session-Key {sessionKey} might be duplicate.");
                return sessionKey;
            }
            //SessionKey already in use?

            if (SessionKeyExists(sessionKey)) GenerateSessionKey(maxRecursionCycles - 1);

            return sessionKey;
        }
    }
}
