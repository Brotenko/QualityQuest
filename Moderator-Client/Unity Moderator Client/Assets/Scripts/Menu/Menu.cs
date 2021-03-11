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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowMainMenu()
    {
        HideAllMenu();
        mainMenu.SetActive(true);
    }

    public void ShowAudioMenu()
    {
        HideAllMenu();
        audioMenu.SetActive(true);
    }

    public void ShowOptionsMenu()
    {
        HideAllMenu();
        optionsMenu.SetActive(true);
    }

    public void ShowLanguageMenu()
    {
        HideAllMenu();
        languageMenu.SetActive(true);
    }

    public void ShowDisplayMenu()
    {
        HideAllMenu();
        displayMenu.SetActive(true);
    }

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
