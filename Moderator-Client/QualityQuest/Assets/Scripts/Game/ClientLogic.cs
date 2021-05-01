using System;
using System.Collections.Generic;
using System.Linq;
using MessageContainer.Messages;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// Class to split the ClientLogic from the MonoBehavior script to achieve a better testability.
/// More info: 
/// </summary>
public class ClientLogic
{
    public int VotingTime { get; set; }
    public bool ActiveVoting { get; set; }
    public string SessionKey { get; set; }
    public string Url { get; set; }

    public StoryGraph StoryGraph { get; }

    public VotingStatistics VotingStatistic { get; set; }
    public Guid ModeratorClientGuid { get; set; }

    public ClientLogic(int votingTime)
    {
        VotingTime = votingTime;
        ActiveVoting = false;
        VotingStatistic = new VotingStatistics(new List<VotingResult>());
        ModeratorClientGuid = Guid.NewGuid();
        StoryGraph = new StoryGraph();
    }

    public void SetNewModeratorClientGuid()
    {
        ModeratorClientGuid = Guid.NewGuid();
    }

    public RequestStartVotingMessage InitializeRequestStartVotingMessage(StoryEvent currentEvent)
    {
        var children = currentEvent.Children.ToList();

        var requestStartVotingMessage = new RequestStartVotingMessage(ModeratorClientGuid, VotingTime, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[currentEvent.Children.Count])
        {
            VotingPrompt = new KeyValuePair<Guid, string>(currentEvent.EventId, currentEvent.Description)
        };

        // start Voting
        for (var i = 0; i < children.Count; i++)
        {
            requestStartVotingMessage.VotingOptions[i] = new KeyValuePair<Guid, string>(children[i].EventId, children[i].Description);
        }

        return requestStartVotingMessage;
    }

    public ReconnectMessage InitializeReconnectMessage()
    {
        var reconnectMessage = new ReconnectMessage(ModeratorClientGuid);
        return reconnectMessage;
    }

    public RequestOpenSessionMessage InitializeRequestOpenSessionMessage(string password)
    {
        var requestOpenSessionMessage = new RequestOpenSessionMessage(ModeratorClientGuid, "!Password123#");
        return requestOpenSessionMessage;
    }

    public RequestGameStartMessage InitializeRequestGameStartMessage()
    {
        var requestGameStartMessage = new RequestGameStartMessage(ModeratorClientGuid);
        return requestGameStartMessage;
    }

    public void SaveUrlAndSessionKey(SessionOpenedMessage sessionOpenedMessage)
    {
        SessionKey = sessionOpenedMessage.SessionKey;
        Url = sessionOpenedMessage.DirectURL.ToString();
    }

    public RequestCloseSessionMessage InitializeRequestCloseSession()
    {
        var requestCloseSessionMessage = new RequestCloseSessionMessage(ModeratorClientGuid, SessionKey);
        return requestCloseSessionMessage;
    }

    public RequestGamePausedStatusChangeMessage InitializeRequestGamePausedStatusChangeMessage(bool pause)
    {
        var requestGamePausedStatusChangeMessage = new RequestGamePausedStatusChangeMessage(ModeratorClientGuid, pause);
        return requestGamePausedStatusChangeMessage;
    }

    /// <summary>
    /// Method to save the voting statistic.
    /// </summary>
    /// <param name="votingPrompt">The current votingPrompt.</param>
    /// <param name="votingOptions">The votingOptions of the current votingPrompt.</param>
    /// <param name="votingResults">The result of the voting.</param>
    /// <param name="totalVotes">The total number of votes.</param>
    public void SaveStatistics(string votingPrompt, HashSet<StoryEvent> votingOptions, Dictionary<Guid, int> votingResults, int totalVotes)
    {
        var voting = new VotingResult(votingPrompt, totalVotes, new Dictionary<string, int>());

        foreach (var storyEvent in votingOptions)
        {
            voting.VotingOptions.Add(storyEvent.Description, votingResults[storyEvent.EventId]);
        }
        VotingStatistic.Statistic.Add(voting);
    }

    /// <summary>
    /// Method to continue the story with a StoryFlowDecisionOption.
    /// If the StoryEvent is a RandomEvent, the story will be continued with a random StoryEvent.
    /// Continues the story in offline or online mode, depending on the state of the game.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
    public StoryEvent ContinueDecision(StoryGraph storyGraph)
    {
        switch (storyGraph.CurrentEvent.Children.Count)
        {
            case 1:
                return storyGraph.CurrentEvent.Children.First();
            default:
                return storyGraph.GetRandomOption();
        }
    }
}
