using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveScreenManager : MonoBehaviour
{

    /// <summary>
    /// All panels that the game switches through during a game.
    /// </summary>
    public GameObject characterSelectionPanel;
    public GameObject decisionPanel;
    public GameObject storyFlowPanel;
    public GameObject resultPanel;
    public GameObject pauseScreenPanel;
    public GameObject gameMenuPanel;
    public GameObject connectPanel;
    public GameObject qrCodePanel;
    public GameObject statisticsPanel;
    public GameObject errorScreenPanel;
    public GameObject loadingScreenPanel;
    public GameObject optionsPanel;
    public GameObject pauseButtonPanel;
    public GameObject gameCrashPanel;

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
    /// This parameter helps to switch back to the correct screen after the pause screen or menu screen was shown.
    /// </summary>
    private GameObject activeMenu;

    /// <summary>
    /// This parameter helps to determine if the game is paused or not.
    /// </summary>
    public static bool paused;

    /// <summary>
    /// This method is called when the script is loaded
    /// when the game is run in the online mode the connect screen is shown
    /// otherwise the CharacterSelection screen is shown and the pause button is disable
    /// because the game can't be paused in offline mode.
    /// </summary>
    private void Start()
    {
        Debug.Log("Game is online:" + GameState.gameIsOnline);
        HideAllMenus();
        if (GameState.gameIsOnline)
        {
            activeMenu = connectPanel;
            connectPanel.SetActive(true);
        }
        else
        {
            pauseButtonPanel.gameObject.SetActive(false);
            activeMenu = characterSelectionPanel;
            characterSelectionPanel.SetActive(true);
        }
    }

    /// <summary>
    /// This method is called before the screen is changed
    /// to make sure that only one screen is active at once.
    /// </summary>
    public void HideAllMenus()
    {
        loadingScreenPanel.SetActive(false);
        gameMenuPanel.SetActive(false);
        errorScreenPanel.SetActive(false);
        connectPanel.SetActive(false);
        qrCodePanel.SetActive(false);
        characterSelectionPanel.SetActive(false);
        decisionPanel.SetActive(false);
        storyFlowPanel.SetActive(false);
        resultPanel.SetActive(false);
        statisticsPanel.SetActive(false);
        optionsPanel.SetActive(false);
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
        activeMenu = connectPanel;
        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        connectPanel.SetActive(true);
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
        activeMenu = statisticsPanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        statisticsPanel.SetActive(true);
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
        activeMenu = errorScreenPanel;

        if (errorScreenPanel.activeSelf) return;
        HideAllMenus();
        errorScreenPanel.SetActive(true);
        this.errorMessage.text = errorMessage;
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
        activeMenu = characterSelectionPanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        characterSelectionPanel.SetActive(true);
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
        activeMenu = qrCodePanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        qrCodePanel.SetActive(true);
        websiteUrl.text = url;
        sessionKey.text = key;
        qrCodeGenerator.GenerateQRCode(url, QrCodeType.QrCodeConnect);
        audienceCount.text = "Verbundene Spieler (0)";
    }

    /// <summary>
    /// Displays the current amount of connected player
    /// </summary>
    public void UpdateAudienceCount(int audienceCount)
    {
        this.audienceCount.text = "Verbundene Spieler (" + audienceCount.ToString() + ")";
    }

    /// <summary>
    /// HideAllMenus is called to make sure no other screen is active
    /// and the options screen is displayed.
    /// </summary>
    public void ShowOptions()
    {
        HideAllMenus();
        optionsPanel.SetActive(true);
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
        activeMenu = decisionPanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        decisionPanel.SetActive(true);
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
        activeMenu = storyFlowPanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        storyFlowPanel.SetActive(true);
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
        activeMenu = resultPanel;

        if (gameMenuPanel.activeSelf || paused) return;
        HideAllMenus();
        resultPanel.SetActive(true);
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
            and the pause screen is displayed. Additionally the session key and url gets updated and generateQrCode is called to display the qr code.
            */
            if (gameMenuPanel.activeSelf) return;
            HideAllMenus();
            pauseScreenPanel.SetActive(true);
            pauseKey.text = sessionKey;
            pauseUrl.text = url;
            qrCodeGenerator.GenerateQRCode(url, QrCodeType.QrCodePause);

        }
        else
        {
            //Sets the pause parameter to false which won't prevent other screens from getting shown.
            paused = false;

            // If the menu screen in not currently active the pause screen gets hidden and the active screen is set be visible again.
            if (gameMenuPanel.activeSelf) return;
            pauseScreenPanel.SetActive(false);
            activeMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Method which shows and hides the game menu.
    /// </summary>
    public void ShowGameMenu()
    {
        // changes the button which is used to switch between offline and online mode according to the currently active mode.
        gameMenuSwitchModeButton.text = GameState.gameIsOnline ? "Offline Mode" : "Online Mode";

        // cant open while loading the game
        if (loadingScreenPanel.activeSelf) return;
        // Hides all screens and displays the game menu.
        if (!gameMenuPanel.activeSelf)
        {
            HideAllMenus();
            if (paused)
            {
                pauseScreenPanel.SetActive(false);
            }

            gameMenuPanel.SetActive(true);
        }
        // Hides the game menu. If the game is currently paused the pause menu is shown otherwise the active screen is displayed.
        else
        {
            gameMenuPanel.SetActive(false);
            if (paused)
            {
                pauseScreenPanel.SetActive(true);
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
    public void ShowPauseButton(bool activate)
    {
        pauseButtonPanel.SetActive(activate);
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

    /// <summary>
    /// Method if the game crashes, e.g. a StoryEvent is null.
    /// </summary>
    public void GameCrash()
    {
        gameCrashPanel.SetActive(true);
    }
}
