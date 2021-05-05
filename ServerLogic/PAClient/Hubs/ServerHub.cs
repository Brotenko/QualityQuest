using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace PAClient.Hubs
{
    /// <summary>
    /// The SignalR communication hub of the PAClient backend.
    /// </summary>
    public class ServerHub : Hub
    {
        /// <summary>
        /// Validates that the sessionkey transmitted by the PAClient
        /// is valid, and adds the user to the Hub-Group identified by
        /// the sessionkey.
        /// </summary>
        /// 
        /// <param name="sessionkey">Sessionkey to be validated.</param>
        public async Task ValidateKey(string sessionkey)
        {
            bool isValid = PABackend.PAVotingResults.GetSessionKeys().Contains(sessionkey);

            await Clients.Caller.SendAsync("KeyValidation", isValid);

            if (isValid)
            {
                PABackend.AddConnection(sessionkey, Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, sessionkey);
            }
        }

        /// <summary>
        /// Transmits a vote from any connected PAClient to the backend.
        /// </summary>
        /// 
        /// <param name="sessionkey">Specifies for which Hub-Group the
        /// vote shall be counted.</param>
        /// 
        /// <param name="option">Specifies for which option a vote was
        /// issued.</param>
        public async Task SendVote(string sessionkey, Guid option)
        {
            PABackend.CountNewVote(sessionkey, option);
        }

        /// <summary>
        /// Called when a new connection is established with the Hub.
        /// Currently only used for debugging purposes.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when an user disconnects from the Hub. In that case
        /// the clientID is removed from all instances of the Hub.
        /// </summary>
        /// 
        /// <param name="exception">If the disconnection occurred due to
        /// an exception, then it will be specified here.</param>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            PABackend.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
