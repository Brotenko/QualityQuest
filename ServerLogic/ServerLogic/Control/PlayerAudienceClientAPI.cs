using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using PlayerAudienceClient;
using PAClient;

namespace ServerLogic.Control
{
    public class PlayerAudienceClientLogicHandler
    {
        //private HttpServer server;
        private PABackend p;

        public List<string> ValidSessionKeys
        {
            get; private set;
        }

        public void UpdateSessionKeys(string key)
        {
            if (ValidSessionKeys.Contains(key))
            {
                ValidSessionKeys.Remove(key);
            }
            else
            {
                ValidSessionKeys.Add(key);
            }
        }

        public void StartServer(int port)
        {
            //server = new HttpServer("F:/QualityQuest/ServerLogic/PlayerAudienceClient/", port);
            

            ValidSessionKeys = new List<string> { "asdasd", "qweqwe" };
            //t = Task.Run(() => p = new PABackend(port));
            p = new PABackend(port);
        }

        public void StopServer()
        {
            //server.Dispose();
            //t.Dispose();
            p.StopServer();
            
        }

        public void SendDebugMessage(string[] list)
        {
            p.SendPushMessage(list[0], list[1], list[2..]);
        }

        public void SendSessionKeys()
        {
            PABackend.ValidSessionKeys = ValidSessionKeys;
        }

        public PlayerAudienceClientLogicHandler()
        {
            
        }
    }
}
