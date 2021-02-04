using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerHub : Hub
    {

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="user"></param>
        /// 
        /// <param name="message"></param>
        /// 
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sessionkey"></param>
        /// 
        /// <returns></returns>
        public async Task ValidateKey(string sessionkey)
        {
            bool isValid = PABackend.PAVotingResults.GetSessionKeys().Contains(sessionkey);

            await Clients.Caller.SendAsync("KeyValidation", isValid);

            if (isValid)
            {
                PABackend.AddConnection(sessionkey, Context.ConnectionId);
                Console.WriteLine(string.Join(", ", PABackend.ConnectionList.Select(kv => kv.Key + " = " + "[" + string.Join(", ", kv.Value.ToArray()) + "]").ToArray()));
                await Groups.AddToGroupAsync(Context.ConnectionId, sessionkey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sessionkey"></param>
        /// 
        /// <param name="option"></param>
        /// 
        /// <returns></returns>
        public async Task SendVote(string sessionkey, Guid option)
        {
            Console.WriteLine(sessionkey, option);
            PABackend.CountNewVote(sessionkey, option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected: " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// 
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected: " + Context.ConnectionId + " ; " + exception);
            PABackend.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
