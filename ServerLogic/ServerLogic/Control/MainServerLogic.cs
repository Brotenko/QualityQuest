using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Control
{
    public class MainServerLogic
    {
        public PlayerAudienceClientAPI playerAudienceClientLogicHandler;

        public MainServerLogic()
        {
            playerAudienceClientLogicHandler = new PlayerAudienceClientAPI();
        }
    }
}
