using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ActiveScreenManagerTest
{
    private ActiveScreenManagerTestClass activeScreen;

    // A Test behaves as an ordinary method
    [SetUp]
    public void SetUp()
    {
        activeScreen = new ActiveScreenManagerTestClass();
        activeScreen.HideAllMenus();
    }


    [Test]
    public void HideAllMenusTest()
    {
        activeScreen.loadingScreenPanel.SetActive(true);
        activeScreen.gameMenuPanel.SetActive(true);
        activeScreen.errorScreenPanel.SetActive(true);
        activeScreen.connectPanel.SetActive(true);
        activeScreen.qrCodePanel.SetActive(true);
        activeScreen.characterSelectionPanel.SetActive(true);
        activeScreen.decisionPanel.SetActive(true);
        activeScreen.storyFlowPanel.SetActive(true);
        activeScreen.resultPanel.SetActive(true);
        activeScreen.statisticsPanel.SetActive(true);
        activeScreen.optionsPanel.SetActive(true);
        activeScreen.HideAllMenus();
        Assert.AreEqual(false, activeScreen.loadingScreenPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.gameMenuPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.errorScreenPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.connectPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.qrCodePanel.activeSelf);
        Assert.AreEqual(false, activeScreen.characterSelectionPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.decisionPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.storyFlowPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.resultPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.statisticsPanel.activeSelf);
        Assert.AreEqual(false, activeScreen.optionsPanel.activeSelf);
    }

    [Test]
    public void ShowErrorScreenTest()
    {
        Assert.IsFalse(activeScreen.errorScreenPanel.activeSelf);
        activeScreen.ShowErrorScreen("Error 0815");
        Assert.AreEqual(true, activeScreen.errorScreenPanel.activeSelf);
        Assert.AreEqual("Error 0815", activeScreen.errorMessage.text);
    }
}
