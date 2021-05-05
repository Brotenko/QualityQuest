using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game;
using MessageContainer;
using UnityEngine;
using MessageContainer.Messages;
using TMPro;
using WebSocketSharp;

/// <summary>
/// Class for the main logic of the ModeratorClient.
/// </summary>
public class Client : MonoBehaviour
{
    [SerializeField]
    private QualityQuestWebSocket qualityQuestWebSocket;
    [SerializeField]
    private ActiveScreenManager activeScreenManager;
    [SerializeField]
    private DisplayStatusbar displayStatusBar;
    [SerializeField]
    private GameAudio gameAudio;
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
    private CharacterSelection characterSelection;
    [SerializeField]
    private TMP_InputField ip;
    [SerializeField]
    private TMP_InputField port;
    [SerializeField]
    private TMP_InputField password;
    [SerializeField] 
    private TMP_InputField votingTime;

    private ClientLogic clientLogic;

    /// <summary>
    /// The unity awake method gets called on script initialization.
    /// Used to construct the clientLogic.
    /// </summary>
    private void Awake()
    {
        clientLogic = new ClientLogic(30);
    }

    /// <summary>
    /// Gets called after all awake methods.
    /// Starts the music and background video and starts the offline mode.
    /// </summary>
    private void Start()
    {
        videoBackground.SwitchBackground(BackgroundType.University);

        if (!GameState.gameIsOnline)
        {
            StartOfflinePlaythrough();
        }
    }

    /// <summary>
    /// Method to start a connection with a WebSocket. Uses the InputFields for ip, port and password.
    /// Shows a errorMessage if one or more of the inputs are blank.
    /// </summary>
    public void Connect()
    {
        try
        {
            if (ip.text != "" && port.text != "" && password.text != "")
            {
                qualityQuestWebSocket.StartConnection(ip.text, port.text);
            }
            else
            {
                activeScreenManager.ShowErrorScreen(
                    "Ip, Port oder Passwort fehlt. Bitte neu versuchen oder im Offline-Modus fortfahren.");
            }
        }
        catch (ArgumentException argumentException)
        {
            activeScreenManager.ShowErrorScreen("Keine gültiger Port. Bitte erneut versuchen oder im Offline-Modus fortfahren.");
        }
    }

    /// <summary>
    /// Method that is called as soon as a connection to the WebSocket is established.
    /// Starts a new session or connects to an existing one.
    /// </summary>
    public void ConnectionEstablished()
    {
        // Open new session
        if (clientLogic.SessionKey == null)
        {
            SendRequestOpenSessionMessage();
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
        qualityQuestWebSocket.SendMessage(clientLogic.InitializeReconnectMessage());
    }

    /// <summary>
    /// Method to send a RequestSessionOpenedMessage.
    /// </summary>
    public void SendRequestOpenSessionMessage()
    {
        qualityQuestWebSocket.SendMessage(clientLogic.InitializeRequestOpenSessionMessage(password.text));
    }

    /// <summary>
    /// Method to send a RequestGameStartMessage.
    /// </summary>
    public void SendRequestGameStartMessage()
    {
        qualityQuestWebSocket.SendMessage(clientLogic.InitializeRequestGameStartMessage());
    }

    /// <summary>
    /// Method to send a RequestCloseSessionMessage.
    /// </summary>
    public void SendRequestCloseSessionMessage()
    {
        if (clientLogic.SessionKey == null) return;
        qualityQuestWebSocket.SendMessage(clientLogic.InitializeRequestCloseSessionMessage());
    }

    /// <summary>
    /// Method when the ModeratorClient receives a GamePausedStatusMessage.
    /// Disables the pausePanel.
    /// </summary>
    /// <param name="gamePausedStatusMessage">The GamePausedStatusMessage.</param>
    public void ReceivedGamePausedStatusChange(GamePausedStatusMessage gamePausedStatusMessage)
    {
        activeScreenManager.ShowPauseMenu(clientLogic.Url, clientLogic.SessionKey);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a ReconnectSuccessfulMessage.
    /// Switches the game into online mode.
    /// </summary>
    /// <param name="reconnectSuccessfulMessage">The ReconnectSuccessfulMessage.</param>
    public void ReceivedReconnectSuccessfulMessage(ReconnectSuccessfulMessage reconnectSuccessfulMessage)
    {
        activeScreenManager.ShowPauseButton(true);
        GameState.gameIsOnline = true;
        ContinueOnlineStory(clientLogic.StoryGraph.CurrentEvent);
    }

    /// <summary>
    /// Method when the ModeratorClient receives a SessionOpenedMessage.
    /// Saves the url and sessionKey and switches to the qrCodePanel.
    /// </summary>
    /// <param name="sessionOpenedMessage">The SessionOpenedMessage.</param>
    public void ReceivedSessionOpenedMessage(SessionOpenedMessage sessionOpenedMessage)
    {
        GameState.gameIsOnline = true;
        clientLogic.SaveUrlAndSessionKey(sessionOpenedMessage);
        activeScreenManager.ShowQrCodePanel(clientLogic.Url, clientLogic.SessionKey);
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
        ContinueOnlineStory(clientLogic.StoryGraph.CurrentEvent);
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
        // delete old click listeners
        displayDecision.RemoveOnlineDecisionListeners();

        clientLogic.ActiveVoting = false;

        var currentEvent = clientLogic.StoryGraph.CurrentEvent;
        
        try
        {
            // validate the received message.
            clientLogic.ValidateVotingEndedMessage(currentEvent, votingEndedMessage.VotingResults);
            // save voting statistic
            clientLogic.SaveStatistics(currentEvent.Description, currentEvent.Children, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes);

            var currentEventChildren = clientLogic.StoryGraph.CurrentEvent.Children.ToList();

            activeScreenManager.ShowResults();
            result.LoadResult(currentEvent, currentEventChildren, votingEndedMessage.VotingResults, votingEndedMessage.TotalVotes, votingEndedMessage.WinningOption);

            // Activate results screen and loads the results. Activates click listeners for option one and two.
            if (currentEventChildren.Count >= 2)
            {
                displayDecision.selectOnlineA.onClick.AddListener(delegate
                {
                    if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
                    {
                        PickNoruso();
                    }
                    else 
                    {
                        ContinueOnlineStory(currentEventChildren[0]);
                    }
                        
                });
                displayDecision.selectOnlineB.onClick.AddListener(delegate 
                { 
                    if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
                    {
                        PickLumati();
                    }
                    else 
                    {
                        ContinueOnlineStory(currentEventChildren[1]);
                    }
                });
            } 
            // Activates the click listener for option 3
            if (currentEventChildren.Count >= 3)
            {
                displayDecision.selectOnlineC.onClick.AddListener(delegate 
                {
                    if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent)) 
                    {
                        PickTurgal();
                    }
                    else 
                    {
                        ContinueOnlineStory(currentEventChildren[2]);
                    }
                });
            } 
            // Activates the click listener for option 4
            if (currentEventChildren.Count >= 4) 
            {
                displayDecision.selectOnlineD.onClick.AddListener(delegate
                {
                    if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
                    {
                        PickKirogh();
                    }
                    else
                    {
                        ContinueOnlineStory(currentEventChildren[3]);
                    }
                });
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
    /// Method gets called to continue with the story in online mode. Updates the character stats.
    /// Checks the next StoryEvenType and continues the story depending on the StoryEventType.
    /// </summary>
    /// <param name="storyEvent">The next StoryEvent.</param>
    public void ContinueOnlineStory(StoryEvent storyEvent)
    {
        try
        {
            Debug.Log("Current Event: " + storyEvent.Description);

            if (!storyEvent.StoryType.Equals(StoryEventType.StoryEnd))
            {
                clientLogic.ValidateStoryEvent(storyEvent);
            }

            displayDecision.RemoveOnlineDecisionListeners();
            clientLogic.StoryGraph.SetCurrentEvent(storyEvent);

            if (storyEvent.SkillChange != null)
            {
                clientLogic.StoryGraph.Character.Abilities.updateAbilities(storyEvent.SkillChange);
                displayStatusBar.DisplaySkills(clientLogic.StoryGraph.Character.Abilities);
                displayStatusBar.UpdateSkillChanges(storyEvent.SkillChange);
                gameAudio.PlaySkillChangeSound();
            }

            switch (storyEvent.StoryType)
            {
                case StoryEventType.StoryBackground:
                    ContinueBackground(storyEvent);
                    break;
                case StoryEventType.StoryRootEvent:
                    ContinueStoryDecision(storyEvent);
                    break;
                case StoryEventType.StoryDecision:
                    ContinueStoryDecision(storyEvent);
                    break;
                case StoryEventType.StoryDecisionOption:
                    ContinueDecisionOption(storyEvent);
                    break;
                case StoryEventType.StoryFlow:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StorySpecialDecision:
                    clientLogic.ContinueSpecialDecision(storyEvent);
                    ContinueStoryDecision(storyEvent);
                    break;
                case StoryEventType.StoryUnlockDecisionOption:
                    clientLogic.UnlockDecision();
                    ContinueDecisionOption(storyEvent);
                    break;
                case StoryEventType.StorySpecialOption:
                    ContinueDecisionOption(storyEvent);
                    break;
                case StoryEventType.StoryWorkshop:
                    WorkshopEvent(storyEvent);
                    break;
                case StoryEventType.StoryFired:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryWorkshopInvite:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryWorkshopNoInvite:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryEnd:
                    StoryEnd(storyEvent);
                    break;
                default:
                    Debug.Log("StoryEventType is not valid.");
                    break;
            }
        }
        catch (WrongStoryEvent wrongStoryEvent)
        {
            Debug.Log("WrongStoryException: " + wrongStoryEvent);
            activeScreenManager.GameCrash();
        }
    }

    /// <summary>
    /// Method if the special StoryEvent before the Workshop is reached.
    /// Checks the player performance depending on the character stats and continues the story.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
    public void WorkshopEvent(StoryEvent currentEvent)
    {
        displayStoryFlow.RemoveStoryFlowListeners();

        activeScreenManager.ShowStoryFlow();
        displayStoryFlow.SetStoryFlow(currentEvent);

        displayStoryFlow.storyFlowButton.onClick.AddListener(delegate
        {
            ContinueOnlineStory(clientLogic.WorkshopDecision(currentEvent));
        });
    }

    /// <summary>
    /// Method which gets triggered if the last StoryEvent gets reached.
    /// If on online mode, sends a RequestCloseSessionMessage and shows statistics.
    /// </summary>
    /// <param name="currentEvent">The last StoryEvent.</param>
    public void StoryEnd(StoryEvent currentEvent)
    {
        displayStoryFlow.RemoveStoryFlowListeners();

        activeScreenManager.ShowStoryFlow();
        displayStoryFlow.SetStoryFlow(currentEvent);

        displayStoryFlow.storyFlowButton.onClick.AddListener(delegate
        {
            if (!GameState.gameIsOnline) return;
            SendRequestCloseSessionMessage();
            if (clientLogic.VotingStatistic == null) return;
            activeScreenManager.ShowStatistics();
            displayStatistics.DisplayAllDecisions(clientLogic.VotingStatistic);
        });
    }

    /// <summary>
    /// Method to continue the story if after a StoryEventDecision.
    /// </summary>
    /// <param name="currentEvent">The current StoryEvent.</param>
    public void ContinueDecisionOption(StoryEvent currentEvent)
    {
        // Display dice animation if its a random event
        if (currentEvent.Children.Count > 1)
        {
            displayStatusBar.DisplayDice(3);
            gameAudio.PlayDiceSound();
        }

        if (GameState.gameIsOnline)
        {
            ContinueOnlineStory(clientLogic.ContinueDecision(clientLogic.StoryGraph));
        }
        else
        {
            ContinueOfflineStory(clientLogic.ContinueDecision(clientLogic.StoryGraph));
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

        if (currentEvent.StoryType.Equals(StoryEventType.StoryRootEvent))
        {
            activeScreenManager.ShowCharacterSelection();
        }
        else
        {
            activeScreenManager.ShowDecision();
        }

        displayDecision.LoadDecision(currentEvent, currentEvent.Children.ToList());

        qualityQuestWebSocket.SendMessage(clientLogic.InitializeRequestStartVotingMessage(currentEvent));
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

    /// <summary>
    /// Method when the ModeratorClient receives a VotingStartedMessage. Starts the voting time on the lower left corner.
    /// </summary>
    /// <param name="votingStartedMessage">The VotingStartedMessage.</param>
    public void ReceivedVotingStartedMessage(VotingStartedMessage votingStartedMessage)
    {
        clientLogic.ActiveVoting = true;
        displayStatusBar.DisplayTimer(clientLogic.VotingTime);
    }

    /// <summary>
    /// Method to request a game pause. Sends the RequestGamePause message and activates or disables the pause screen.
    /// Is used by unity.
    /// </summary>
    public void RequestGamePause()
    {
        if (clientLogic.ActiveVoting)
        {
            qualityQuestWebSocket.SendMessage(!ActiveScreenManager.paused
                ? clientLogic.InitializeRequestGamePausedStatusChangeMessage(true)
                : clientLogic.InitializeRequestGamePausedStatusChangeMessage(false));
        }
        else
        {
            activeScreenManager.ShowPauseMenu(clientLogic.Url, clientLogic.SessionKey);
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
                activeScreenManager.ShowErrorScreen("Verbindung wurde beendet. Bitte erneut Verbindung oder im Offline-Modus fortfahren.");
                break;
            case 1006:
                activeScreenManager.ShowErrorScreen("Es konnte keine Verbindung zum Server aufgebaut werden.");
                break;
            case 1005:
                Debug.Log("Connection is closed.");
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
                clientLogic.SetNewModeratorClientGuid();
                SendRequestOpenSessionMessage();
                break;
            case ErrorType.UnknownGuid:
                clientLogic.SetNewModeratorClientGuid();
                SendRequestOpenSessionMessage();
                break;
            case ErrorType.WrongPassword:
                activeScreenManager.ShowErrorScreen("Passwort ist falsch. Bitte erneut versuchen oder im Offline-Modus fortfahren.");
                break;
            case ErrorType.WrongSession:
                clientLogic.SetNewModeratorClientGuid();
                SendRequestOpenSessionMessage();
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
        if (GameState.gameIsOnline)
        {
            // reset every connection to the online mode
            ActiveScreenManager.paused = false;
            GameState.gameIsOnline = false;
            clientLogic.ActiveVoting = false;
            activeScreenManager.ShowPauseButton(false);

            // Check if there is a active connection
            if (qualityQuestWebSocket.webSocket != null)
            {
                qualityQuestWebSocket.CloseConnection();
                ContinueOfflineStory(clientLogic.StoryGraph.CurrentEvent);
            }
            else
            {
                ContinueOfflineStory(clientLogic.StoryGraph.CurrentEvent);
            }
        }
        else
        {
            // remove offline listeners
            characterSelection.RemoveOfflinePickButtons();
            displayDecision.RemoveOfflineDecisionListeners();

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
                        if (clientLogic.SessionKey == null)
                        {
                            GameState.gameIsOnline = true;
                            SendRequestOpenSessionMessage();
                        }
                        else
                        {
                            GameState.gameIsOnline = true;
                            ContinueOnlineStory(clientLogic.StoryGraph.CurrentEvent);
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
        characterSelection.InitializeCharacter(characterSelection.noruso, clientLogic.StoryGraph, displayStatusBar);
        var list = clientLogic.StoryGraph.CurrentEvent.Children.ToList();
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
        characterSelection.InitializeCharacter(characterSelection.lumati, clientLogic.StoryGraph, displayStatusBar);
        var list = clientLogic.StoryGraph.CurrentEvent.Children.ToList();
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
        characterSelection.InitializeCharacter(characterSelection.turgal, clientLogic.StoryGraph, displayStatusBar);
        var list = clientLogic.StoryGraph.CurrentEvent.Children.ToList();
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
        characterSelection.InitializeCharacter(characterSelection.kirogh, clientLogic.StoryGraph, displayStatusBar);
        var list = clientLogic.StoryGraph.CurrentEvent.Children.ToList();
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
        try
        {
            if (!storyEvent.StoryType.Equals(StoryEventType.StoryEnd))
            {
                clientLogic.ValidateStoryEvent(storyEvent);
            }

            clientLogic.StoryGraph.SetCurrentEvent(storyEvent);
            Debug.Log("Current Event: " + clientLogic.StoryGraph.CurrentEvent.Description);

            if (storyEvent.SkillChange != null)
            {
                clientLogic.StoryGraph.Character.Abilities.updateAbilities(storyEvent.SkillChange);
                displayStatusBar.DisplaySkills(clientLogic.StoryGraph.Character.Abilities);
                displayStatusBar.UpdateSkillChanges(storyEvent.SkillChange);
                gameAudio.PlaySkillChangeSound();
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
                case StoryEventType.StorySpecialDecision:
                    clientLogic.ContinueSpecialDecision(storyEvent);
                    ContinueOfflineDecision(storyEvent);
                    break;
                case StoryEventType.StoryUnlockDecisionOption:
                    clientLogic.UnlockDecision();
                    ContinueDecisionOption(storyEvent);
                    break;
                case StoryEventType.StorySpecialOption:
                    ContinueDecisionOption(storyEvent);
                    break;
                case StoryEventType.StoryWorkshop:
                    WorkshopEvent(storyEvent);
                    break;
                case StoryEventType.StoryFired:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryWorkshopInvite:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryWorkshopNoInvite:
                    ContinueStoryFlow(storyEvent);
                    break;
                case StoryEventType.StoryEnd:
                    StoryEnd(storyEvent);
                    break;
                default:
                    break;
            }
        }
        catch (WrongStoryEvent wrongStoryEvent)
        {
            Debug.Log("WrongStoryException: " + wrongStoryEvent);
            activeScreenManager.GameCrash();
        }
    }

    /// <summary>
    /// Method to switch the background between the StoryEvents. Continues the game the game in offline or online mode, after switching the background.
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ContinueBackground(StoryEvent currentEvent)
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

    /// <summary>
    /// Method to change the VotingTime.
    /// </summary>
    public void SetVotingTime()
    {
        var newVotingTime = int.Parse(this.votingTime.text);
        clientLogic.SetVotingTime(newVotingTime);
    }
}
