using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ActiveScreenManagerTest
{
    private ActiveScreenManagerTestClass activeScreen;

    
    /// <summary>
    /// Setup.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        activeScreen = new ActiveScreenManagerTestClass();
        activeScreen.HideAllMenus();
    }

    /// <summary>
    /// Test for HideAllMenus method.
    /// </summary>
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

    /// <summary>
    /// Test for ShowErrorScreen method.
    /// </summary>
    [Test]
    public void ShowErrorScreenTest_NoActiveSelf()
    {
        Assert.IsNull(activeScreen.errorMessage.text);
        Assert.IsFalse(activeScreen.errorScreenPanel.activeSelf);
        activeScreen.ShowErrorScreen("Error 0815");
        Assert.IsTrue(activeScreen.errorScreenPanel.activeSelf);
        Assert.AreEqual("Error 0815", activeScreen.errorMessage.text);
    }

    /// <summary>
    /// Test for ShowErrorScreen method.
    /// </summary>
    [Test]
    public void ShowErrorScreenTest_ActiveSelf()
    {
        Assert.IsNull(activeScreen.errorMessage.text);
        Assert.IsFalse(activeScreen.errorScreenPanel.activeSelf);
        activeScreen.ShowErrorScreen("Error 0815");
        Assert.IsTrue(activeScreen.errorScreenPanel.activeSelf);
        Assert.AreEqual("Error 0815", activeScreen.errorMessage.text);
        activeScreen.ShowErrorScreen("Error 08");
        Assert.AreEqual("Error 0815", activeScreen.errorMessage.text);
    }

    /// <summary>
    /// Test for the ShowConnection will pause is not active.
    /// </summary>
    [Test]
    public void ShowConnectionTest()
    {
        GameState.gameIsOnline = false;
        Assert.IsFalse(activeScreen.connectPanel.activeSelf);
        activeScreen.ShowConnection();
        Assert.IsTrue(GameState.gameIsOnline);
        Assert.IsTrue(activeScreen.connectPanel.activeSelf);
    }

    [Test]
    public void ShowStatisticsTest()
    {
        Assert.IsFalse(activeScreen.statisticsPanel.activeSelf);
        activeScreen.ShowStatistics();
        Assert.IsTrue(activeScreen.statisticsPanel.activeSelf);
    }

    [Test]
    public void ShowCharacterSelectionTest()
    {
        Assert.IsFalse(activeScreen.characterSelectionPanel.activeSelf);
        activeScreen.ShowCharacterSelection();
        Assert.IsTrue(activeScreen.characterSelectionPanel.activeSelf);
    }

    [Test]
    public void ShowQrCodePanelTest()
    {
        Assert.IsNull(activeScreen.sessionKey.text);
        Assert.IsFalse(activeScreen.qrCodePanel.activeSelf);
        activeScreen.ShowQrCodePanel("www.qualityquest.de", "1234");
        Assert.IsTrue(activeScreen.qrCodePanel.activeSelf);
        Assert.AreEqual("1234", activeScreen.sessionKey.text);
    }

    [Test]
    public void UpdateAudienceCountTest()
    {
        Assert.IsNull(activeScreen.audienceCount.text);
        activeScreen.UpdateAudienceCount(99);
        Assert.AreEqual("Verbundene Spieler (99)" , activeScreen.audienceCount.text);
    }

    [Test]
    public void ShowOptionsTest()
    {
        Assert.IsFalse(activeScreen.optionsPanel.activeSelf);
        activeScreen.ShowOptions();
        Assert.IsTrue(activeScreen.optionsPanel.activeSelf);
    }

    [Test]
    public void ShowDecisionTest()
    {
        Assert.IsFalse(activeScreen.decisionPanel.activeSelf);
        activeScreen.ShowDecision();
        Assert.IsTrue(activeScreen.decisionPanel.activeSelf);
    }

    [Test]
    public void ShowStoryFlowTest()
    {
        Assert.IsFalse(activeScreen.storyFlowPanel.activeSelf);
        activeScreen.ShowStoryFlow();
        Assert.IsTrue(activeScreen.storyFlowPanel.activeSelf);
    }

    [Test]
    public void ShowResultsTest()
    {
        Assert.IsFalse(activeScreen.resultPanel.activeSelf);
        activeScreen.ShowResults();
        Assert.IsTrue(activeScreen.resultPanel.activeSelf);
    }

    [Test]
    public void ShowPauseMenuTest()
    {
        Assert.IsNull(activeScreen.pauseUrl.text);
        Assert.IsNull(activeScreen.pauseKey.text);
        activeScreen.pauseScreenPanel.SetActive(false);
        Assert.IsFalse(activeScreen.pauseScreenPanel.activeSelf);
        activeScreen.ShowPauseMenu("www.qualityquest.de", "1234");
        Assert.IsTrue( activeScreen.pauseScreenPanel.activeSelf);
    }

    [Test]
    public void ShowPauseButtonTest()
    {
        activeScreen.pauseButtonPanel.SetActive(false);
        Assert.IsFalse(activeScreen.pauseButtonPanel.activeSelf);
        activeScreen.ShowPauseButton(true);
        Assert.IsTrue(activeScreen.pauseButtonPanel.activeSelf);
    }

    [Test]
    public void GameCrashTest()
    {
        activeScreen.gameCrashPanel.SetActive(false);
        Assert.IsFalse(activeScreen.gameCrashPanel.activeSelf);
        activeScreen.GameCrash();
        Assert.IsTrue(activeScreen.gameCrashPanel.activeSelf);
    }

    /// <summary>
    /// Test for the ShowGameMenu method with the loadingPanel active. The game menu can not open.
    /// </summary>
    [Test]
    public void ShowGameMenuTest_LoadingPanelOn_GameIsOnline()
    {
        activeScreen.loadingScreenPanel.SetActive(true);
        Assert.IsFalse(activeScreen.gameMenuPanel.activeSelf);
        activeScreen.ShowGameMenu();
        Assert.IsFalse(activeScreen.gameMenuPanel.activeSelf);
    }

    [Test]
    public void SetPauseQrPanelTest()
    {
        activeScreen.pauseKey.text = "KEY";
        activeScreen.pauseUrl.text = "url";
        activeScreen.SetPauseQrPanel("www.newurl.de", "NEWKEY");
        Assert.AreEqual("NEWKEY",activeScreen.pauseKey.text);
        Assert.AreEqual("www.newurl.de", activeScreen.pauseUrl.text);
    }
}
