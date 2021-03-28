using System;
using System.Collections.Generic;
using System.Timers;
using Fleck;
using Newtonsoft.Json;
using ServerLogic.Model.Messages;

namespace ServerLogic.Control
{
    public partial class ModeratorClientManager
    {
        private class ModeratorClientAttributesHelperClass
        {
            public Guid moderatorGuid;
            public string sessionkey;
            public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> statistics;
            //FR31 'Network protocol violation', connection is canceled after three strikes/violations against network-protocol.
            public int strikes;
            public Timer PlayerAudienceCountLiveTimer;
            private IWebSocketConnection _socket;

            public ModeratorClientAttributesHelperClass(Guid moderatorGuid, string sessionkey, IWebSocketConnection socket)
            {
                this.moderatorGuid = moderatorGuid;
                this.sessionkey = sessionkey;
                this.statistics = new Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>();
                this.strikes = 0;
                this._socket = socket;
            }


            /// <summary>
            /// 
            /// </summary>
            public void StartAudienceCountLiveUpdate()
            {
                //TODO check that for every case of interruption of connection it is checked that every timer is disposed
                //FR42 'PlayerAudience-Client count live update'
                //The ServerLogic should inform the Moderator-Client in 3 seconds intervals about the amount of PlayerAudience-Clients connected to the ServerLogic,
                //as long as the game didn't start yet.
                Timer audienceCountLiveUpdateTimer = new Timer(3000);
                audienceCountLiveUpdateTimer.Elapsed += SendAudienceCount;
                audienceCountLiveUpdateTimer.AutoReset = true;
                audienceCountLiveUpdateTimer.Enabled = true;
            }

            private void SendAudienceCount(Object source, ElapsedEventArgs e)
            {
                _socket.Send(JsonConvert.SerializeObject(new AudienceStatusMessage(this.moderatorGuid, 2 /*todo*/)));
            }

            /// <summary>
            /// Always call when removing a ModeratorClientAttributes-Object.
            /// Stops all processes that may still be running in the background.
            /// </summary>
            public void Stop()
            {
                try
                {
                    PlayerAudienceCountLiveTimer.Stop();
                    PlayerAudienceCountLiveTimer.Close();
                    _socket.Close();
                }
                catch (Exception e)
                {
                    //Closing a already stopped process might cause an exception.
                }
            }

        }
    }
}