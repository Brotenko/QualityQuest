﻿using Microsoft.AspNetCore.SignalR;
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

        public async Task SendMessageToCaller(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }
    }
}
