using System;
using System.Collections.Generic;
using System.Timers;
using Fleck;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;

namespace ServerLogic.Control
{
    public class ModeratorClientManager
    {
        public Guid ModeratorGuid;
        public string SessionKey;
        //FR31 'Network protocol violation', connection is canceled after three Strikes/violations against network-protocol in a row.
        public int Strikes;
        public bool IsVoting;
        public bool IsPaused;
        public bool IsInactive;

        private Timer _playerAudienceCountLiveUpdateTimer;
        private Timer _votingTimer;
        private KeyValuePair<Guid, string> _currentPrompt;

        private readonly Timer _inactivityTimer;
        private readonly IWebSocketConnection _socket;
        private readonly PlayerAudienceClientAPI _playerAudienceClientApi;


        /// <summary>
        /// An object that manages attributes and methods required for communication between the server and a specific moderator client.
        /// </summary>
        /// <param name="moderatorGuid">The transmitted Guid of a ModeratorClient.</param>
        /// <param name="sessionKey">The assigned sessionKey.</param>
        /// <param name="socket">The assigned IWebSocketConnection.</param>
        /// <param name="playerAudienceClientApi"></param>
        public ModeratorClientManager(Guid moderatorGuid, string sessionKey, IWebSocketConnection socket, PlayerAudienceClientAPI playerAudienceClientApi)
        {
            ModeratorGuid = moderatorGuid;
            SessionKey = sessionKey;
            Strikes = 0;
            _socket = socket;
            _playerAudienceClientApi = playerAudienceClientApi;
            _playerAudienceClientApi.StartNewSession(sessionKey);
            StartAudienceCountLiveUpdate();
            IsVoting = false;
            IsPaused = false;
            IsInactive = false;
            //30min = 1800000ms
            _inactivityTimer = new Timer(1800000);
            _inactivityTimer.Elapsed += SetToInactive;
            _inactivityTimer.AutoReset = false;
            _inactivityTimer.Enabled = true;
        }

        /// <summary>
        /// Resets the inactivity Timer for this ModeratorClientManager-Object.
        /// </summary>
        public void ResetInactivity()
        {
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
        }

        /// <summary>
        /// Is automatically triggered after 30 minutes by the _inactivityTimer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SetToInactive(object source, ElapsedEventArgs e)
        {
            ServerLogger.LogDebug($"MC-{ModeratorGuid} was set to inactive.");
            IsInactive = true;
            Stop();
        }



        /// <summary>
        /// Initializes and starts a Timer, which sends an <a cref="AudienceStatusMessage">AudienceStatusMessage</a> every 3 seconds.
        /// </summary>
        public void StartAudienceCountLiveUpdate()
        {
            //FR42 'PlayerAudience-Client count live update'
            //The ServerLogic should inform the Moderator-Client in 3 seconds intervals about the amount of PlayerAudience-Clients connected to the ServerLogic, as long as the game didn't start yet.
            _playerAudienceCountLiveUpdateTimer = new Timer(3000);
            _playerAudienceCountLiveUpdateTimer.Elapsed += SendAudienceCount;
            _playerAudienceCountLiveUpdateTimer.AutoReset = true;
            _playerAudienceCountLiveUpdateTimer.Enabled = true;
        }

        /// <summary>
        /// This method is automatically triggered by the _audienceCountLiveUpdate-Timer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SendAudienceCount(object source, ElapsedEventArgs e)
        {
            _socket.Send(JsonConvert.SerializeObject(new AudienceStatusMessage(this.ModeratorGuid, PAClient.PABackend.ConnectionList[this.SessionKey].Count)));
        }

        /// <summary>
        /// Initializes and starts a Timer, which triggers the sending of a <a cref="VotingEndedMessage">VotingEndedMessage</a> after the time specified in the <a cref="RequestStartVotingMessage">RequestStartVotingMessage</a> time parameter has elapsed 
        /// </summary>
        /// <param name="startVoting"></param>
        public void StartVotingTimer(RequestStartVotingMessage startVoting)
        {
            _playerAudienceClientApi.StartNewVote(SessionKey, startVoting.VotingPrompt, startVoting.VotingOptions);
            //Timer takes milliseconds
            _votingTimer = new Timer(startVoting.VotingTime * 1000);
            _votingTimer.Elapsed += SendVotingResults;
            _votingTimer.AutoReset = false;
            _votingTimer.Enabled = true;
            _currentPrompt = startVoting.VotingPrompt;
            IsVoting = true;
        }

        /// <summary>
        /// Pauses/unpauses the timeout for voting if there is currently one ongoing.
        /// </summary>
        /// <param name="pause"></param>
        public bool PauseVotingTimer(bool pause)
        {
            if (IsVoting)
            {
                IsPaused = pause;
                if (pause)
                {
                    ServerLogger.LogDebug("Game is paused.");
                    _votingTimer.Stop();
                    return true;
                }
                ServerLogger.LogDebug("Game is continued.");
                _votingTimer.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves the results of the voting process from the PA-Client. Determines the prompt with the highest votes and sends a <a cref="VotingEndedMessage">VotingEndedMessage</a>.
        /// Gets automatically triggered by the _votingTimer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SendVotingResults(object source, ElapsedEventArgs e)
        {

            Dictionary<KeyValuePair<Guid, string>, int> votingResults = _playerAudienceClientApi.GetVotingResult(SessionKey, _currentPrompt);
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
            ServerLogger.LogDebug($"Voting ended. Winning prompt is '{winningOption.Value}' with {winningVotes} votes.");
            IsVoting = false;
            _socket.Send(JsonConvert.SerializeObject(new VotingEndedMessage(ModeratorGuid, winningOption, votingResults)));
        }


        /// <summary>
        /// Always call before removing a ModeratorClientAttributes-Object.
        /// Stops all processes that may still be running in the background.
        /// </summary>
        public void Stop()
        {
            try
            {
                StopAudienceCountLiveUpdate();
                _votingTimer.Stop();
                _votingTimer.Dispose();
                _playerAudienceCountLiveUpdateTimer.Stop();
                _playerAudienceCountLiveUpdateTimer.Dispose();
                _socket.Close();
                _inactivityTimer.Stop();
                _inactivityTimer.Dispose();
                ServerLogger.LogDebug($"MC-{ModeratorGuid} was stopped.");
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
            _playerAudienceCountLiveUpdateTimer.Stop();
            _playerAudienceCountLiveUpdateTimer.Dispose();
        }

    }

}