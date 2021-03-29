using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Fleck;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;

namespace ServerLogic.Control
{
    /// <summary>
    /// Contains attributes and classes that are necessary for the exchange with the ModeratorClient.
    /// </summary>
    public class ModeratorClientManager
    {
        public Guid moderatorGuid;
        public string sessionkey;
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> statistics;
        //public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> statistics;
        //FR31 'Network protocol violation', connection is canceled after three strikes/violations against network-protocol in a row.
        public int strikes;
        private Timer PlayerAudienceCountLiveUpdateTimer;
        private Timer VotingTimer;
        private IWebSocketConnection _socket;
        private DateTime votingStarted;
        private int votingTimeLeft;
        private PlayerAudienceClientAPI playerAudienceClientApi;
        private KeyValuePair<Guid, string> currentPrompt;

        public ModeratorClientManager(Guid moderatorGuid, string sessionkey, IWebSocketConnection socket, PlayerAudienceClientAPI playerAudienceClientApi)
        {
            this.moderatorGuid = moderatorGuid;
            this.sessionkey = sessionkey;
            this.statistics = new Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>();
            this.strikes = 0;
            this._socket = socket;
            this.playerAudienceClientApi = playerAudienceClientApi;
            playerAudienceClientApi.StartNewSession(sessionkey);
            StartAudienceCountLiveUpdate();
        }


        /// <summary>
        /// 
        /// </summary>
        public void StartAudienceCountLiveUpdate()
        {
            //FR42 'PlayerAudience-Client count live update'
            //The ServerLogic should inform the Moderator-Client in 3 seconds intervals about the amount of PlayerAudience-Clients connected to the ServerLogic,
            //as long as the game didn't start yet.
            this.PlayerAudienceCountLiveUpdateTimer = new Timer(3000);
            PlayerAudienceCountLiveUpdateTimer.Elapsed += SendAudienceCount;
            PlayerAudienceCountLiveUpdateTimer.AutoReset = true;
            PlayerAudienceCountLiveUpdateTimer.Enabled = true;
        }

        /// <summary>
        /// This method is automatically triggered by the AudiencCountLiveUpdate-Timer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SendAudienceCount(object source, ElapsedEventArgs e)
        {
            _socket.Send(JsonConvert.SerializeObject(new AudienceStatusMessage(this.moderatorGuid, PAClient.PABackend.ConnectionList[this.sessionkey].Count)));
        }


        public void StartVotingTimer(RequestStartVotingMessage startVoting)
        {
            playerAudienceClientApi.StartNewVote(sessionkey, startVoting.VotingPrompt, startVoting.VotingOptions);
            this.votingTimeLeft = startVoting.VotingTime;
            //Timer takes milliseconds
            this.VotingTimer = new Timer(startVoting.VotingTime * 1000);
            VotingTimer.Elapsed += SendVotingResults;
            VotingTimer.AutoReset = false;
            VotingTimer.Enabled = true;
            this.votingStarted = DateTime.Now;
            currentPrompt = startVoting.VotingPrompt;
        }

        public void PauseVotingTimer(bool pause)
        {
            if (pause)
            {
                VotingTimer.Stop();
                this.votingTimeLeft = votingTimeLeft - votingStarted.Subtract(DateTime.Now).Seconds;
            }
            else
            {
                StartVotingTimer(votingTimeLeft);
            }
        }

        private void StartVotingTimer(int time)
        {
            this.votingTimeLeft = time;
            //Timer takes milliseconds
            this.VotingTimer = new Timer(time * 1000);
            VotingTimer.Elapsed += SendVotingResults;
            VotingTimer.AutoReset = false;
            VotingTimer.Enabled = true;
            this.votingStarted = DateTime.Now;
        }

        private void SendVotingResults(object source, ElapsedEventArgs e)
        {
            
            Dictionary<KeyValuePair<Guid, string>, int> votingResults = playerAudienceClientApi.GetVotingResult(sessionkey, currentPrompt);
            KeyValuePair<Guid, string> winningOption = new();
            int winningVotes = 0;
            foreach (var (key, value) in votingResults)
            {
                if (value > winningVotes)
                {
                    winningOption = key;
                    winningVotes = value;
                }
            }

            _socket.Send(JsonConvert.SerializeObject(new VotingEndedMessage(moderatorGuid,winningOption, votingResults)));
        }


        /// <summary>
        /// Always call before removing a ModeratorClientAttributes-Object.
        /// Stops all processes that may still be running in the background.
        /// </summary>
        public void Stop()
        {
            try
            {
               StartAudienceCountLiveUpdate();
                _socket.Close();
            }
            catch (Exception exception)
            {
                //Closing a already stopped process might cause an exception. However, this should not usually cause any problems.
                ServerLogger.LogDebug($"ModeratorClientManager.Stop() caused Exception: {exception}");
            }
        }

        /// <summary>
        /// Stops and disposes the Timer used for the AudienceCountLiveUpdate.
        /// </summary>
        public void StopAudienceCountLiveUpdate()
        {
            PlayerAudienceCountLiveUpdateTimer.Stop();
            PlayerAudienceCountLiveUpdateTimer.Dispose();
        }

    }

}