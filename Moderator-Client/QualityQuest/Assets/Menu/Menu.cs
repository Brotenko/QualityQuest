using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject audioMenu;
    public GameObject optionsMenu;
    public GameObject languageMenu;
    public GameObject displayMenu;

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
    }

    /// <summary>
    /// Method to quit the application through the main menu.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}