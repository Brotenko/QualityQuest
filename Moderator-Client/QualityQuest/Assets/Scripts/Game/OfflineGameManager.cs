using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class OfflineGameManager : MonoBehaviour
{
    /*
    public DisplayStatusbar statusBar;
    public CharacterSelection characterSelection;
    public GameStory gameStory;
    public VideoBackground videoBackground;
    public DisplayDecision displayDecision;
    public DisplayStoryFlow displayStoryFlow;
    public ActiveScreenManager activeScreenManager;

    void Start()
    {
        if (!GameState.gameIsOnline)
        {
            StartOfflinePlaythrough();
        }
    }


    public void StartOfflinePlaythrough()
    {
        activeScreenManager.ShowCharacterSelection();
        characterSelection.ActivateOfflineCharacterPickButtons();
    }

    public void PickNoruso()
    {
        characterSelection.InitializeCharacter(characterSelection.noruso, gameStory, statusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {

        }
        else
        {
            ContinueOfflineStory(list[0]);
        }
        
    }

    public void PickLumati()
    {
        characterSelection.InitializeCharacter(characterSelection.lumati, gameStory, statusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {

        }
        else
        {
            ContinueOfflineStory(list[0]);
        }
    }

    public void PickTurgal()
    {
        characterSelection.InitializeCharacter(characterSelection.turgal, gameStory, statusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {

        }
        else
        {
            ContinueOfflineStory(list[0]);
        }
    }

    public void PickKirogh()
    {
        characterSelection.InitializeCharacter(characterSelection.kirogh, gameStory, statusBar);
        var list = gameStory.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {

        }
        else
        {
            ContinueOfflineStory(list[0]);
        }
    }

    public void ContinueOfflineStory(StoryEvent storyEvent)
    {
        gameStory.SetCurrentEvent(storyEvent);
        Debug.Log("Current Event: " + gameStory.playThrough.CurrentEvent.Description);

        if (storyEvent.SkillChange != null)
        {
            gameStory.playThrough.Character.Abilities.updateAbilities(storyEvent.SkillChange);
            statusBar.DisplaySkills(gameStory.playThrough.Character.Abilities);
            statusBar.UpdateSkillChanges(storyEvent.SkillChange);
        }

        switch (storyEvent.StoryType)
        {
            case StoryEventType.StoryRootEvent:
                StartOfflinePlaythrough();
                break;
            case StoryEventType.StoryBackground:
                ContinueOfflineBackground(storyEvent);
                break;
            case StoryEventType.StoryDecision:
                ContinueOfflineDecision(storyEvent);
                break;
            case StoryEventType.StoryDecisionOption:
                ContinueOfflineDecisionOption(storyEvent);
                break;
            case StoryEventType.StoryFlow:
                ContinueOfflineStoryFlow(storyEvent);
                break;
            default:
                break;
        }
    }

    private void ContinueOfflineBackground(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Any())
        {
            videoBackground.SwitchBackground(currentEvent.Background);
            ContinueOfflineStory(currentEvent.Children.First());
        }
        else
        {
            // Should not happen
            Debug.Log("Story Event has no Children");
        }
    }

    private void ContinueOfflineDecisionOption(StoryEvent currentEvent)
    {
        displayStoryFlow.RemoveStoryFlowListeners();

        if (currentEvent.Children.Count() == 1)
        {
            ContinueOfflineStory(currentEvent.Children.First());
        }
        else if (gameStory.playThrough.CurrentEvent.Children.Count() > 1)
        {
            ContinueOfflineStory(gameStory.GetRandomOption(statusBar));
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

    private void ContinueOfflineStoryFlow(StoryEvent currentEvent)
    {
        displayStoryFlow.RemoveStoryFlowListeners();

        if (currentEvent.Children.Count > 0)
        {
            activeScreenManager.ShowStoryFlow();
            displayStoryFlow.SetStoryFlow(currentEvent);
            displayStoryFlow.storyFlowButton.onClick.AddListener(delegate
            {
                ContinueOfflineStory(currentEvent.Children.First());
            });
        }

    }
    */
}
