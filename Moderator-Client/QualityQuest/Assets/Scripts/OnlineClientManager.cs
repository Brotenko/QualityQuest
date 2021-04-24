using System;
using System.Collections.Generic;
using System.Linq;
using MessageContainer;
using UnityEngine;
using MessageContainer.Messages;
using TMPro;
using UnityEngine.UI;
using WebSocketSharp;
using Random = System.Random;

public class OnlineClientManager : MonoBehaviour
{

    public QualityQuestWebSocket qualityQuestWebSocket;
    public ActiveScreenManager activeScreen;
    public GameStory story;
    public DisplayStatusbar statusBar;
    public DisplayDecision decision;
    public DisplayStoryFlow storyFlow;
    public Result result;
    public DisplayStatistics displayStatistics;
    public VideoBackground video;
    public OfflineGameManager offlineGameManager;
    public VotingStatistics votingStatistics;

    public int votingTime;
    public int debugVotingTime;

    public TMP_InputField ip;
    public TMP_InputField port;
    public TMP_InputField password;

    public Button storyFlowButton;
    public Button selectOnlineA;
    public Button selectOnlineB;
    public Button selectOnlineC;
    public Button selectOnlineD;

    private string sessionKey;
    private string url;
    private Guid moderatorClientGuid;

    
    public void StartOnlineMode()
    {
        votingTime = 20;
        debugVotingTime = 1;
        moderatorClientGuid = Guid.NewGuid();
        votingStatistics ??= new VotingStatistics(new List<VotingResult>());
    }

    public void Connect()
    {
        
        if (ip.text != "" && port.text != "" && password.text != "")
        {
            qualityQuestWebSocket.StartConnection(ip.text, port.text);
        }
        else
        {
            activeScreen.ShowErrorScreen("Ip, Port oder Passwort fehlt. Bitte neu versuchen oder im Offline-Modus fortfahren.");
        } 
        //qualityQuestWebSocket.StartConnection("127.0.0.1", "8181");
    }

    public void ConnectionEstablished()
    {
        // Open new session
        if (sessionKey == null)
        {
            SendRequestSessionOpenedMessage();
        }
        else
        {
            // TODO: Reconnect to old session
            // Stay offline until reconnect
        }
    }

    public void SendRequestSessionOpenedMessage()
    {
        var requestOpenSessionMessage = new RequestOpenSessionMessage(moderatorClientGuid, password.text);
        // for testing with a default password
        //var requestOpenSessionMessage = new RequestOpenSessionMessage(moderatorClientGuid, "!Password123#");
        qualityQuestWebSocket.SendMessage(requestOpenSessionMessage);
    }

    public void SendRequestGameStartMessage()
    {
        Debug.Log("RequestGameStartMessage");
        var requestGameStartMessage = new RequestGameStartMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(requestGameStartMessage);
    }

    public void ReceivedReconnectSuccessfulMessage(ReconnectSuccessfulMessage reconnectSuccessfulMessage)
    {
        GameState.gameIsOnline = true;
        ContinueOnlineStory(story.playThrough.CurrentEvent);
    }

    

    public void ReceivedSessionOpenedMessage(SessionOpenedMessage sessionOpenedMessage)
    {
        GameState.gameIsOnline = true;
        url = sessionOpenedMessage.DirectURL.ToString();
        sessionKey = sessionOpenedMessage.SessionKey;
        activeScreen.ShowQrCodePanel(sessionOpenedMessage.DirectURL.ToString(), sessionOpenedMessage.SessionKey);
    }

    public void ReceivedAudienceStatusMessage(AudienceStatusMessage audienceStatusMessage)
    {
        activeScreen.UpdateAudienceCount(audienceStatusMessage.AudienceCount);
    }

    public void ReceivedGameStartedMessage(GameStartedMessage gameStartedMessage)
    {
        ContinueOnlineStory(story.playThrough.CurrentEvent);
    }

    public void ReceivedVotingEndedMessage(VotingEndedMessage votingEndedMessage)
    {
        var currentEvent = story.playThrough.CurrentEvent;
        try
        {
            ValidateVotingEndedMessage(currentEvent, votingEndedMessage.VotingResults);
            SaveStatistics(currentEvent.Description, currentEvent.Children, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes);

            var currentEventChildren = story.playThrough.CurrentEvent.Children.ToList();

            if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
            {
                OnlineModePickInitializeChar(currentEvent, currentEventChildren, votingEndedMessage);
            }
            else
            {
                // Activate results screen and loads the results. Activates click listeners for option one and two.
                if (currentEventChildren.Count >= 2)
                {
                    activeScreen.ShowResults();
                    result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults,
                        votingEndedMessage.TotalVotes);
                    selectOnlineA.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[0]); });
                    selectOnlineB.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[1]); });
                }
                // Activates the click listener for option 3
                if (currentEventChildren.Count >= 3)
                {
                    selectOnlineC.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[2]); });
                }
                // Activates the click listener for option 4
                if (currentEventChildren.Count >= 4)
                {
                    selectOnlineD.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[3]); });
                }
            }
        }
        catch (WrongVotingEndedMessage e)
        {
            Debug.Log("Exception :" + e);
            Debug.Log("Switch to offline mode.");
            SwitchModes();
        }
    }

    private void OnlineModePickInitializeChar(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, VotingEndedMessage votingEndedMessage)
    {
        activeScreen.ShowResults();
        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes);
        selectOnlineA.onClick.AddListener(delegate
        { 
            offlineGameManager.PickNoruso(); 
            ContinueOnlineStory(currentEventChildren[0]);
        }); 
        selectOnlineB.onClick.AddListener(delegate 
        { 
            offlineGameManager.PickLumati(); 
            ContinueOnlineStory(currentEventChildren[1]);
        });
        selectOnlineC.onClick.AddListener(delegate 
        { 
            offlineGameManager.PickTurgal(); 
            ContinueOnlineStory(currentEventChildren[2]);
        }); 
        selectOnlineD.onClick.AddListener(delegate 
        { 
            offlineGameManager.PickKirogh(); 
            ContinueOnlineStory(currentEventChildren[3]);
        });
    }

    private void SaveStatistics(string votingPrompt, HashSet<StoryEvent> votingOptions, Dictionary<Guid, int> votingResults, int totalVotes)
    {
        var voting = new VotingResult(votingPrompt, totalVotes, new Dictionary<string, int>());

        foreach (var storyEvent in votingOptions)
        {
            voting.VotingOptions.Add(storyEvent.Description, votingResults[storyEvent.EventId]);
        }
        votingStatistics.Statistic.Add(voting);
    }

    public void ValidateVotingEndedMessage(StoryEvent currentEvent, Dictionary<Guid, int> votingOptions)
    {
        if (currentEvent.Children.Any(child => !votingOptions.ContainsKey(child.EventId)))
        {
            throw new WrongVotingEndedMessage("The VotingEndedMessage does not match the current StoryEvent.");
        }
    }

    public void ContinueOnlineStory(StoryEvent storyEvent)
    {
        RemoveListeners();
        story.SetCurrentEvent(storyEvent);
        
        if (storyEvent.SkillChange != null)
        {
            story.playThrough.Character.Abilities.updateAbilities(storyEvent.SkillChange);
            statusBar.DisplaySkills(story.playThrough.Character.Abilities);
            statusBar.UpdateSkillChanges(storyEvent.SkillChange);
        }

        switch (storyEvent.StoryType)
        {
            case StoryEventType.StoryBackground:
                ContinueOnlineBackground(storyEvent);
                break;
            case StoryEventType.StoryRootEvent:
                // FALL THROUGH
            case StoryEventType.StoryDecision:
                ContinueStoryDecision(storyEvent);
                break;
            case StoryEventType.StoryDecisionOption:
                ContinueDecisionOption(storyEvent);
                break;
            case StoryEventType.StoryFlow:
                ContinueStoryFlow(storyEvent);
                break;
            default:
                Debug.Log("StoryEventType is not valid.");
                break;
        }
    }

    private void ContinueOnlineBackground(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Any())
        {
            video.SwitchBackground(currentEvent.Background);
            ContinueOnlineStory(currentEvent.Children.First());
        }
        else
        {
            Debug.Log("Story Event has no Children");
        }
    }

    private void ContinueDecisionOption(StoryEvent currentEvent)
    {
        switch (currentEvent.Children.Count())
        {
            case 1:
                ContinueOnlineStory(currentEvent.Children.First());
                break;
            default:
                ContinueOnlineStory(story.GetRandomOption(statusBar));
                break;
        }
    }

    private void ContinueStoryDecision(StoryEvent currentEvent)
    {
        if (currentEvent.StoryType.Equals(StoryEventType.StoryDecision))
        {
            activeScreen.ShowDecision();
        }
        else
        {
            activeScreen.ShowCharacterSelection();
        }

        var children = currentEvent.Children.ToList();
        decision.LoadDecision(currentEvent, children);

        var requestStartVotingMessage = new RequestStartVotingMessage(moderatorClientGuid, votingTime, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[currentEvent.Children.Count])
        {
            VotingPrompt = new KeyValuePair<Guid, string>(currentEvent.EventId, currentEvent.Description)
        };

        // start Voting
        for (var i = 0; i < children.Count; i++)
        {
            requestStartVotingMessage.VotingOptions[i] = new KeyValuePair<Guid, string>(children[i].EventId, children[i].Description);
        }
        qualityQuestWebSocket.SendMessage(requestStartVotingMessage);
    }

    private void ContinueStoryFlow(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Count > 0)
        {
            activeScreen.ShowStoryFlow();
            storyFlow.SetStoryFlow(currentEvent);
            storyFlowButton.onClick.AddListener(delegate { ContinueOnlineStory(currentEvent.Children.First()); });
        }
        else
        {
            storyFlowButton.onClick.AddListener(delegate
            {
                SendRequestCloseSessionMessage();
                activeScreen.ShowStatistics();
                displayStatistics.DisplayAllDescisions(votingStatistics);
            });
        }
    }

    public void SendRequestCloseSessionMessage()
    {
        if (sessionKey == null) return;
        var requestCloseSessionMessage = new RequestCloseSessionMessage(moderatorClientGuid, sessionKey);
        qualityQuestWebSocket.SendMessage(requestCloseSessionMessage);
    }

    void RemoveListeners()
    {
        storyFlowButton.onClick.RemoveAllListeners();
        selectOnlineA.onClick.RemoveAllListeners();
        selectOnlineB.onClick.RemoveAllListeners();
        selectOnlineC.onClick.RemoveAllListeners();
        selectOnlineD.onClick.RemoveAllListeners();
    }


    public void ReceivedVotingStartedMessage(VotingStartedMessage votingStartedMessage)
    {
        statusBar.DisplayTimer(votingTime);
    }

    public void RequestGamePause()
    {
        if (!ActiveScreenManager.paused)
        {
            var requestGamePausedStatusChangeMessage = new RequestGamePausedStatusChangeMessage(moderatorClientGuid, true);
            qualityQuestWebSocket.SendMessage(requestGamePausedStatusChangeMessage);
        }
        else
        {
            var requestGamePausedStatusChangeMessage = new RequestGamePausedStatusChangeMessage(moderatorClientGuid, false);
            qualityQuestWebSocket.SendMessage(requestGamePausedStatusChangeMessage);
        }
    }

    public void ReceivedGamePausedStatusChange(GamePausedStatusMessage gamePausedStatusMessage)
    {
        if (gamePausedStatusMessage.GamePaused)
        {
            ActiveScreenManager.paused = false;
            activeScreen.ShowPauseMenu(url, sessionKey);
        }
        else
        {
            ActiveScreenManager.paused = true;
            activeScreen.ShowPauseMenu(url, sessionKey);
        }
    }


    public void ServerIssues(int errorCode)
    {
        moderatorClientGuid = Guid.NewGuid();
        switch (errorCode)
        {
            case 1006:
                activeScreen.ShowErrorScreen("Es konnte keine Verbindung zum Server aufgebaut werden.");
                break;
            default:
                break;
        }
    }

    public void ReceivedErrorMessage(ErrorMessage errorMessage)
    {
        switch (errorMessage.ErrorMessageType)
        {
            case ErrorType.IllegalMessage:
                break;
            case ErrorType.IllegalPauseAction:
                break;
            case ErrorType.GuidAlreadyExists:
                break;
            case ErrorType.UnknownGuid:
                break;
            case ErrorType.WrongPassword:
                activeScreen.ShowErrorScreen("Passwort ist falsch. Bitte erneut versuchen oder im Offline-Modus fortfahren.");
                break;
            case ErrorType.WrongSession:
                break;
            default:
                break;
        }
    }

    public void SendReconnectMessage()
    {
        var reconnectMessage = new ReconnectMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(reconnectMessage);
    }


    public void SwitchModes()
    {
        
        if (GameState.gameIsOnline)
        {
            GameState.gameIsOnline = false;
            offlineGameManager.ContinueOfflineStory(story.playThrough.CurrentEvent);
        }
        else
        {
            decision.RemoveOfflineDecisionListeners();
            offlineGameManager.characterSelection.RemoveOfflinePickButtons();
            if (qualityQuestWebSocket.webSocket == null)
            {
                activeScreen.ShowConnection();
            }
            else
            {
                switch (qualityQuestWebSocket.webSocket.ReadyState)
                {
                    case WebSocketState.Closed:
                        activeScreen.ShowConnection();
                        break;
                    case WebSocketState.Open:
                        if (sessionKey == null)
                        {
                            SendRequestSessionOpenedMessage();
                        }
                        else
                        {
                            GameState.gameIsOnline = true;
                            ContinueOnlineStory(story.playThrough.CurrentEvent);
                        }
                        break;
                    case WebSocketState.Closing:
                        break;
                    case WebSocketState.Connecting:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
