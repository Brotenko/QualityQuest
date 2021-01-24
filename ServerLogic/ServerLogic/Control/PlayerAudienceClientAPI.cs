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
    public class PlayerAudienceClientAPI
    {
        //private HttpServer server;
        private PABackend pABackend;


        public void StartServer(int port)
        {
            pABackend = new PABackend(port);
        }

        public void StopServer()
        {
            pABackend.StopServer();
        }

        public void StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            pABackend.SendPushMessage(sessionkey, prompt, options);
        }

        public void StartNewSession(string sessionkey)
        {
            
        }

        public void RemoveSession(string sessionkey)
        {
        
        }

        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResults(string sessionkey, string prompt)
        {
            
        }



        public PlayerAudienceClientAPI()
        {
            
        }
    }
}
