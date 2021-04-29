using System;
using System.Collections.Generic;
using System.Linq;
using MessageContainer;
using UnityEngine;
using MessageContainer.Messages;
using TMPro;
using WebSocketSharp;

public class OnlineClientManager : MonoBehaviour
{
    [SerializeField]
    private QualityQuestWebSocket qualityQuestWebSocket;
    [SerializeField]
    private ActiveScreenManager activeScreenManager;
    [SerializeField]
    private GameStory gameStory;
    [SerializeField]
    private DisplayStatusbar displayStatusBar;
    [SerializeField]
    private DisplayDecision displayDecision;
    [SerializeField]
    private DisplayStoryFlow displayStoryFlow;
    [SerializeField]
    private Result result;
    [SerializeField]
    private DisplayStatistics displayStatistics;
    [SerializeField]
    private GameBackground videoBackground;
    [SerializeField]
    private VotingStatistics votingStatistics;
    [SerializeField]
    private CharacterSelection characterSelection;

    //private ClientLogic clientLogic;
    private bool activeVoting;
    private int votingTime;
    private int debugVotingTime;

    [SerializeField]
    private TMP_InputField ip;
    [SerializeField]
    private TMP_InputField port;
    [SerializeField]
    private TMP_InputField password;

    private string sessionKey;
    private string url;
    private Guid moderatorClientGuid;

    private void Awake()
    {
        clientLogic = new ClientLogic();
    }

    private void Start()
    {
        if (!GameState.gameIsOnline)
        {
            StartOfflinePlaythrough();
        }
    }

    /// <summary>
    /// Method that initializes all variables needed for the online mode.
    /// </summary>
    public void StartOnlineMode()
    {
        votingTime = 20;
        debugVotingTime = 1;
        votingStatistics ??= new VotingStatistics(new List<VotingResult>());
    }

    /// <summary>
    /// Method to start a connection with a WebSocket. Uses the InputFields for ip, port and password.
    /// Shows a errorMessage if one or more of the inputs are blank.
    /// </summary>
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

    /// <summary>
    /// Method that is called as soon as a connection to the WebSocket is established.
    /// Starts a new session or connects to an existing one.
    /// </summary>
    public void ConnectionEstablished()
    {
        // Open new session
        if (sessionKey == null)
        {
            moderatorClientGuid = Guid.NewGuid();
            SendRequestSessionOpenedMessage();
        }
        // Reconnect 
        else
        {
            SendReconnectMessage();
        }
    }

    /// <summary>
    /// Method to send a ReconnectMessage.
    /// </summary>
    public void SendReconnectMessage()
    {
        var reconnectMessage = new ReconnectMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(reconnectMessage);
    }

    /// <summary>
    /// Method to send a RequestSessionOpenedMessage.
    /// </summary>
    public void SendRequestSessionOpenedMessage()
    {
        var requestOpenSessionMessage = new RequestOpenSessionMessage(moderatorClientGuid, password.text);
        qualityQuestWebSocket.SendMessage(requestOpenSessionMessage);
    }

    /// <summary>
    /// Method to send a RequestGameStartMessage.
    /// </summary>
    public void SendRequestGameStartMessage()
    {
        Debug.Log("RequestGameStartMessage");
        var requestGameStartMessage = new RequestGameStartMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(requestGameStartMessage);
    }

    /// <summary>
    /// Method to send a RequestCloseSessionMessage.
    /// </summary>
    public void SendRequestCloseSessionMessage()
    {
        if (sessionKey == null) return;
        var requestCloseSessionMessage = new RequestCloseSessionMessage(moderatorClientGuid, sessionKey);
        qualityQuestWebSocket.SendMessage(requestCloseSessionMessage);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a GamePausedStatusMessage.
    /// Disables the pausePanel.
    /// </summary>
    /// <param name="gamePausedStatusMessage">The GamePausedStatusMessage.</param>
    public void ReceivedGamePausedStatusChange(GamePausedStatusMessage gamePausedStatusMessage)
    {
        if (activeVoting)
        {
            activeScreenManager.ShowPauseMenu(url, sessionKey);
        }
        else
        {
            activeScreenManager.ShowPauseMenu(url, sessionKey);
        }
    }

    /// <summary>
    /// Method when the ModeratorClient receives a ReconnectSuccessfulMessage.
    /// Switches the game into online mode.
    /// </summary>
    /// <param name="reconnectSuccessfulMessage">The ReconnectSuccessfulMessage.</param>
    public void ReceivedReconnectSuccessfulMessage(ReconnectSuccessfulMessage reconnectSuccessfulMessage)
    {
        GameState.gameIsOnline = true;
        ContinueOnlineStory(gameStory.playThrough.CurrentEvent);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a SessionOpenedMessage.
    /// Saves the url and sessionKey and switches to the qrCodePanel.
    /// </summary>
    /// <param name="sessionOpenedMessage">The SessionOpenedMessage.</param>
    public void ReceivedSessionOpenedMessage(SessionOpenedMessage sessionOpenedMessage)
    {
        GameState.gameIsOnline = true;
        url = sessionOpenedMessage.DirectURL.ToString();
        sessionKey = sessionOpenedMessage.SessionKey;
        activeScreenManager.ShowQrCodePanel(sessionOpenedMessage.DirectURL.ToString(), sessionOpenedMessage.SessionKey);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a AudienceStatusMessage.
    /// Updates the PlayerAudience count on the qrCodePanel.
    /// </summary>
    /// <param name="audienceStatusMessage">The AudienceStatusMessage.</param>
    public void ReceivedAudienceStatusMessage(AudienceStatusMessage audienceStatusMessage)
    {
        activeScreenManager.UpdateAudienceCount(audienceStatusMessage.AudienceCount);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a GameStartedMessage.
    /// Starts the online mode of the QualityQuest.
    /// </summary>
    /// <param name="gameStartedMessage">The GameStartedMessage.</param>
    public void ReceivedGameStartedMessage(GameStartedMessage gameStartedMessage)
    {
        ContinueOnlineStory(gameStory.playThrough.CurrentEvent);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a VotingEndedMessage.
    /// Checks the VotingEndedMessage for correctness and then saves the voting for the statistics.
    /// Switches to the resultPanel and displays the voting.
    /// Activates the ClickListeners so that the moderator can continue with the game.
    /// </summary>
    /// <param name="votingEndedMessage">The VotingEndedMessage.</param>
    public void ReceivedVotingEndedMessage(VotingEndedMessage votingEndedMessage)
    {
        var currentEvent = gameStory.playThrough.CurrentEvent;
        try
        {
            // validate the received message.
            ValidateVotingEndedMessage(currentEvent, votingEndedMessage.VotingResults);
            // save voting statistic
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

    /// <summary>
    /// Method to display the result of the CharacterSelection after a VotingEndedMessage.
    /// Sets the clickListeners for the individual characters.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
    /// <param name="currentEventChildren">The children of the current StoryEvent.</param>
    /// <param name="votingEndedMessage">The VotingEndedMessage.</param>
    private void OnlineModePickInitializeChar(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, VotingEndedMessage votingEndedMessage)
    {
        activeScreenManager.ShowResults();
        result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes, votingEndedMessage.WinningOption);
        displayDecision.selectOnlineA.onClick.AddListener(PickNoruso);
        displayDecision.selectOnlineB.onClick.AddListener(PickLumati);
        displayDecision.selectOnlineC.onClick.AddListener(PickTurgal);
        displayDecision.selectOnlineD.onClick.AddListener(PickKirogh);
    }

    /// <summary>
    /// Method to save the voting statistic.
    /// </summary>
    /// <param name="votingPrompt">The current votingPrompt.</param>
    /// <param name="votingOptions">The votingOptions of the current votingPrompt.</param>
    /// <param name="votingResults">The result of the voting.</param>
    /// <param name="totalVotes">The total number of votes.</param>
    private void SaveStatistics(string votingPrompt, HashSet<StoryEvent> votingOptions, Dictionary<Guid, int> votingResults, int totalVotes)
    {
        var voting = new VotingResult(votingPrompt, totalVotes, new Dictionary<string, int>());

        foreach (var storyEvent in votingOptions)
        {
            voting.VotingOptions.Add(storyEvent.Description, votingResults[storyEvent.EventId]);
        }
        votingStatistics.Statistic.Add(voting);
    }

    /// <summary>
    /// Checks if the voting options match the current StoryEvent.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
    /// <param name="votingOptions">The votingOptions of the VotingEndedMessage.</param>
    public void ValidateVotingEndedMessage(StoryEvent currentEvent, Dictionary<Guid, int> votingOptions)
    {
        if (currentEvent.Children.Any(child => !votingOptions.ContainsKey(child.EventId)))
        {
            throw new WrongVotingEndedMessage("The VotingEndedMessage does not match the current StoryEvent.");
        }
    }

    /// <summary>
    /// Method gets called to continue with the story in online mode. Updates the character stats.
    /// Checks the next StoryEvenType and continues the story depending on the StoryEventType.
    /// </summary>
    /// <param name="storyEvent">The next StoryEvent.</param>
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

    /// <summary>
    /// Method to continue the story with a StoryFlowDecisionOption.
    /// If the StoryEvent is a RandomEvent, the story will be continued with a random StoryEvent.
    /// Continues the story in offline or online mode, depending on the state of the game.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
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

    /// <summary>
    /// Method to continue the story with a StoryFlowDecision if the game is in online mode.
    /// Activates the decisionPanel and sends the RequestStartVotingMessage.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
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

    /// <summary>
    /// Method to continue the StoryFlow between the StoryFlowDecisions.
    /// Activates the click listeners for the StoryFlowButton so the Moderator can continue the story.
    /// Continues the game in online or offline mode, depending on the state of the game.
    /// The last StoryFlow activates and shows the statistics, if the game is in online mode. Also sends the RequestCloseSessionMessage.
    /// </summary>
    /// <param name="currentEvent">The current StoryFlow StoryEvent.</param>
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
                    displayStatistics.DisplayAllDecisions(votingStatistics);
                }
            });
        }
    }

    /// <summary>
    /// Method when the ModeratorClient receives a VotingStartedMessage. Starts the voting time on the lower left corner.
    /// </summary>
    /// <param name="votingStartedMessage">The VotingStartedMessage.</param>
    public void ReceivedVotingStartedMessage(VotingStartedMessage votingStartedMessage)
    {
        displayStatusBar.DisplayTimer(votingTime);
    }

    /// <summary>
    /// Method to request a game pause. Sends the RequestGamePause message and activates or disables the pause screen.
    /// </summary>
    public void RequestGamePause()
    {
        if (gameStory.playThrough.CurrentEvent.StoryType.Equals(StoryEventType.StoryDecision) || gameStory.playThrough.CurrentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
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
        else
        {
            activeScreenManager.ShowPauseMenu(url, sessionKey);
        }
    }



    /// <summary>
    /// Method gets called on the WebSocket onClose event. Triggers an ErrorMessage depending on the reason why the WebSocket was closed
    /// </summary>
    /// <param name="errorCode">The errorCode of the close reason.</param>
    public void ServerIssues(int errorCode)
    {
        switch (errorCode)
        {
            case 1000:
                activeScreenManager.ShowErrorScreen("Passwort ist falsch. Bitte erneut versuchen oder im Offline-Modus fortfahren.");
                break;
            case 1006:
                activeScreenManager.ShowErrorScreen("Es konnte keine Verbindung zum Server aufgebaut werden.");
                break;
            default:
                activeScreenManager.ShowErrorScreen("Verbindung verloren. Bitte erneut verbinden oder im Offline-Modus fortfahren.");
                break;
        }
    }

    /// <summary>
    /// Method when the ModeratorClient receives a ErrorMessage.
    /// Depending on the ErrorMessage a new SessionOpenedMessage is sent or the ErrorPanel is activated.
    /// </summary>
    /// <param name="errorMessage"></param>
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

    /// <summary>
    /// Method to switch between the offline and offline mode.
    /// </summary>
    public void SwitchModes()
    {
        Debug.Log("Switch modes:" + GameState.gameIsOnline);
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
                            GameState.gameIsOnline = true;
                            SendRequestSessionOpenedMessage();
                            activeScreenManager.ActivatePauseButton();
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

    /// <summary>
    /// Method to start the offline playthrough.
    /// </summary>
    public void StartOfflinePlaythrough()
    {
        activeScreenManager.ShowCharacterSelection();
        characterSelection.ActivateOfflineCharacterPickButtons();
    }

    /// <summary>
    /// Method to initialize the char Noruso and continues the game in offline or online mode, depending on the game state.
    /// </summary>
    public void PickNoruso()
    {
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

    /// <summary>
    /// Method to initialize the char Lumati and continues the game in offline or online mode, depending on the game state.
    /// </summary>
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

    /// <summary>
    /// Method to initialize the char Turgal and continues the game in offline or online mode, depending on the game state.
    /// </summary>
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

    /// <summary>
    /// Method to initialize the char Kirogh and continues the game in offline or online mode, depending on the game state.
    /// </summary>
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

    /// <summary>
    /// Method gets called to continue with the story in offline mode. Updates the character stats.
    /// Checks the next StoryEvenType and continues the story depending on the StoryEventType.
    /// </summary>
    /// <param name="storyEvent">The next StoryEvent.</param>
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

    /// <summary>
    /// Method to switch the background between the StoryEvents. Continues the game the game in offline or online mode, after switching the background.
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ContinueBackground(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Any())
        {
            videoBackground.SwitchBackground(currentEvent.BackgroundType);
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

    /// <summary>
    /// Method to continue they game if a StoryFlowDecision happens in offline mode. Activates the click
    /// listeners so that the player can decide by clicking on the button, also activates the decisionPanel.
    /// </summary>
    /// <param name="currentEvent"></param>
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
