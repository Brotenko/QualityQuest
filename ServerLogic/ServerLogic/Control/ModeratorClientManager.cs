using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;
using Fleck;

namespace ServerLogic.Control
{
    class ModeratorClientManager
    {
        void startWebsocket()
        {
            var allsockets = new List<IWebSocketConnection>();
            //var server = new WebSocketServer("wss://127.0.0.1:8181");
            var server = new WebSocketServer("ws://127.0.0.1:8181");
            //TODO: this certificate is for testing purposes only and should never ever be used in an actual deployment!
            //server.Certificate = new X509Certificate2("..\\..\\..\\TestCert.pfx", "thisIsForTestingOnly");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
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
                    socket.Send(checkStringMessage(message, socket));
                };
                socket.OnBinary = bytes =>
                {
                    Console.WriteLine($"OnBinary {Encoding.UTF8.GetString(bytes)}");
                    //socket.Send(checkByteMessage(bytes, socket));
                };
                socket.OnError = exception =>
                    Console.WriteLine($"OnError {exception.Message}");
                socket.OnPing = bytes =>
                    Console.WriteLine("OnPing");
                socket.OnPong = bytes =>
                    Console.WriteLine("OnPong");
            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (IWebSocketConnection socket in allsockets)
                {
                    //standard socket raw send method
                    socket.Send(input);
                }

                input = Console.ReadLine();
            }
        }

        private string checkStringMessage(string message, IWebSocketConnection socket)
        {
            if (message.Contains("Hello"))
            {
                Console.WriteLine("Responded Hello");
                return "Hello back!";
            }
            else
            {
                // GameStartedMessage gameStartedMessage = new GameStartedMessage(new Guid());
                // gameStartedMessage = JsonConvert.DeserializeObject<GameStartedMessage>(message, settings);

                GameStartedMessage gameStartedMessage = JsonConvert.DeserializeObject<GameStartedMessage>(message);
                //MessageContainer messageContainer = JsonConvert.DeserializeObject<MessageContainer>(message);
                //HelloMessage helloMessage = JsonConvert.DeserializeObject<HelloMessage>(message);
                //Console.WriteLine(helloMessage.name);
                Console.WriteLine("String:\n" + gameStartedMessage.ToString());
                Console.WriteLine("Received Guid: " + gameStartedMessage.ModeratorID);
                Console.WriteLine("Im fine");
            }

            return "";
        }

        private string checkByteMessage(byte[] messageBytes, IWebSocketConnection socket)
        {
            GameStartedMessage gameStartedMessage =
                JsonConvert.DeserializeObject<GameStartedMessage>(Encoding.UTF8.GetString(messageBytes));
            Console.WriteLine("Bytes:\n" + gameStartedMessage.ToString());
            Console.WriteLine("Received Guid: " + gameStartedMessage.ModeratorID);
            Console.WriteLine("Im fine");
            return "";
        }
    }
}
