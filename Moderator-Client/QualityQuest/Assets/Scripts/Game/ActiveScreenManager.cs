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

    /// <summary>
    /// All panels that the game switches through during a game.
    /// </summary>
    public GameObject characterSelection;
    public GameObject decision;
    public GameObject storyflow;
    public GameObject result;
    public GameObject pauseScreen;
    public GameObject gameMenu;
    public GameObject connect;
    public GameObject qrCode;
    public GameObject statistics;
    public GameObject errorScreen;
    public GameObject loadingScreen;

    /// <summary>
    /// Generates the QR code.
    /// </summary>
    public QRCode qrCodeGenerator;

    /// <summary>
    /// Text elements which are changed during the game.
    /// </summary>
    public TMP_Text gameMenuSwitchModeButton;
    public TMP_Text audienceCount;
    public TMP_Text websiteUrl;
    public TMP_Text sessionKey;
    public TMP_Text pauseUrl;
    public TMP_Text pauseKey;
    public TMP_Text errorMessage;

    /// <summary>
    /// Button which is used to pause the game.
    /// </summary>
    public GameObject pauseButton;

    /// <summary>
    /// This parameter helps to switch back to the correct screen after the pause screen or menu screen was shown.
    /// </summary>
    private GameObject activeMenu;

    /// <summary>
    /// This parameter helps to determine if the game si paused or not.
    /// </summary>
    public static bool paused;

    /// <summary>
    /// This method is called when the script is loaded
    /// when the game is run in the online mode the connect screen is shown
    /// otherwise the CharacterSelection screen is shown and the pause button is disable
    /// because the game can't be paused in offline mode.
    /// </summary>
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

    /// <summary>
    /// This method is called before the screen is changed
    /// to make sure that only one screen is active at once.
    /// </summary>
    public void HideAllMenus()
    {
        loadingScreen.SetActive(false);
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

    /// <summary>
    /// Sets the activeMenu parameter to connect
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the connect screen is displayed.
    /// </summary>
    public void ShowConnection()
    {
        GameState.gameIsOnline = true;
        activeMenu = connect;
        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            connect.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the activeMenu parameter to statistics
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the statistics screen is displayed.
    /// </summary>
    public void ShowStatistics()
    {
        activeMenu = statistics;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            statistics.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the activeMenu parameter to errorScreen
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the error screen is displayed.
    /// </summary>
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

    /// <summary>
    /// Sets the activeMenu parameter to characterSelection
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the character selection screen is displayed.
    /// </summary>
    public void ShowCharacterSelection()
    {
        activeMenu = characterSelection;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            characterSelection.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the activeMenu parameter to qrCode
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the QR code screen is displayed.
    /// Additionally the url, session key and qr code image get updated.
    /// </summary>
    public void ShowQrCodePanel(string url, string key)
    {
        activeMenu = qrCode;
        
        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            qrCode.SetActive(true);
            websiteUrl.text = url;
            sessionKey.text = key;
            qrCodeGenerator.GenerateQRCode(url, QrCodeType.QrCodeConnect);
            audienceCount.text = "Verbundene Spieler (0)";
            Debug.Log("3");
        }
    }

    /// <summary>
    /// Displays the current amount of connected player
    /// </summary>
    public void UpdateAudienceCount(int audienceCount)
    {
        this.audienceCount.text = "Verbundene Spieler (" + audienceCount.ToString() + ")";
    }

    /// <summary>
    /// Sets the activeMenu parameter to decision
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the decision screen is displayed.
    /// </summary>
    public void ShowDecision()
    {
        activeMenu = decision;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            decision.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the activeMenu parameter to StoryFlow
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are 
    /// not currently active
    /// HideAllMenus is called to make sure no other screen is active.
    /// and the StoryFlow screen is displayed.
    /// </summary>
    public void ShowStoryFlow()
    {
        activeMenu = storyflow;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            storyflow.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the activeMenu parameter to result
    /// which helps to switch back to the correct screen
    /// after the pause screen or menu screen was shown.
    /// If the pause or menu screen are not currently active
    /// HideAllMenus is called to make sure no other screen is active
    /// and the result screen is displayed.
    /// </summary>
    public void ShowResults()
    {
        activeMenu = result;

        if (!gameMenu.activeSelf && !paused)
        {
            HideAllMenus();
            result.SetActive(true);
        }
    }

    /// <summary>
    /// Method which displays and hides the pause screen and updates its content.
    /// </summary>
    /// <param name="url">Url which is used by the audience to connect to the server</param> 
    /// <param name="sessionKey">Key that has to be entered by the audience to enter the session</param> 
    public void ShowPauseMenu(string url, string sessionKey)
    {

        if (!paused)
        {
            //Sets the pause parameter to true which prevents all other screens except for the menu screen from getting shown.
            paused = true;


            /*
            If the menu screen is not currently active HideAllMenus is called to make sure no other screen is active
            and the pause screen is displayed. Additionally the session key and url gets updated and generateWRCode is called to display the qr code.
            */
            if (!gameMenu.activeSelf)
            {
                HideAllMenus();
                pauseScreen.SetActive(true);
                pauseKey.text = sessionKey;
                pauseUrl.text = url;
                qrCodeGenerator.GenerateQRCode(url, QrCodeType.QrCodePause);
            }

        }
        else
        {
            //Sets the pause parameter to false which won't prevent other screens from getting shown.
            paused = false;

            // If the menu screen in not currently active the pause screen gets hidden and the active screen is set be visible again.
            if (!gameMenu.activeSelf)
            {
                pauseScreen.SetActive(false);
                activeMenu.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Method which shows and hides the game menu.
    /// </summary>
    public void ShowGameMenu()
    {
        // changes the button which is used to switch between offline and online mode according to the currently active mode.
        if (GameState.gameIsOnline)
        {
            gameMenuSwitchModeButton.text = "Offline Mode";
        }
        else
        {
            gameMenuSwitchModeButton.text = "Online Mode";
        }

        // Hides all screens and displays the game menu.
        if (!gameMenu.activeSelf)
        {
            HideAllMenus();
            if (paused)
            {
                pauseScreen.SetActive(false);
            }
            gameMenu.SetActive(true);
        }
        // Hides the game menu. If the game is currently paused the pause menu is shown otherwise the active screen is displayed.
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

    /// <summary>
    /// Method to activate the pause button on the top right corner.
    /// </summary>
    public void ActivatePauseButton()
    {
        pauseButton.SetActive(true);
    }

    /// <summary>
    /// This method closes the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    /// <summary>
    /// Switches the active scene to display the main menu.
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
