using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{

    public GameObject menu;
    public GameObject selectchar;
    public GameObject pauseMenu;

    public GameObject pauseIcon;
    public GameObject timer;
    public GameObject skills;

    public Sprite play;
    public Sprite pause;

    public Image pauseButton;

    public TMP_Text time;

    private bool gamePaused;
    private bool menuOpen;

    private void Start()
    {
        pauseMenu.SetActive(false);
        selectchar.SetActive(true);
        menu.SetActive(false);
        pauseIcon.SetActive(false);
        skills.SetActive(true);
        timer.SetActive(true);
        gamePaused = false;
        pauseButton.sprite = pause;
        menuOpen = false;
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
                selectchar.SetActive(true);
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
            selectchar.SetActive(false);

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
        selectchar.SetActive(true);
        pauseIcon.SetActive(false);
        //skills.SetActive(true);
        timer.SetActive(true);
    }
    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        selectchar.SetActive(false);
        pauseIcon.SetActive(true);
        //skills.SetActive(false);
        timer.SetActive(false);
    }


}
