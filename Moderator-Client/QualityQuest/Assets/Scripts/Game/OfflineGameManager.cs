using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OfflineGameManager : MonoBehaviour
{

    public static OfflineGameManager current;

    public DisplayStatusbar statusBar;
    public CharacterSelection characterSelection;
    public GameStory story;
    public VideoBackground video;
    public DisplayDecision decision;
    public DisplayStoryFlow storyflow;
    public ActiveScreenManager screenmanager;

    public DisplayCharacter monster1;
    public DisplayCharacter monster2;
    public DisplayCharacter monster3;
    public DisplayCharacter monster4;

    public Button selectLumati;
    public Button selectTurgal;
    public Button selectKirogh;
    public Button selectNoruso;

    




    private void Awake()
    {
        
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else if (current != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        characterSelection.SetCharacters(monster1, monster2, monster3, monster4);
        
        selectNoruso.onClick.AddListener(delegate {
            PickNoruso();
        });

        selectLumati.onClick.AddListener(delegate {
            PickLumati();
        });

        selectTurgal.onClick.AddListener(delegate {
            PickTurgal();
        });

        selectKirogh.onClick.AddListener(delegate {
            PickKirogh();
        });
    }

    public void PickNoruso()
    {
        CharacterPickPhaseRemoveListeners();
        characterSelection.InitializeCharacter(characterSelection.noruso, story, statusBar);
        story.PlayGame();
    }

    public void PickLumati()
    {
        CharacterPickPhaseRemoveListeners();
        characterSelection.InitializeCharacter(characterSelection.lumati, story, statusBar);
        story.PlayGame();
    }

    public void PickTurgal()
    {
        CharacterPickPhaseRemoveListeners();
        characterSelection.InitializeCharacter(characterSelection.turgal, story, statusBar);
        story.PlayGame();
    }

    public void PickKirogh()
    {
        CharacterPickPhaseRemoveListeners();
        characterSelection.InitializeCharacter(characterSelection.kirogh, story, statusBar);
        story.PlayGame();
    }

    void CharacterPickPhaseRemoveListeners()
    {
        selectNoruso.onClick.RemoveAllListeners();
        selectLumati.onClick.RemoveAllListeners();
        selectTurgal.onClick.RemoveAllListeners();
        selectKirogh.onClick.RemoveAllListeners();
    }

    /*
    void PlayGame()
    {
        if (story.playThrough.getCurrentEvent().GetSkills() != null)
        {
            story.playThrough.getCharacter().GetAbilities().updateAbilities(story.playThrough.getCurrentEvent().GetSkills());
            statusBar.DisplaySkills(story.playThrough.getCharacter().GetAbilities());
            statusBar.UpdateSkillChanges(story.playThrough.getCurrentEvent().GetSkills());
        }

        Debug.Log(playThrough.getCurrentEvent().GetStoryType());

        if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryDecision))
        {
            OfflineGameManager.current.decision.LoadDecision(playThrough.getCurrentEvent(), playThrough.getCurrentEvent().GetChildren());
            OfflineGameManager.current.screenmanager.ShowDecision();
            Debug.Log("StoryDecision: " + playThrough.getCurrentEvent().GetDescription());
        }

        else if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryDecisionOption))
        {
            if (playThrough.getCurrentEvent().GetChildren().Count() > 0)
            {
                NextStoryEvent();
                Debug.Log("Option: " + playThrough.getCurrentEvent().GetDescription());
                this.PlayGame();
            }
            else
            {
                Debug.Log("Story Event has no Children");
            }
        }

        else if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryFlow))
        {
            if (playThrough.getCurrentEvent().GetChildren().Count() > 0)
            {
                Debug.Log("StoryFlow: " + playThrough.getCurrentEvent().GetDescription());
                OfflineGameManager.current.screenmanager.ShowStoryFlow();
                OfflineGameManager.current.storyflow.LoadStoryFlow(playThrough.getCurrentEvent());
                NextStoryEvent();


            }
            else
            {
                Debug.Log("Story Event has no Children");
            }
        }

        else if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryBackground))
        {
            if (playThrough.getCurrentEvent().GetChildren().Count() > 0)
            {
                OfflineGameManager.current.video.SwitchBackground(playThrough.getCurrentEvent().GetBackground());
                NextStoryEvent();
                this.PlayGame();
            }
            else
            {
                Debug.Log("Story Event has no Children");
            }
        }
    }
    */

}
