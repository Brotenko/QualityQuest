using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerAudienceClient;

namespace ServerLogic.Control
{
    public class PlayerAudienceClientLogicHandler
    {
        private HttpServer server;

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
            server = new HttpServer("F:/QualityQuest/ServerLogic/PlayerAudienceClient/", port);
        }

        public PlayerAudienceClientLogicHandler()
        {
            
        }
    }
}
