using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject connect;
    public GameObject qrCode;
    public GameObject statistics;
    public QRCode qrCodeGenerator;
    public TMP_Text audienceCount;
    public TMP_Text websiteUrl;
    public TMP_Text sessionKey;

    private GameObject activeMenu;
    private bool paused = false;

    private void Awake()
    {
        Debug.Log("Game is online:" + Menu.gameIsOnline);
        HideAllMenus();
        if (Menu.gameIsOnline)
        {
            
            activeMenu = connect;
            connect.SetActive(true);
        }
        else
        {
            activeMenu = characterSelection;
            characterSelection.SetActive(true);
        }
    }

    public void HideAllMenus()
    {
        connect.SetActive(false);
        qrCode.SetActive(false);
        characterSelection.SetActive(false);
        decision.SetActive(false);
        storyflow.SetActive(false);
        result.SetActive(false);
        statistics.SetActive(false);
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
            qrCodeGenerator.GenerateQRCode(url);
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
