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
            bool isValid = PABackend.ValidSessionKeys.Contains(sessionkey);

            await Clients.Caller.SendAsync("KeyValidation", isValid);

            if (isValid)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, sessionkey);
            }
        }

        /*
        public async Task SendMessageToCaller(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToGroup(string user, string message, string sessionkey)
        {
            await Clients.Group(sessionkey).SendAsync("ReceiveMessage", user, message);
        }
        */
    }
}
