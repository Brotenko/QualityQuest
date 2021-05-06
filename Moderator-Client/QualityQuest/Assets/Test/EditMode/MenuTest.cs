using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MenuTest
{
    private MenuTestClass menu;
    /// <summary>
    /// SetUp.
    /// </summary>
    [SetUp]
    public void MenuTestSimplePasses()
    {
        menu = new MenuTestClass();
        menu.mainMenu.SetActive(false);
        menu.audioMenu.SetActive(false);
        menu.optionsMenu.SetActive(false);
        menu.languageMenu.SetActive(false);
        menu.displayMenu.SetActive(false);
        menu.playOnline.SetActive(false);
    }

    /// <summary>
    /// Test for the Start method.
    /// </summary>
    [Test]
    public void StartTest()
    {
        Assert.IsFalse(menu.mainMenu.activeSelf);
        menu.Start();
        Assert.IsTrue(menu.mainMenu.activeSelf);
    }

    /// <summary>
    /// Test for the ShowMainMenuTest.
    /// </summary>
    [Test]
    public void ShowMainMenuTest()
    {
        Assert.IsFalse(menu.mainMenu.activeSelf);
        menu.ShowMainMenu();
        Assert.IsTrue(menu.mainMenu.activeSelf);
    }

    /// <summary>
    /// Test for the ShowAudioMenu method.
    /// </summary>
    [Test]
    public void ShowAudioMenu()
    {
        Assert.IsFalse(menu.audioMenu.activeSelf);
        menu.ShowAudioMenu();
        Assert.IsTrue(menu.audioMenu.activeSelf);
    }

    /// <summary>
    /// Test for the ShowOptionsMenu method.
    /// </summary>
    [Test]
    public void ShowOptionsMenuTest()
    {
        Assert.IsFalse(menu.optionsMenu.activeSelf);
        menu.ShowOptionsMenu();
        Assert.IsTrue(menu.optionsMenu.activeSelf);
    }

    /// <summary>
    /// Test for the ShowLanguageMenu method.
    /// </summary>
    [Test]
    public void ShowLanguageMenuTest()
    {
        Assert.IsFalse(menu.languageMenu.activeSelf);
        menu.ShowLanguageMenu();
        Assert.IsTrue(menu.languageMenu.activeSelf);
    }

    /// <summary>
    /// Test for the ShowDisplayMenu method.
    /// </summary>
    [Test]
    public void ShowDisplayMenu()
    {
        Assert.IsFalse(menu.displayMenu.activeSelf);
        menu.ShowDisplayMenu();
        Assert.IsTrue(menu.displayMenu.activeSelf);
    }
    /// <summary>
    /// Test for the HideAllMenu method.
    /// </summary>
    public void HideAllMenuTest()
    {
        menu.mainMenu.SetActive(true);
        menu.audioMenu.SetActive(true);
        menu.optionsMenu.SetActive(true);
        menu.languageMenu.SetActive(true);
        menu.displayMenu.SetActive(true);
        menu.playOnline.SetActive(true);
        menu.HideAllMenu();
        Assert.IsFalse(menu.mainMenu.activeSelf);
        Assert.IsFalse(menu.audioMenu.activeSelf);
        Assert.IsFalse(menu.optionsMenu.activeSelf);
        Assert.IsFalse(menu.languageMenu.activeSelf);
        Assert.IsFalse(menu.displayMenu.activeSelf);
        Assert.IsFalse(menu.playOnline.activeSelf);
    }
}
