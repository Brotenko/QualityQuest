using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// TEST CLASS
/// </summary>
public class MenuTestClass
{
    public GameObject mainMenu;
    public GameObject audioMenu;
    public GameObject optionsMenu;
    public GameObject languageMenu;
    public GameObject displayMenu;
    public GameObject playOnline;


    public MenuTestClass()
    {
        mainMenu = GameObject.Instantiate(new GameObject());
        audioMenu = GameObject.Instantiate(new GameObject());
        optionsMenu = GameObject.Instantiate(new GameObject());
        languageMenu = GameObject.Instantiate(new GameObject());
        displayMenu = GameObject.Instantiate(new GameObject());
        playOnline = GameObject.Instantiate(new GameObject());
    }

    /// <summary>
    /// Makes sure that when the game is launched the main menu is displayed.
    /// </summary>
    public  void Start()
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
    public void PlayOfflineMode()
    {
        GameState.gameIsOnline = false;
        Debug.Log("Offlinemode enabled");
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    /// <summary>
    /// Makes the playOnline panel visible.
    /// </summary>
    public void PlayOnline()
    {
        GameState.gameIsOnline = true;
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
