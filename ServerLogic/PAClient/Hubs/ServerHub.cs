using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient.Hubs
{
    public class ServerHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

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

        public async Task SendVote(string sessionkey, string option)
        {
            Console.WriteLine(sessionkey, option);
            PABackend.CountNewVote(sessionkey, option);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected: " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected: " + Context.ConnectionId + " ; " + exception);
            PABackend.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
