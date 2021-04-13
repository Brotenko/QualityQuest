using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScreenManager : MonoBehaviour
{

    public GameObject characterSelection;
    public GameObject decision;
    public GameObject storyflow;
    public GameObject result;
    public GameObject pauseScreen;
    public GameObject gameMenu;
    public GameObject gameInfo;

    private GameObject activeMenu;
    private bool paused = false;

    private void Awake()
    {
        activeMenu = characterSelection;
    }

    public void HideAllMenus()
    {
        characterSelection.SetActive(false);
        decision.SetActive(false);
        storyflow.SetActive(false);
        result.SetActive(false);
    }

    public void ShowCharacterSelection()
    {
        activeMenu = characterSelection;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            characterSelection.SetActive(true);
        }
    }

    public void ShowDecision()
    {
        activeMenu = decision;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            decision.SetActive(true);
        }
    }
    public void ShowStoryFlow()
    {
        activeMenu = storyflow;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            storyflow.SetActive(true);
        }
    }

    public void ShowPauseMenu()
    {

        if (!paused)
        {
            paused = true;

            if (!gameMenu.activeSelf)
            {
                HideAllMenus();
                pauseScreen.SetActive(true);
            }

        }
        else
        {
            paused = false;

            if (!gameMenu.activeSelf)
            {
                pauseScreen.SetActive(false);
                activeMenu.SetActive(true);
            }
        }
    }

    public void ShowGameMenu()
    {
        if (!gameMenu.activeSelf)
        {
            HideAllMenus();
            if (paused)
            {
                pauseScreen.SetActive(false);
            }
            gameMenu.SetActive(true);
        }
        else
        {
            gameMenu.SetActive(false);
            if (paused)
            {
                pauseScreen.SetActive(true);
            }
            else
            {
                activeMenu.SetActive(true);
            }

        }


    }


}
