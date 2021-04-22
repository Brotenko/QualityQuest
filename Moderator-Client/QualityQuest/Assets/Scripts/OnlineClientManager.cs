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
        //qualityQuestWebSocket.StartConnection("127.0.0.1", 8181);
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
        SwitchModes();
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
        var currentEvent = story.playThrough.CurrentEvent;
        if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
        {
            activeScreen.ShowCharacterSelection();
            var requestStartVotingMessage = new RequestStartVotingMessage(moderatorClientGuid, votingTime, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[currentEvent.Children.Count]);
            requestStartVotingMessage.VotingPrompt = new KeyValuePair<Guid, string>(currentEvent.EventId, currentEvent.Description);

            // start Voting for the character
            var options = currentEvent.Children.ToArray();
            for (var i = 0; i < options.Length; i++)
            {
                requestStartVotingMessage.VotingOptions[i] = new KeyValuePair<Guid, string>(options[i].EventId, options[i].Description);
            }
            qualityQuestWebSocket.SendMessage(requestStartVotingMessage);
        }
        else
        {
            ContinueOnlineStory(currentEvent);
        }
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
                switch (currentEventChildren.Count)
                {
                    case 2:
                        activeScreen.ShowResults();
                        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults,
                            votingEndedMessage.TotalVotes);
                        selectOnlineA.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[0]); });
                        selectOnlineB.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[1]); });
                        break;
                    case 3:
                        activeScreen.ShowResults();
                        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults,
                            votingEndedMessage.TotalVotes);
                        selectOnlineA.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[0]); });
                        selectOnlineB.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[1]); });
                        selectOnlineC.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[2]); });
                        break;
                    case 4:
                        activeScreen.ShowResults();
                        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults,
                            votingEndedMessage.TotalVotes);
                        selectOnlineA.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[0]); });
                        selectOnlineB.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[1]); });
                        selectOnlineC.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[2]); });
                        selectOnlineD.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[3]); });
                        break;
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

    void OnlineModePickInitializeChar(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, VotingEndedMessage votingEndedMessage)
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

    void SaveStatistics(string votingPrompt, HashSet<StoryEvent> votingOptions, Dictionary<Guid, int> votingResults, int totalVotes)
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
        foreach (var child in currentEvent.Children)
        {
            if (!votingOptions.ContainsKey(child.EventId))
            {
                throw new WrongVotingEndedMessage("The VotingEndedMessage does not match the current StoryEvent.");
            }
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
                ContinueStoryDecision();
                break;
            case StoryEventType.StoryDecisionOption:
                ContinueDecisionOption();
                break;
            case StoryEventType.StoryFlow:
                ContinueStoryFlow();
                break;
        }
    }

    void ContinueOnlineBackground(StoryEvent currentEvent)
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

    void ContinueDecisionOption()
    {
        if (story.playThrough.CurrentEvent.Children.Count() == 1)
        {
            ContinueOnlineStory(story.playThrough.CurrentEvent.Children.First());
        } 
        else if (story.playThrough.CurrentEvent.Children.Count() > 1)
        {
            statusBar.DisplayDice(3);
            Random diceRoll = new Random();
            int rollTheDice = diceRoll.Next(0, 6);
            var children = story.playThrough.CurrentEvent.Children.ToList();
            switch (story.playThrough.CurrentEvent.Children.First().Random)
            {
                case RandomType.DecisionFiveOne:
                    bool randomEventOne = rollTheDice + story.playThrough.Character.Abilities.Programming + 1 > 8;
                    if (randomEventOne == children[0].RandomOption)
                    {
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionFiveTwo:
                    var randomEventTwo = rollTheDice + story.playThrough.Character.Abilities.Programming - 1 > 8;
                    if (randomEventTwo == children[0].RandomOption)
                    {
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionEight:
                    var randomEventThree = rollTheDice + story.playThrough.Character.Abilities.Partying > 8;
                    Debug.Log("Test hier, randomBool: " + randomEventThree);
                    if (randomEventThree == children[0].RandomOption)
                    {
                        Debug.Log(children[0].RandomOption);
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        Debug.Log(children[1].RandomOption);
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionEleven:
                    var randomEventFour = rollTheDice > 3;
                    if (randomEventFour == children[0].RandomOption)
                    {
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionThirteenOne:
                    var randomEventFive = rollTheDice <= 3;
                    if (randomEventFive == children[0].RandomOption)
                    {
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionThirteenTwo:
                    var randomEventSix = story.playThrough.Character.Abilities.Communication > 6;
                    if (randomEventSix == children[0].RandomOption)
                    {
                        ContinueOnlineStory(children[0]);
                    }
                    else
                    {
                        ContinueOnlineStory(children[1]);
                    }
                    break;
                default:
                    ContinueOnlineStory(story.playThrough.CurrentEvent.Children.First());
                    break;
            }
        }
    }

    void ContinueStoryDecision()
    {
        var children = story.playThrough.CurrentEvent.Children.ToList();

        decision.LoadOnlineDecision(story.playThrough.CurrentEvent, children);
        activeScreen.ShowDecision();

        var requestStartVotingMessage = new RequestStartVotingMessage(moderatorClientGuid, votingTime, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[story.playThrough.CurrentEvent.Children.Count]);
        requestStartVotingMessage.VotingPrompt = new KeyValuePair<Guid, string>(story.playThrough.CurrentEvent.EventId, story.playThrough.CurrentEvent.Description);

        // start Voting
        var options = story.playThrough.CurrentEvent.Children.ToArray();
        for (var i = 0; i < options.Length; i++)
        {
            requestStartVotingMessage.VotingOptions[i] = new KeyValuePair<Guid, string>(options[i].EventId, options[i].Description);
        }
        qualityQuestWebSocket.SendMessage(requestStartVotingMessage);
    }

    void ContinueStoryFlow()
    {
        if (story.playThrough.CurrentEvent.Children.Count > 0)
        {
            activeScreen.ShowStoryFlow();
            storyFlow.SetStoryFlow(story.playThrough.CurrentEvent);
            storyFlowButton.onClick.AddListener(delegate { ContinueOnlineStory(story.playThrough.CurrentEvent.Children.First()); });
        }
        else
        {
            storyFlowButton.onClick.AddListener(delegate { LoadStatistics(); });
        }
    }

    void LoadStatistics()
    {
        activeScreen.ShowStatistics();
        displayStatistics.DisplayAllDescisions(votingStatistics);
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
            GameState.gameIsOnline = true;
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
                            SendReconnectMessage();
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
