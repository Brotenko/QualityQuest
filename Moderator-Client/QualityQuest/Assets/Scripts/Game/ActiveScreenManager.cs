using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using MessageContainer;
using MessageContainer.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveScreenManager : MonoBehaviour
{

    public GameObject characterSelection;
    public GameObject decision;
    public GameObject storyflow;
    public GameObject result;
    public GameObject pauseScreen;
    public GameObject gameMenu;
    public GameObject gameInfo;
    public GameObject connect;
    public GameObject qrCode;
    public GameObject statistics;
    public GameObject errorScreen;
    public QRCode qrCodeGenerator;

    public TMP_Text gameMenuSwitchModeButton;
    public TMP_Text audienceCount;
    public TMP_Text websiteUrl;
    public TMP_Text sessionKey;
    public TMP_Text pauseUrl;
    public TMP_Text pauseKey;
    public TMP_Text errorMessage;
    public Button pauseButton;

    private GameObject activeMenu;
    public static bool paused;

    private void Awake()
    {
        Debug.Log("Game is online:" + GameState.gameIsOnline);
        HideAllMenus();
        if (GameState.gameIsOnline)
        {
            activeMenu = connect;
            connect.SetActive(true);
        }
        else
        {
            pauseButton.gameObject.SetActive(false);
            activeMenu = characterSelection;
            characterSelection.SetActive(true);
        }
    }

    public void HideAllMenus()
    {
        gameMenu.SetActive(false);
        errorScreen.SetActive(false);
        connect.SetActive(false);
        qrCode.SetActive(false);
        characterSelection.SetActive(false);
        decision.SetActive(false);
        storyflow.SetActive(false);
        result.SetActive(false);
        statistics.SetActive(false);
    }

    public void ShowConnection()
    {
        activeMenu = connect;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            connect.SetActive(true);
        }
    }

    public void ShowStatistics()
    {
        activeMenu = statistics;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            statistics.SetActive(true);
        }
    }

    public void ShowErrorScreen(string errorMessage)
    {
        activeMenu = errorScreen;

        if (!gameMenu.activeSelf)
        {
            HideAllMenus();
            errorScreen.SetActive(true);
            this.errorMessage.text = errorMessage;
        }
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

    public void ShowQrCodePanel(string url, string key)
    {
        activeMenu = qrCode;
        
        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            qrCode.SetActive(true);
            websiteUrl.text = url;
            sessionKey.text = key;
            qrCodeGenerator.GenerateQRCode(url,1);
            audienceCount.text = "Verbundene Spieler (0)";
            Debug.Log("3");
        }
    }

    public void UpdateAudienceCount(int audienceCount)
    {
        this.audienceCount.text = "Verbundene Spieler (" + audienceCount.ToString() + ")";
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

    public void ShowResults()
    {
        activeMenu = result;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            result.SetActive(true);
        }
    }

    public void ShowPauseMenu(string url, string sessionKey)
    {

        if (!paused)
        {
            paused = true;

            if (!gameMenu.activeSelf)
            {
                HideAllMenus();
                pauseScreen.SetActive(true);
                pauseKey.text = sessionKey;
                pauseUrl.text = url;
                qrCodeGenerator.GenerateQRCode(url, 2);
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
        if (GameState.gameIsOnline)
        {
            gameMenuSwitchModeButton.text = "Offline Mode";
        }
        else
        {
            gameMenuSwitchModeButton.text = "Online Mode";
        }

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

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
