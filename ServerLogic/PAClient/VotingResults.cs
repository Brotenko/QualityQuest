using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Initializes a new instance of the <see cref="VotingResults"/> class.
        /// </summary>
        /// 
        /// <param name="x">The internal datatype.</param>
        public VotingResults(Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>> x)
        {
            data = x;
        }

        /// <summary>
        /// Retrieves all sessionkeys that correspond to currently active sessions.
        /// </summary>
        /// 
        /// <returns>All sessionkeys that correspond to currently active sessions.</returns>
        public string[] GetSessionKeys()
        {
            return data.Keys.ToArray();
        }

        /// <summary>
        /// Adds a new sessionkey to the VotingResults.
        /// </summary>
        /// 
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// 
        /// <param name="sessionkey">The to be added sessionkey.</param>
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
        /// Adds a new poll for a specific session, with the given prompt and voting options.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The session which the poll belongs to.</param>
        /// 
        /// <param name="prompt">The prompt of the vote.</param>
        /// 
        /// <param name="options">The voting options of the prompt.</param>
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
        /// Adds a new poll for a specific session, with the given prompt and voting options.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The session which the poll belongs to.</param>
        /// 
        /// <param name="prompt">The prompt of the vote.</param>
        /// 
        /// <param name="options">The voting options of the prompt.</param>
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
        /// Removes a sessionkey and all related data from the VotingResults.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The to be removed sessionkey.</param>
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
        /// Retrieves all prompts related to the given sessionkey.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the prompts are to be retrieved.</param>
        /// 
        /// <returns>All prompts related to the given sessionkey. If a given sessionkey
        /// has no prompts associated with it, an empty array is returned.</returns>
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
        /// Retrieves all prompt's GUIDs related to the given sessionkey.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the prompt's GUIDs are to be retrieved.</param>
        /// 
        /// <returns>All prompt's GUIDs related to the given sessionkey. If a given sessionkey
        /// has no prompts associated with it, null is returned.</returns>
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
        /// Retrieves all prompt's strings related to the given sessionkey.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the prompt's strings are to be retrieved.</param>
        /// 
        /// <returns>All prompt's strings related to the given sessionkey. If a given sessionkey
        /// has no prompts associated with it, null is returned.</returns>
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
        /// Retrieves all options-votes pairs related to the given sessionkey and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the options-votes pairs are to be retrieved.</param>
        /// 
        /// <param name="prompt">The prompt for which the options-votes pairs are to be retrieved.</param>
        /// 
        /// <returns>All options-votes pairs related to the given sessionkey and prompt. If a given prompt
        /// has no options-votes pairs associated with it, null is returned.</returns>
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
        /// Retrieves all voting options related to the given sessionkey and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the voting options are to be retrieved.</param>
        /// 
        /// <param name="prompt">The prompt for which the voting options are to be retrieved.</param>
        /// 
        /// <returns>All voting options related to the given sessionkey and prompt. If a given prompt
        /// has no options associated with it, an empty array is returned.</returns>
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
        /// Retrieves all voting option's strings related to the given sessionkey and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the voting option's strings are to be retrieved.</param>
        /// 
        /// <param name="prompt">The prompt for which the voting option's strings are to be retrieved.</param>
        /// 
        /// <returns>All voting option's strings related to the given sessionkey and prompt. If a given prompt
        /// has no options associated with it, null is returned.</returns>
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
        /// Retrieves all voting option's GUIDs related to the given sessionkey and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the voting option's GUIDs are to be retrieved.</param>
        /// 
        /// <param name="prompt">The prompt for which the voting option's GUIDs are to be retrieved.</param>
        /// 
        /// <returns>All voting option's GUIDs related to the given sessionkey and prompt. If a given prompt
        /// has no options associated with it, null is returned.</returns>
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
        /// Retrieves all voting option specific votes related to the given sessionkey and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the votes are to be retrieved.</param>
        /// 
        /// <param name="prompt">The prompt for which the votes are to be retrieved.</param>
        /// 
        /// <param name="option">The voting option for which the votes are to be retrieved.</param>
        /// 
        /// <returns>All voting option specific votes related to the given sessionkey and prompt.</returns>
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
        /// Retrieves the statistics, containing prompts, voting options and number of votes, for a specific
        /// sessionkey.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey for which the statistics is to be retrieved.</param>
        /// 
        /// <returns>The statistics, containing prompts, voting options and number of votes.</returns>
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
        /// ToString-Method of the VotingResults class.
        /// </summary>
        /// 
        /// <returns>String-based visualisation of the VotingResults.</returns>
        public override string ToString()
        {
            string ret = "VotingResults:\n";

            foreach (string key in this.GetSessionKeys())
            {
                ret += " - " + key + ":\n";
                int i = 0;

                foreach (string prompt in this.GetPromptStringsBySession(key))
                {
                    ret += "   - " + prompt + " (" + GetPromptGuidsBySession(key)[i].ToString() + "):\n";
                    int j = 0;

                    foreach (string option in this.GetOptionStringsByPrompt(key, GetPromptGuidsBySession(key)[i]))
                    {
                        ret += "     - " + option + " (" + GetOptionGuidsByPrompt(key, GetPromptGuidsBySession(key)[i])[j].ToString() + "): " + this.GetVotesByOption(key, GetPromptGuidsBySession(key)[i], GetOptionGuidsByPrompt(key, GetPromptGuidsBySession(key)[i])[j]) + "\n";
                        j++;
                    }

                    i++;
                }
            }

            return ret;
        }

        /// <summary>
        /// Checks if a specific sessionkey is part of the VotingResults.
        /// </summary>
        /// 
        /// <param name="sessionkey">The sessionkey which is to be checked.</param>
        /// 
        /// <returns>If the sessionkey is part of the VotingResults.</returns>
        private bool IsSessionActive(string sessionkey)
        {
            return GetSessionKeys().Contains(sessionkey);
        }
    }
}
