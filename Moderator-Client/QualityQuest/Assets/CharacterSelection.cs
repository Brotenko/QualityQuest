using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Video;

public class CharacterSelection : MonoBehaviour
{

    public GameObject menu;
    public GameObject selectchar;
    public GameObject pauseMenu;
    public GameObject decision;
    public GameObject result;
    public GameObject storyflow;

    public VideoPlayer player;

    public GameObject pauseIcon;
    public GameObject timer;
    public GameObject skills;

    public Button storyflowbutton;
    public TMP_Text storyflowtext;

    public TMP_Text programming;
    public TMP_Text communication;
    public TMP_Text analytics;
    public TMP_Text party;

    public Sprite play;
    public Sprite pause;

    public Image pauseButton;

    public TMP_Text time;

    private bool gamePaused;
    private bool menuOpen;

    private Character playerCharacter;

    public static CharacterSelection current;

    private GameObject activeMenu;

    private void Start()
    {
        if (current == null)
        {
            current = this;
        }

        pauseMenu.SetActive(false);
        selectchar.SetActive(true);
        menu.SetActive(false);
        pauseIcon.SetActive(false);
        skills.SetActive(true);
        timer.SetActive(true);
        gamePaused = false;
        pauseButton.sprite = pause;
        menuOpen = false;
        activeMenu = selectchar;
    }

    private float timeRemaining = 60;

    /// <summary>
    /// Update timer
    /// </summary>
    void Update()
    {
        if (timeRemaining > 0 && !gamePaused)
        {
            timeRemaining -= Time.deltaTime;
        }
        time.text = ((int)timeRemaining).ToString();

    }

    public void UpdateSkills(Skills s)
    {
        analytics.text = s.getAnalytics().ToString();
        communication.text = s.getCommunication().ToString();
        party.text = s.getPartying().ToString();
        programming.text = s.getProgramming().ToString();
    }

    /// <summary>
    /// Shows and hides the ingame menu.
    /// </summary>
    public void DisplayMenu()
    {
        if (menu.activeSelf)
        {
            if (gamePaused)
            {
                ShowPauseMenu();
            }
            else
            {
                activeMenu.SetActive(true);
            }
            menu.SetActive(false);
            menuOpen = false;
        }
        else
        {
            if (gamePaused)
            {
                HidePauseMenu();
            }
            activeMenu.SetActive(false);

            menu.SetActive(true);
            menuOpen = true;
        }
    }

    /// <summary>
    /// Displays the pause screen if the menu is not open.
    /// </summary>
    public void PauseGame()
    {
        if (gamePaused)
        {
            if (!menuOpen)
            {
                HidePauseMenu();
            }
            pauseButton.sprite = pause;
            gamePaused = false;
            Debug.Log("game resumed");
        }
        else
        {
            if (!menuOpen)
            {
                ShowPauseMenu();
            }
            pauseButton.sprite = play;

            gamePaused = true;
            Debug.Log("game paused");
        }
    
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        activeMenu.SetActive(true);
        pauseIcon.SetActive(false);
        //skills.SetActive(true);
        timer.SetActive(true);
    }
    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        activeMenu.SetActive(false);
        pauseIcon.SetActive(true);
        //skills.SetActive(false);
        timer.SetActive(false);
    }

    public void InitializeCharacterNoruso()
    {
        playerCharacter = new Character(new Skills(3, 1, 2, 1), "Noruso");
        Story.playThrough.SetCharacter(playerCharacter);
        UpdateSkills(Story.playThrough.getCharacter().getAbilities());
        Story.current.PlayGame();
    }

    public void InitializeCharacterLumati()
    {
        playerCharacter = new Character(new Skills(1, 3, 0, 4), "Lumati");
        Story.playThrough.SetCharacter(playerCharacter);
        UpdateSkills(Story.playThrough.getCharacter().getAbilities());
        Story.current.PlayGame();
    }

    public void InitializeCharacterTurgal()
    {
        playerCharacter = new Character(new Skills(2, 2, 2, 2), "Turgal");
        Story.playThrough.SetCharacter(playerCharacter);
        UpdateSkills(Story.playThrough.getCharacter().getAbilities());
        Story.current.PlayGame();
    }

    public void InitializeCharacterKirogh()
    {
        playerCharacter = new Character(new Skills(2, 0, 5, 1), "Kirogh");
        Story.playThrough.SetCharacter(playerCharacter);
        UpdateSkills(Story.playThrough.getCharacter().getAbilities());
        Story.current.PlayGame();
    }

    private StoryEvent se;

    public void ShowDecision(StoryEvent currentEvent, HashSet<StoryEvent> children)
    {

        activeMenu = decision;

        se = null;

        Decision.current.LoadDecision(currentEvent, children);

        storyflow.SetActive(false);
        selectchar.SetActive(false);
        decision.SetActive(true);
        Debug.Log("ShowDecision");
        timeRemaining = 60;



}

    public void ShowStoryFlow(StoryEvent currentEvent, HashSet<StoryEvent> children)
    {

        activeMenu = storyflow;

        se = null;
        storyflowtext.text = currentEvent.GetDescription();
        decision.SetActive(false);
        selectchar.SetActive(false);
        storyflow.SetActive(true);
        storyflowbutton.onClick.RemoveAllListeners();
        storyflowbutton.onClick.AddListener(delegate { Pick(children.First()); });

    }


    public void SwitchBackground(VideoClip video)
    {
        player.clip = video;
    }

    public void Pick(StoryEvent e)
    {
        Debug.Log(e.GetChildren().Count);

        if (se == null)
        {
            se = e;
            Debug.Log("Selected: " + e.GetDescription());
            Story.current.SetCurrentEvent(e);
        }

    }


}
