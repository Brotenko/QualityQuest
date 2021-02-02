using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient
{
    public struct VotingResults
    {
        /*
         *                                             All Voting Results
         *    |------------------------------------------------------------------------------------------------------|
         *                                                 Session-related votes
         *               |-------------------------------------------------------------------------------------------| 
         *               
         *                                                       Prompt-related voting-options
         *                                  |-----------------------------------------------------------------------|
         *                                                                               Option-related votes
         *                                                                         |-------------------------------|
         *              sessionkeys                  prompts                                 options           votes
         *               |------|           |-------------------------|            |-------------------------| |---|
         */
        public Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>> data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public VotingResults(Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>> x)
        {
            data = x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetSessionKeys()
        {
            return data.Keys.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public void AddSessionKey(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (!IsSessionActive(sessionkey))
            {
                data.Add(sessionkey, new Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>());
            }
            else
            {
                throw new ArgumentException(message: "A session with that key is already registered.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        public void AddNewPoll(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }
            if (prompt.Value == null)
            {
                throw new ArgumentNullException("The prompt's description can not be null.");
            }
            if (options == null)
            {
                throw new ArgumentNullException("The options can not be null.");
            }
            foreach (KeyValuePair<Guid, string> pair in options)
            {
                if (pair.Value == null)
                {
                    throw new ArgumentNullException("The option's description can not be null.");
                }
            }

            if (IsSessionActive(sessionkey))
            {
                if (!this.GetPromptsBySession(sessionkey).Contains(prompt))
                {
                    Dictionary<KeyValuePair<Guid, string>, int> tempDict = new Dictionary<KeyValuePair<Guid, string>, int>();
                    foreach (KeyValuePair<Guid, string> o in options)
                    {
                        tempDict.Add(o, 0);
                    }

                    data.GetValueOrDefault(sessionkey).Add(prompt, tempDict);
                }
                else
                {
                    throw new ArgumentException(message: "A poll with the given Guid has already been held.");
                }
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="option"></param>
        public void AddVote(string sessionkey, Guid prompt, Guid option)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                if (this.GetPromptGuidsBySession(sessionkey).Contains(prompt))
                {
                    Dictionary<KeyValuePair<Guid, string>, int> possibleChoices = this.GetOptionsVotesPairsByPrompt(sessionkey, prompt);

                    foreach (KeyValuePair<Guid, string> options in possibleChoices.Keys)
                    {
                        if (option == options.Key)
                        {
                            possibleChoices[options] += 1;
                            return;
                        }
                    }
                    throw new ArgumentException(message: "The transmitted option is invalid.");
                }
                else
                {
                    throw new ArgumentException(message: "The transmitted prompt is either invalid or belongs to another session.");
                }
            }
            else
            {
                throw new SessionNotFoundException(message: "The transmitted session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public void RemoveSession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                data.Remove(sessionkey);
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public KeyValuePair<Guid, string>[] GetPromptsBySession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                foreach (KeyValuePair<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>> entry in this.data)
                {
                    if (sessionkey == entry.Key)
                    {
                        return entry.Value.Keys.ToArray();
                    }
                }

                throw new ArgumentException(message: "This statement should be unreachable!");
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> GetStatistics(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                return data.GetValueOrDefault(sessionkey);
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public Guid[] GetPromptGuidsBySession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                List<Guid> prompts = new List<Guid>();
                KeyValuePair<Guid, string>[] tempPrompts = GetPromptsBySession(sessionkey);

                if (tempPrompts != null)
                {
                    foreach (KeyValuePair<Guid, string> prompt in tempPrompts)
                    {
                        prompts.Add(prompt.Key);
                    }
                    return prompts.ToArray();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public string[] GetPromptStringsBySession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                List<string> prompts = new List<string>();
                KeyValuePair<Guid, string>[] tempPrompts = GetPromptsBySession(sessionkey);

                if (tempPrompts != null)
                {
                    foreach (KeyValuePair<Guid, string> prompt in tempPrompts)
                    {
                        prompts.Add(prompt.Value);
                    }
                    return prompts.ToArray();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetOptionsVotesPairsByPrompt(string sessionkey, Guid prompt)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                foreach (KeyValuePair<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>> session in data)
                {
                    if (sessionkey == session.Key)
                    {
                        foreach (KeyValuePair<Guid, string> tempPrompt in session.Value.Keys)
                        {
                            if (prompt == tempPrompt.Key)
                            {
                                foreach (Dictionary<KeyValuePair<Guid, string>, int> optionsVotesPairs in session.Value.Values)
                                {
                                    return optionsVotesPairs;
                                }
                            }
                        }
                    }
                }

                return null;
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public KeyValuePair<Guid, string>[] GetOptionsByPrompt(string sessionkey, Guid prompt)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                List<KeyValuePair<Guid, string>> options = new List<KeyValuePair<Guid, string>>();
                Dictionary<KeyValuePair<Guid, string>, int> optionsVotesPairs = GetOptionsVotesPairsByPrompt(sessionkey, prompt);

                if (optionsVotesPairs != null)
                {
                    foreach (KeyValuePair<Guid, string> option in optionsVotesPairs.Keys)
                    {
                        options.Add(option);
                    }
                }

                return options.ToArray();
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public string[] GetOptionStringsByPrompt(string sessionkey, Guid prompt)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                List<string> options = new List<string>();
                KeyValuePair<Guid, string>[] tempOptions = GetOptionsByPrompt(sessionkey, prompt);

                if (tempOptions.Count() != 0)
                {
                    foreach (KeyValuePair<Guid, string> option in tempOptions)
                    {
                        options.Add(option.Value);
                    }
                    return options.ToArray();
                }

                return null;
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Guid[] GetOptionGuidsByPrompt(string sessionkey, Guid prompt)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                List<Guid> options = new List<Guid>();
                KeyValuePair<Guid, string>[] tempOptions = GetOptionsByPrompt(sessionkey, prompt);

                if (tempOptions.Count() != 0)
                {
                    foreach (KeyValuePair<Guid, string> option in tempOptions)
                    {
                        options.Add(option.Key);
                    }
                    return options.ToArray();
                }

                return null;
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public int GetVotesByOption(string sessionkey, Guid prompt, Guid option)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                Dictionary<KeyValuePair<Guid, string>, int> optionsVotesPairs = GetOptionsVotesPairsByPrompt(sessionkey, prompt);

                if (optionsVotesPairs != null)
                {
                    foreach (KeyValuePair<Guid, string> options in optionsVotesPairs.Keys)
                    {
                        if (option == options.Key)
                        {
                            return optionsVotesPairs.GetValueOrDefault(options);
                        }
                    }
                    throw new ArgumentException(message: "The requested option is not part of this prompt.");
                }

                throw new ArgumentException(message: "The requested prompt is not part of this session.");
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = "VotingResults:\n";

            foreach (string key in this.GetSessionKeys())
            {
                ret += " - " + key + ":\n";

                foreach (string prompt in this.GetPromptStringsBySession(key))
                {
                    ret += "   - " + prompt + " (" + GetPromptGuidsBySession(key)[0].ToString() + "):\n";

                    foreach (string option in this.GetOptionStringsByPrompt(key, GetPromptGuidsBySession(key)[0]))
                    {
                        ret += "     - " + option + " (" + GetOptionGuidsByPrompt(key, GetPromptGuidsBySession(key)[0])[0].ToString() + "): " + this.GetVotesByOption(key, GetPromptGuidsBySession(key)[0], GetOptionGuidsByPrompt(key, GetPromptGuidsBySession(key)[0])[0]) + "\n";
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        private bool IsSessionActive(string sessionkey)
        {
            return GetSessionKeys().Contains(sessionkey);
        }
    }
}
