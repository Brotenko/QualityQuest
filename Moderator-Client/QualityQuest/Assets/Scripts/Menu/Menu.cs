using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    public static bool offlineMode;
    public TMP_InputField ip;
    public TMP_InputField port;
    public TMP_InputField password;


    public GameObject mainMenu;
    public GameObject audioMenu;
    public GameObject optionsMenu;
    public GameObject languageMenu;
    public GameObject displayMenu;
    public GameObject playOnline;

    /// <summary>
    /// Makes sure that when the game is launched the main menu is displayed.
    /// </summary>
    void Start()
    {
        HideAllMenu();
        ShowMainMenu();
    }

    /// <summary>
    /// Makes the main menu panel visible.
    /// </summary>
    public void ShowMainMenu()
    {
        HideAllMenu();
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// Makes the audio volume panel visible.
    /// </summary>
    public void ShowAudioMenu()
    {
        HideAllMenu();
        audioMenu.SetActive(true);
    }

    /// <summary>
    /// Makes the options submenu visible.
    /// </summary>
    public void ShowOptionsMenu()
    {
        HideAllMenu();
        optionsMenu.SetActive(true);
    }

    /// <summary>
    /// Makes the language selection panel visible.
    /// </summary>
    public void ShowLanguageMenu()
    {
        HideAllMenu();
        languageMenu.SetActive(true);
    }

    /// <summary>
    /// Makes the display settings panel visible.
    /// </summary>
    public void ShowDisplayMenu()
    {
        HideAllMenu();
        displayMenu.SetActive(true);
    }

    /// <summary>
    /// Method that hides all menu panels.
    /// </summary>
    public void HideAllMenu()
    {
        audioMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        languageMenu.SetActive(false);
        displayMenu.SetActive(false);
        playOnline.SetActive(false);
    }

    /// <summary>
    /// Method to quit the application through the main menu.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    /// <summary>
    /// Method to set the offlineMode through the main menu.
    /// </summary>
    public void SetOfflineMode()
    {
        offlineMode = true;
        Debug.Log("Offlinemode enabled");
        StartGame();
    }

    /// <summary>
    /// Makes the playOnline panel visible.
    /// </summary>
    public void PlayOnline()
    {
        HideAllMenu();
        playOnline.SetActive(true);
    }

    /// <summary>
    /// Method to start the game through the main menu.
    /// </summary>
    public void StartGame()
    {
        if (offlineMode)
        {
            Story.InitializeStoryGraph();
        } 
        else
        {

        }
    }


    /************************ Method for test porpose ************************/

    public void StartTestScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
