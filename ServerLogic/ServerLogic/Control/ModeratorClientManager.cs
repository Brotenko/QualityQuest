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
    class ModeratorClientManager
    {
        //TODO move to settings, same for WebSocket port and Homepage port
        //TODO maybe refactor/rename/both
        private class ModeratorClientAttributes
        {
            public Guid moderatorGuid;
            public string sessionkey;

            public ModeratorClientAttributes(Guid moderatorGuid, string sessionkey)
            {
                this.moderatorGuid = moderatorGuid;
                this.sessionkey = sessionkey;
            }
        }
        private List<IWebSocketConnection> allsockets = new();
        private Dictionary<IWebSocketConnection,ModeratorClientAttributes> socketToModerator = new();
        private WebSocketServer server = new("ws://"+Settings.Default.ServerURL+":"+Settings.Default.MCWebSocketPort);

        public void StopWebsocket()
        {
            foreach (var connection in allsockets)
            {
                //connection.Send(SessionClosedMessage);
                connection.Close();
            }
            //todo: necessary?
            //server.Dispose();
        }
        public void StartWebsocket()
        {
            //TODO: this certificate is for testing purposes only and should never ever be used in an actual deployment!
            //server.Certificate = new X509Certificate2(Settings.Default.CertFilePath, "thisIsForTestingOnly");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    //todo Use ServerLogger for all Console
                    ServerLogger.LogDebug("WebSocket-connection to " + socket.ConnectionInfo.ClientIpAddress + " established.\nHeader: " + socket.ConnectionInfo.Headers+
                    "\nIP: " + socket.ConnectionInfo.ClientIpAddress+"\nSubProtocol: " + socket.ConnectionInfo.NegotiatedSubProtocol);
                    allsockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    ServerLogger.LogDebug("Websocket-connection to "+socket.ConnectionInfo.ClientIpAddress+" was closed.");
                    allsockets.Remove(socket);
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
                        //TODO: Check password
                        //after pw confirmation, add GUID in a list to the corresponding socket an send a 'SessionOpenedMessage'
                        //check pw
                        //if(pw ok?){
                        socketToModerator.Add(socket, new ModeratorClientAttributes(openSessionMessage.ModeratorID, GenerateSessionKey(openSessionMessage.ModeratorID)));
                        //ServerLogger.LogInformation("Current IP: "+server.Location);
                        return JsonConvert.SerializeObject(new SessionOpenedMessage(socketToModerator[socket].moderatorGuid, socketToModerator[socket].sessionkey, new Uri($"https://{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}"), GenerateQR()));
                        //} else { socket.Close();

                    //Is sent multiple times after MC lost connection to server
                    case MessageType.RequestServerStatus:
                        RequestServerStatusMessage serverStatusMessage =
                            JsonConvert.DeserializeObject<RequestServerStatusMessage>(message);
                        return (JsonConvert.SerializeObject(new ServerStatusMessage(socketToModerator[socket].moderatorGuid)));

                    //To reestablish a lost connection
                    case MessageType.Reconnect:
                        ReconnectMessage reconnectMessage = 
                            JsonConvert.DeserializeObject<ReconnectMessage>(message);
                        //searches for the ModeratorID in the previous connections, and replaces the socket in the entry with the current one.
                        foreach (var (key, value) in socketToModerator)
                        {
                            if (value.moderatorGuid.Equals(reconnectMessage.ModeratorID))
                            {
                                socketToModerator.Add(socket, new ModeratorClientAttributes(value.moderatorGuid, value.sessionkey));
                                socketToModerator.Remove(key);
                                return (JsonConvert.SerializeObject(new ReconnectSuccessfulMessage(value.moderatorGuid)));
                            }
                        }
                        break;

                    //Is sent to request the start of the current Online-Session
                    case MessageType.RequestGameStart:
                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
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
                        //socketToModerator.Remove(socket);
                        //socket.Close();
                        break;

                    // unknown Messagetype
                    default:
                        //todo always end the session if the message type is incorrect?
                        if (socketToModerator.ContainsKey(socket))socketToModerator.Remove(socket);
                        ServerLogger.LogDebug($"Corrupted Messagetype: {typeof(MessageType)}.");
                        socket.Close();
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
        /// Generates a semi-random sessionkey which starts with a part of the corresponding ModeratorGuid.
        /// </summary>
        /// <param name="moderatorGuid"></param>
        /// <returns>A sessionkey.</returns>
        private string GenerateSessionKey(Guid moderatorGuid)
        {
            var rand = new Random();
            return moderatorGuid.ToString().Split(":")[0] +rand.Next(1000,9999).ToString();
        }

        private Bitmap GenerateQR()
    {
            //TODO get serverURL and port from resource file
            BarcodeGenerator generator = new(EncodeTypes.QR, $"{Settings.Default.ServerURL}:{Settings.Default.PAWebPagePort}");
            generator.Parameters.Resolution = 800;
            return generator.GenerateBarCodeImage();
        }
    }
}
