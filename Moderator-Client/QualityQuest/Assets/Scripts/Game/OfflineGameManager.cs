using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = System.Random;

public class OfflineGameManager : MonoBehaviour
{
    public DisplayStatusbar statusBar;
    public CharacterSelection characterSelection;
    public GameStory story;
    public VideoBackground video;
    public DisplayDecision decision;
    public DisplayStoryFlow storyFlow;
    public ActiveScreenManager activeScreen;

    public DisplayCharacter monster1;
    public DisplayCharacter monster2;
    public DisplayCharacter monster3;
    public DisplayCharacter monster4;

    public Button storyFlowButton;
    public Button selectOfflineLumati;
    public Button selectOfflineTurgal;
    public Button selectOfflineKirogh;
    public Button selectOfflineNoruso;
    public Button selectOfflineA;
    public Button selectOfflineB;
    public Button selectOfflineC;
    public Button selectOfflineD;
    private Character a;


    void Awake()
    {
        selectOfflineKirogh.gameObject.SetActive(false);
        selectOfflineLumati.gameObject.SetActive(false);
        selectOfflineTurgal.gameObject.SetActive(false);
        selectOfflineNoruso.gameObject.SetActive(false);
    }

    void Start()
    {
        characterSelection.SetCharacters(monster1, monster2, monster3, monster4);

        if (!GameState.gameIsOnline)
        {
            StartOfflinePlaythrough();
        }
        else
        {

        }
    }


    public void StartOfflinePlaythrough()
    {
        activeScreen.ShowCharacterSelection();
        selectOfflineKirogh.gameObject.SetActive(true);
        selectOfflineLumati.gameObject.SetActive(true);
        selectOfflineTurgal.gameObject.SetActive(true);
        selectOfflineNoruso.gameObject.SetActive(true);

        selectOfflineNoruso.onClick.AddListener(delegate {
            PickNoruso();
        });

        selectOfflineLumati.onClick.AddListener(delegate {
            PickLumati();
        });

        selectOfflineTurgal.onClick.AddListener(delegate {
            PickTurgal();
        });

        selectOfflineKirogh.onClick.AddListener(delegate {
            PickKirogh();
        });
    }

    public void PickNoruso()
    {
        RemovePickListeners();
        characterSelection.InitializeCharacter(characterSelection.noruso, story, statusBar);
        var list = story.playThrough.CurrentEvent.Children.ToList();
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
        RemovePickListeners();
        characterSelection.InitializeCharacter(characterSelection.lumati, story, statusBar);
        var list = story.playThrough.CurrentEvent.Children.ToList();
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
        RemovePickListeners();
        characterSelection.InitializeCharacter(characterSelection.turgal, story, statusBar);
        var list = story.playThrough.CurrentEvent.Children.ToList();
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
        RemovePickListeners();
        characterSelection.InitializeCharacter(characterSelection.kirogh, story, statusBar);
        var list = story.playThrough.CurrentEvent.Children.ToList();
        if (GameState.gameIsOnline)
        {

        }
        else
        {
            ContinueOfflineStory(list[0]);
        }
    }

    void RemovePickListeners()
    {
        selectOfflineNoruso.onClick.RemoveAllListeners();
        selectOfflineLumati.onClick.RemoveAllListeners();
        selectOfflineTurgal.onClick.RemoveAllListeners();
        selectOfflineKirogh.onClick.RemoveAllListeners();
    }

    public void ContinueOfflineStory(StoryEvent storyEvent)
    {
        selectOfflineA.onClick.RemoveAllListeners();
        selectOfflineB.onClick.RemoveAllListeners();
        selectOfflineC.onClick.RemoveAllListeners();
        selectOfflineD.onClick.RemoveAllListeners();

        story.playThrough.CurrentEvent = storyEvent;

        if (storyEvent.SkillChange != null)
        {
            story.playThrough.Character.Abilities.updateAbilities(storyEvent.SkillChange);
            statusBar.DisplaySkills(story.playThrough.Character.Abilities);
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

    void ContinueOfflineBackground(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Any())
        {
            video.SwitchBackground(currentEvent.Background);
            ContinueOfflineStory(currentEvent.Children.First());
        }
        else
        {
            Debug.Log("Story Event has no Children");
        }
    }

    void ContinueOfflineDecisionOption(StoryEvent currentEvent)
    {
        if (currentEvent.Children.Count() == 1)
        {
            ContinueOfflineStory(currentEvent.Children.First());
        }
        else if (story.playThrough.CurrentEvent.Children.Count() > 1)
        {
            statusBar.DisplayDice(3);
            Random diceRoll = new Random();
            int rollTheDice = diceRoll.Next(0, 6);
            var children = story.playThrough.CurrentEvent.Children.ToList();
            switch (currentEvent.Children.First().Random)
            {
                case RandomType.DecisionFiveOne:
                    bool randomEventOne = rollTheDice + story.playThrough.Character.Abilities.Programming + 1 > 8;
                    if (randomEventOne == children[0].RandomOption)
                    {
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionFiveTwo:
                    var randomEventTwo = rollTheDice + story.playThrough.Character.Abilities.Programming - 1 > 8;
                    if (randomEventTwo == children[0].RandomOption)
                    {
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionEight:
                    var randomEventThree = rollTheDice + story.playThrough.Character.Abilities.Partying > 8;
                    if (randomEventThree == children[0].RandomOption)
                    {
                        Debug.Log(children[0].RandomOption);
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        Debug.Log(children[1].RandomOption);
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionEleven:
                    var randomEventFour = rollTheDice > 3;
                    if (randomEventFour == children[0].RandomOption)
                    {
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionThirteenOne:
                    var randomEventFive = rollTheDice <= 3;
                    if (randomEventFive == children[0].RandomOption)
                    {
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                case RandomType.DecisionThirteenTwo:
                    var randomEventSix = story.playThrough.Character.Abilities.Communication > 6;
                    if (randomEventSix == children[0].RandomOption)
                    {
                        ContinueOfflineStory(children[0]);
                    }
                    else
                    {
                        ContinueOfflineStory(children[1]);
                    }
                    break;
                default:
                    ContinueOfflineStory(story.playThrough.CurrentEvent.Children.First());
                    break;
            }
        }
    }

    void ContinueOfflineDecision(StoryEvent currentEvent)
    {
        var children = currentEvent.Children.ToList();
        decision.LoadDecision(currentEvent, children);
        activeScreen.ShowDecision();

        if (children.Count >= 2)
        {
            selectOfflineA.onClick.AddListener(delegate { ContinueOfflineStory(children[0]); });
            selectOfflineB.onClick.AddListener(delegate { ContinueOfflineStory(children[1]); });
        }

        if (children.Count >= 3)
        {
            selectOfflineC.onClick.AddListener(delegate { ContinueOfflineStory(children[2]); });
        }

        if (children.Count >= 4)
        {
            selectOfflineD.onClick.AddListener(delegate { ContinueOfflineStory(children[3]); });
        }
    
    }

    private void ContinueOfflineStoryFlow(StoryEvent currentEvent)
    {
        storyFlowButton.onClick.RemoveAllListeners();

        if (currentEvent.Children.Count > 0)
        {
            activeScreen.ShowStoryFlow();
            storyFlow.SetStoryFlow(currentEvent);
            storyFlowButton.onClick.AddListener(delegate
            {
                ContinueOfflineStory(currentEvent.Children.First());
            });
        }
    }
}
