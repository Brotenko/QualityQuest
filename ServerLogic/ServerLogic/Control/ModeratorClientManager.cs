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

namespace ServerLogic.Control
{
    class ModeratorClientManager
    {
        //TODO move to settings, same for WebSocket port and Homepage port
        public static Uri serverURL = new Uri("127.0.0.1");
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
        private List<IWebSocketConnection> allsockets = new List<IWebSocketConnection>();
        private Dictionary<IWebSocketConnection,ModeratorClientAttributes> socketToModerator = new Dictionary<IWebSocketConnection, ModeratorClientAttributes>();
        private WebSocketServer server = new WebSocketServer("ws://"+serverURL+":8181");

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
            //server.Certificate = new X509Certificate2("..\\..\\..\\TestCert.pfx", "thisIsForTestingOnly");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    //todo Use ServerLogger for all Console
                    Console.WriteLine("Open!");
                    Console.WriteLine("Header: " + socket.ConnectionInfo.Headers);
                    Console.WriteLine("IP: " + socket.ConnectionInfo.ClientIpAddress);
                    Console.WriteLine("SubProtocol: " + socket.ConnectionInfo.NegotiatedSubProtocol);
                    allsockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allsockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine($"OnMessage {message}");
                    socket.Send(CheckStringMessage(message, socket));
                };
                socket.OnBinary = bytes =>
                {
                    Console.WriteLine($"OnBinary {Encoding.UTF8.GetString(bytes)}");
                };
                socket.OnError = exception =>
                    Console.WriteLine($"OnError {exception.Message}");
                socket.OnPing = bytes =>
                    Console.WriteLine("OnPing");
                socket.OnPong = bytes =>
                    Console.WriteLine("OnPong");
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
                        //TODO: build a sessionKey-Generator
                        //TODO: deal somehow with the currently unknown URL of the Homepage
                        //after pw confirmation, add GUID in a list to the corresponding socket an send a 'SessionOpenedMessage'
                        //check pw
                        //if(pw ok?){
                        socketToModerator.Add(socket, new ModeratorClientAttributes(openSessionMessage.ModeratorID, GenereateSessionKey(openSessionMessage.ModeratorID)));
                        //ServerLogger.LogInformation("Current IP: "+server.Location);
                        socket.Send(JsonConvert.SerializeObject(new SessionOpenedMessage(socketToModerator[socket].moderatorGuid, socketToModerator[socket].sessionkey, new Uri(serverURL+"7777"), GenerateQR())));
                        //} else { socket.Close();}
                        break;
                    case MessageType.RequestServerStatus:
                        RequestServerStatusMessage serverStatusMessage =
                            JsonConvert.DeserializeObject<RequestServerStatusMessage>(message);
                        socket.Send(JsonConvert.SerializeObject(new ServerStatusMessage(socketToModerator[socket].moderatorGuid)));
                        break;
                    case MessageType.Reconnect:
                        ReconnectMessage reconnectMessage = 
                            JsonConvert.DeserializeObject<ReconnectMessage>(message);
                        foreach (KeyValuePair<WebSocketConnection, ModeratorClientAttributes> entry in socketToModerator)
                        {
                            if (entry.Value.moderatorGuid.Equals(reconnectMessage.ModeratorID))
                            {
                                entry.Key = socket;
                                socket.Send(JsonConvert.SerializeObject(new ReconnectSuccessfulMessage(entry.Value.moderatorGuid)));
                            }
                        }
                        break;
                    case MessageType.RequestGameStart:
                        RequestGameStartMessage gameStartMessage =
                            JsonConvert.DeserializeObject<RequestGameStartMessage>(message);
                        break;
                    // ######## Voting ######## 
                    case MessageType.RequestStartVoting:
                        RequestStartVotingMessage startVotingMessage =
                            JsonConvert.DeserializeObject<RequestStartVotingMessage>(message);
                        break;
                    // ######## Control messages ########
                    case MessageType.RequestGamePausedStatusChange:
                        RequestGamePausedStatusChangeMessage gamePausedStatusChange =
                            JsonConvert.DeserializeObject<RequestGamePausedStatusChangeMessage>(message);
                        break;
                    // ######## Postgame ########
                    case MessageType.RequestCloseSession:
                        RequestCloseSessionMessage closeSessionMessage =
                            JsonConvert.DeserializeObject<RequestCloseSessionMessage>(message);
                        break;
                    // unknown Messagetype
                    default:
                        //socket.Close();
                        //DebugLog
                        break;

                }
            }
            catch (JsonSerializationException jsonSerializationException)
            {
                //Todo Debug-Log
            }
            catch (Exception exception)
            {
                //Todo Warning-Log (unexpected Exception e)
            }

            return "";
        }

        /// <summary>
        /// Generates a semi-random sessionkey which starts with a part of the corresponding ModeratorGuid.
        /// </summary>
        /// <param name="moderatorGuid"></param>
        /// <returns>A sessionkey.</returns>
        private string GenereateSessionKey(Guid moderatorGuid)
        {
            var rand = new Random();
            return moderatorGuid.ToString().Split(":")[0] +rand.Next(1000,9999).ToString();
        }

        private Bitmap GenerateQR()
        {
            //TODO get serverURL and port from resource file
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, serverURL + "7777");
            generator.Parameters.Resolution = 800;
            return generator.GenerateBarCodeImage();
        }
    }
}
