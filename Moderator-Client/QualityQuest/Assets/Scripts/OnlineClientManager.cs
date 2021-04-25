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
    public ActiveScreenManager activeScreenManager;
    public GameStory gameStory;
    public DisplayStatusbar displayStatusBar;
    public DisplayDecision displayDecision;
    public DisplayStoryFlow displayStoryFlow;
    public Result result;
    public DisplayStatistics displayStatistics;
    public VideoBackground videoBackground;
    public VotingStatistics votingStatistics;

    public CharacterSelection characterSelection;

    public int votingTime;
    public int debugVotingTime;

    public TMP_InputField ip;
    public TMP_InputField port;
    public TMP_InputField password;

    private string sessionKey;
    private string url;
    private Guid moderatorClientGuid;

    private void Start()
    {
        if (!GameState.gameIsOnline)
        {
            StartOfflinePlaythrough();
        }
    }

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
            activeScreenManager.ShowErrorScreen("Ip, Port oder Passwort fehlt. Bitte neu versuchen oder im Offline-Modus fortfahren.");
        }
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
            SendReconnectMessage();
        }
    }

    public void SendReconnectMessage()
    {
        var reconnectMessage = new ReconnectMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(reconnectMessage);
    }

    public void SendRequestSessionOpenedMessage()
    {
        var requestOpenSessionMessage = new RequestOpenSessionMessage(moderatorClientGuid, password.text);
        qualityQuestWebSocket.SendMessage(requestOpenSessionMessage);
    }

    public void SendRequestGameStartMessage()
    {
        Debug.Log("RequestGameStartMessage");
        var requestGameStartMessage = new RequestGameStartMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(requestGameStartMessage);
    }

    public void SendRequestCloseSessionMessage()
    {
        if (sessionKey == null) return;
        var requestCloseSessionMessage = new RequestCloseSessionMessage(moderatorClientGuid, sessionKey);
        qualityQuestWebSocket.SendMessage(requestCloseSessionMessage);
    }

    public void ReceivedGamePausedStatusChange(GamePausedStatusMessage gamePausedStatusMessage)
    {
        if (gamePausedStatusMessage.GamePaused)
        {
            ActiveScreenManager.paused = false;
            activeScreenManager.ShowPauseMenu(url, sessionKey);
        }
        else
        {
            ActiveScreenManager.paused = true;
            activeScreenManager.ShowPauseMenu(url, sessionKey);
        }
    }

    public void ReceivedReconnectSuccessfulMessage(ReconnectSuccessfulMessage reconnectSuccessfulMessage)
    {
        GameState.gameIsOnline = true;
        ContinueOnlineStory(gameStory.playThrough.CurrentEvent);
    }

    public void ReceivedSessionOpenedMessage(SessionOpenedMessage sessionOpenedMessage)
    {
        GameState.gameIsOnline = true;
        url = sessionOpenedMessage.DirectURL.ToString();
        sessionKey = sessionOpenedMessage.SessionKey;
        activeScreenManager.ShowQrCodePanel(sessionOpenedMessage.DirectURL.ToString(), sessionOpenedMessage.SessionKey);
    }

    public void ReceivedAudienceStatusMessage(AudienceStatusMessage audienceStatusMessage)
    {
        activeScreenManager.UpdateAudienceCount(audienceStatusMessage.AudienceCount);
    }

    public void ReceivedGameStartedMessage(GameStartedMessage gameStartedMessage)
    {
        ContinueOnlineStory(gameStory.playThrough.CurrentEvent);
    }

    public void ReceivedVotingEndedMessage(VotingEndedMessage votingEndedMessage)
    {
        var currentEvent = gameStory.playThrough.CurrentEvent;
        try
        {
            ValidateVotingEndedMessage(currentEvent, votingEndedMessage.VotingResults);
            SaveStatistics(currentEvent.Description, currentEvent.Children, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes);


            var currentEventChildren = gameStory.playThrough.CurrentEvent.Children.ToList();

            if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
            {
                OnlineModePickInitializeChar(currentEvent, currentEventChildren, votingEndedMessage);
            }
            else
            {
                // Activate results screen and loads the results. Activates click listeners for option one and two.
                if (currentEventChildren.Count >= 2)
                {
                    activeScreenManager.ShowResults();
                    result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults,
                        votingEndedMessage.TotalVotes, votingEndedMessage.WinningOption);
                    displayDecision.selectOnlineA.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[0]); });
                    displayDecision.selectOnlineB.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[1]); });
                }
                // Activates the click listener for option 3
                if (currentEventChildren.Count >= 3)
                {
                    displayDecision.selectOnlineC.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[2]); });
                }
                // Activates the click listener for option 4
                if (currentEventChildren.Count >= 4)
                {
                    displayDecision.selectOnlineD.onClick.AddListener(delegate { ContinueOnlineStory(currentEventChildren[3]); });
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
        activeScreenManager.ShowResults();
        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes, votingEndedMessage.WinningOption);
        displayDecision.selectOnlineA.onClick.AddListener(delegate
        { 
            PickNoruso(); 
            ContinueOnlineStory(currentEventChildren[0]);
        });
        displayDecision.selectOnlineB.onClick.AddListener(delegate 
        { 
            PickLumati(); 
            ContinueOnlineStory(currentEventChildren[1]);
        });
        displayDecision.selectOnlineC.onClick.AddListener(delegate 
        { 
            PickTurgal(); 
            ContinueOnlineStory(currentEventChildren[2]);
        });
        displayDecision.selectOnlineD.onClick.AddListener(delegate 
        { 
            PickKirogh(); 
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
        displayDecision.RemoveOnlineDecisionListeners();
        gameStory.SetCurrentEvent(storyEvent);
        
        if (storyEvent.SkillChange != null)
        {
            gameStory.playThrough.Character.Abilities.updateAbilities(storyEvent.SkillChange);
            displayStatusBar.DisplaySkills(gameStory.playThrough.Character.Abilities);
            displayStatusBar.UpdateSkillChanges(storyEvent.SkillChange);
        }

        switch (storyEvent.StoryType)
        {
            case StoryEventType.StoryBackground:
                ContinueBackground(storyEvent);
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

    private void ContinueDecisionOption(StoryEvent currentEvent)
    {
        switch (currentEvent.Children.Count())
        {
            case 1:
                if (GameState.gameIsOnline)
                {
                    ContinueOnlineStory(currentEvent.Children.First());
                }
                else
                {
                    ContinueOfflineStory(currentEvent.Children.First());
                }
                break;
            default:
                if (GameState.gameIsOnline)
                {
                    ContinueOnlineStory(gameStory.GetRandomOption(displayStatusBar));
                }
                else
                {
                    ContinueOfflineStory(gameStory.GetRandomOption(displayStatusBar));
                }
                break;
        }
    }

    private void ContinueStoryDecision(StoryEvent currentEvent)
    {
        displayDecision.RemoveOnlineDecisionListeners();

        if (currentEvent.StoryType.Equals(StoryEventType.StoryDecision))
        {
            activeScreenManager.ShowDecision();
        }
        else
        {
            activeScreenManager.ShowCharacterSelection();
        }

        var children = currentEvent.Children.ToList();
        displayDecision.LoadDecision(currentEvent, children);

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
        displayStoryFlow.RemoveStoryFlowListeners();
        if (currentEvent.Children.Count > 0)
        {
            activeScreenManager.ShowStoryFlow();
            displayStoryFlow.SetStoryFlow(currentEvent);
            displayStoryFlow.storyFlowButton.onClick.AddListener(delegate
            {
                if (GameState.gameIsOnline)
                {
                    ContinueOnlineStory(currentEvent.Children.First());
                }
                else
                {
                    ContinueOfflineStory(currentEvent.Children.First());
                }
            });
        }
        else
        {
            displayStoryFlow.storyFlowButton.onClick.AddListener(delegate
            {
                if (GameState.gameIsOnline)
                {
                    SendRequestCloseSessionMessage();
                    activeScreenManager.ShowStatistics();
                    displayStatistics.DisplayAllDescisions(votingStatistics);
                }
            });
        }
    }

    public void ReceivedVotingStartedMessage(VotingStartedMessage votingStartedMessage)
    {
        displayStatusBar.DisplayTimer(votingTime);
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

    


    public void ServerIssues(int errorCode)
    {
        moderatorClientGuid = Guid.NewGuid();
        switch (errorCode)
        {
            case 1006:
                activeScreenManager.ShowErrorScreen("Es konnte keine Verbindung zum Server aufgebaut werden.");
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
                moderatorClientGuid = Guid.NewGuid();
                SendRequestSessionOpenedMessage();
                break;
            case ErrorType.UnknownGuid:
                moderatorClientGuid = Guid.NewGuid();
                SendRequestSessionOpenedMessage();
                break;
            case ErrorType.WrongPassword:
                activeScreenManager.ShowErrorScreen("Passwort ist falsch. Bitte erneut versuchen oder im Offline-Modus fortfahren.");
                break;
            case ErrorType.WrongSession:
                moderatorClientGuid = Guid.NewGuid();
                SendRequestSessionOpenedMessage();
                break;
            default:
                break;
        }
    }

    public void SwitchModes()
    {
        
        if (GameState.gameIsOnline)
        {
            GameState.gameIsOnline = false;
            ContinueOfflineStory(gameStory.playThrough.CurrentEvent);
        }
        else
        {
            displayDecision.RemoveOfflineDecisionListeners();
            characterSelection.RemoveOfflinePickButtons();
            if (qualityQuestWebSocket.webSocket == null)
            {
                activeScreenManager.ShowConnection();
            }
            else
            {
                switch (qualityQuestWebSocket.webSocket.ReadyState)
                {
                    case WebSocketState.Closed:
                        activeScreenManager.ShowConnection();
                        break;
                    case WebSocketState.Open:
                        if (sessionKey == null)
                        {
                            SendRequestSessionOpenedMessage();
                        }
                        else
                        {
                            GameState.gameIsOnline = true;
                            ContinueOnlineStory(gameStory.playThrough.CurrentEvent);
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


    public void StartOfflinePlaythrough()
    {
        Debug.Log("ahaha");
        activeScreenManager.ShowCharacterSelection();
        characterSelection.ActivateOfflineCharacterPickButtons();
    }

    public void PickNoruso()
    {
        Debug.Log("haha");
        characterSelection.InitializeCharacter(characterSelection.noruso, gameStory, displayStatusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {
            ContinueOnlineStory(list[0]);
        }
        else
        {
            ContinueOfflineStory(list[0]);
        }

    }

    public void PickLumati()
    {
        characterSelection.InitializeCharacter(characterSelection.lumati, gameStory, displayStatusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {
            ContinueOnlineStory(list[1]);
        }
        else
        {
            ContinueOfflineStory(list[1]);
        }
    }

    public void PickTurgal()
    {
        characterSelection.InitializeCharacter(characterSelection.turgal, gameStory, displayStatusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {
            ContinueOnlineStory(list[2]);
        }
        else
        {
            ContinueOfflineStory(list[2]);
        }
    }

    public void PickKirogh()
    {
        characterSelection.InitializeCharacter(characterSelection.kirogh, gameStory, displayStatusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {
            ContinueOnlineStory(list[3]);
        }
        else
        {
            ContinueOfflineStory(list[3]);
        }
    }

    public void ContinueOfflineStory(StoryEvent storyEvent)
    {
        gameStory.SetCurrentEvent(storyEvent);
        Debug.Log("Current Event: " + gameStory.playThrough.CurrentEvent.Description);

        if (storyEvent.SkillChange != null)
        {
            gameStory.playThrough.Character.Abilities.updateAbilities(storyEvent.SkillChange);
            displayStatusBar.DisplaySkills(gameStory.playThrough.Character.Abilities);
            displayStatusBar.UpdateSkillChanges(storyEvent.SkillChange);
        }

        switch (storyEvent.StoryType)
        {
            case StoryEventType.StoryRootEvent:
                StartOfflinePlaythrough();
                break;
            case StoryEventType.StoryBackground:
                ContinueBackground(storyEvent);
                break;
            case StoryEventType.StoryDecision:
                ContinueOfflineDecision(storyEvent);
                break;
            case StoryEventType.StoryDecisionOption:
                ContinueDecisionOption(storyEvent);
                break;
            case StoryEventType.StoryFlow:
                ContinueStoryFlow(storyEvent);
                break;
            default:
                break;
        }
    }

    private void ContinueBackground(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Any())
        {
            videoBackground.SwitchBackground(currentEvent.Background);
            if (GameState.gameIsOnline)
            {
                ContinueOnlineStory(currentEvent.Children.First());
            }
            else
            {
                ContinueOfflineStory(currentEvent.Children.First());
            }
        }
        else
        {
            // Should not happen
            Debug.Log("Story Event has no Children");
        }
    }

    private void ContinueOfflineDecision(StoryEvent currentEvent)
    {
        displayDecision.RemoveOfflineDecisionListeners();

        var children = currentEvent.Children.ToList();
        displayDecision.LoadDecision(currentEvent, children);
        activeScreenManager.ShowDecision();

        if (children.Count >= 2)
        {
            displayDecision.selectOfflineA.onClick.AddListener(delegate { ContinueOfflineStory(children[0]); });
            displayDecision.selectOfflineB.onClick.AddListener(delegate { ContinueOfflineStory(children[1]); });
        }

        if (children.Count >= 3)
        {
            displayDecision.selectOfflineC.onClick.AddListener(delegate { ContinueOfflineStory(children[2]); });
        }

        if (children.Count >= 4)
        {
            displayDecision.selectOfflineD.onClick.AddListener(delegate { ContinueOfflineStory(children[3]); });
        }
    }
}
