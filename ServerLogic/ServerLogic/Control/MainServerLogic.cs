using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Control
{
    //TODO: Migth be completely refactored into ModeratorClientManager
    public class MainServerLogic
    {
        private PlayerAudienceClientAPI playerAudienceClientAPI;
        private ModeratorClientManager moderatorClientManager;

        public MainServerLogic()
        {
            playerAudienceClientAPI = new PlayerAudienceClientAPI();
            moderatorClientManager = new ModeratorClientManager(playerAudienceClientAPI);
        }

        public void StartServer(int port)
        {
            moderatorClientManager.StartWebsocket();
            playerAudienceClientAPI.StartServer(port);
        }

        public void StopServer()
        {
            moderatorClientManager.StopWebsocket();
            playerAudienceClientAPI.StopServer();
        }
    }
}
