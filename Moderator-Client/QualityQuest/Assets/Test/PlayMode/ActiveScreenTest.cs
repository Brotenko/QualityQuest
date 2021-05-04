using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class ActiveScreenTest
{

    private ActiveScreenManager screenManager;
    private GameObject scriptHolder;

    [SetUp]
    public void SetUp()
    {
        scriptHolder = GameObject.Instantiate(new GameObject());
        screenManager = scriptHolder.AddComponent<ActiveScreenManager>();
        //screenManager = new ActiveScreenManager();
        screenManager.characterSelectionPanel = GameObject.Instantiate(new GameObject());
        screenManager.decisionPanel = GameObject.Instantiate(new GameObject());
        screenManager.storyFlowPanel = GameObject.Instantiate(new GameObject());
        screenManager.resultPanel = GameObject.Instantiate(new GameObject());
        screenManager.pauseScreenPanel = GameObject.Instantiate(new GameObject());
        screenManager.gameMenuPanel = GameObject.Instantiate(new GameObject());
        screenManager.connectPanel = GameObject.Instantiate(new GameObject());
        screenManager.qrCodePanel = GameObject.Instantiate(new GameObject());
        screenManager.statisticsPanel = GameObject.Instantiate(new GameObject());
        screenManager.errorScreenPanel = GameObject.Instantiate(new GameObject());
        screenManager.loadingScreenPanel = GameObject.Instantiate(new GameObject());
        screenManager.optionsPanel = GameObject.Instantiate(new GameObject());
        screenManager.pauseButtonPanel = GameObject.Instantiate(new GameObject());
        screenManager.gameCrashPanel = GameObject.Instantiate(new GameObject());

        screenManager.qrCodeGenerator = new QRCode();

        screenManager.gameMenuSwitchModeButton = new GameObject().AddComponent<TextMeshPro>();
        screenManager.audienceCount = new GameObject().AddComponent<TextMeshPro>();
        screenManager.websiteUrl = new GameObject().AddComponent<TextMeshPro>();
        screenManager.sessionKey = new GameObject().AddComponent<TextMeshPro>();
        screenManager.pauseUrl = new GameObject().AddComponent<TextMeshPro>();
        screenManager.pauseKey = new GameObject().AddComponent<TextMeshPro>();
        screenManager.errorMessage = new GameObject().AddComponent<TextMeshPro>();

    }

    [Test]
    public void HideAllMenusTest()
    {

        screenManager.HideAllMenus();
        Assert.AreEqual(false, screenManager.loadingScreenPanel.activeSelf);
        Assert.AreEqual(false, screenManager.gameMenuPanel.activeSelf);
        Assert.AreEqual(false, screenManager.errorScreenPanel.activeSelf);
        Assert.AreEqual(false, screenManager.connectPanel.activeSelf);
        Assert.AreEqual(false, screenManager.qrCodePanel.activeSelf);
        Assert.AreEqual(false, screenManager.characterSelectionPanel.activeSelf);
        Assert.AreEqual(false, screenManager.decisionPanel.activeSelf);
        Assert.AreEqual(false, screenManager.storyFlowPanel.activeSelf);
        Assert.AreEqual(false, screenManager.resultPanel.activeSelf);
        Assert.AreEqual(false, screenManager.statisticsPanel.activeSelf);
        Assert.AreEqual(false, screenManager.optionsPanel.activeSelf);

    }

    [Test]
    public void ShowConnectionTest()
    {

        screenManager.ShowConnection();
        Assert.AreEqual(true, screenManager.connectPanel.activeSelf);

    }

    [Test]
    public void ShowStatisticsTest()
    {

        screenManager.ShowStatistics();
        Assert.AreEqual(true, screenManager.statisticsPanel.activeSelf);

    }

    [Test]
    public void ShowErrorScreenTest()
    {

        screenManager.ShowErrorScreen("Error 0815");
        Assert.AreEqual(true, screenManager.errorScreenPanel.activeSelf);
        //Assert.AreEqual("Error 0815", screenManager.errorMessage.text);

    }

    [Test]
    public void ShowCharacterSelectionTest()
    {

        screenManager.ShowCharacterSelection();
        Assert.AreEqual(true, screenManager.characterSelectionPanel.activeSelf);
        //Assert.AreEqual(screenManager.characterSelectionPanel,screenManager.activeMenu);

    }

    [Test]
    public void ShowQrCodePanelTest()
    {

        screenManager.ShowQrCodePanel("www.qualityquest.de", "1234");
        Assert.AreEqual(true, screenManager.qrCodePanel.activeSelf);
        //Assert.AreEqual("1234", screenManager.pauseKey.text);

    }

    [Test]
    public void UpdateAudienceCountTest()
    {

        screenManager.UpdateAudienceCount(99);

    }

    [Test]
    public void ShowOptionsTest()
    {

        screenManager.ShowOptions();
        Assert.AreEqual(true, screenManager.optionsPanel.activeSelf);

    }

    [Test]
    public void ShowDecisionTest()
    {

        screenManager.ShowDecision();
        Assert.AreEqual(true, screenManager.decisionPanel.activeSelf);

    }

    [Test]
    public void ShowStoryFlowTest()
    {

        screenManager.ShowStoryFlow();
        Assert.AreEqual(true, screenManager.storyFlowPanel.activeSelf);

    }

    [Test]
    public void ShowResultsTest()
    {

        screenManager.ShowResults();

    }

    [Test]
    public void ShowPauseMenuTest()
    {

        screenManager.ShowPauseMenu("www.qualityquest.de", "1234");
        Assert.AreEqual(true, screenManager.pauseScreenPanel.activeSelf);

    }

    /*
    [Test]
    public void ShowGameMenuTest()
    {
        screenManager.gameMenuPanel.SetActive(true);
        screenManager.ShowGameMenu();
        Assert.AreEqual(true, screenManager.gameMenuPanel.activeSelf);

    }
    */

    [Test]
    public void ShowPauseButtonTest()
    {

        screenManager.ShowPauseButton(true);
        Assert.AreEqual(true, screenManager.pauseButtonPanel.activeSelf);

    }

    [Test]
    public void QuitGameTest()
    {

        screenManager.QuitGame();

    }

    [Test]
    public void BackToMainMenuTest()
    {

        screenManager.BackToMainMenu();

    }

    [Test]
    public void GameCrashTest()
    {

        screenManager.GameCrash();
        Assert.AreEqual(true, screenManager.errorScreenPanel.activeSelf);

    }

    [TearDown]
    public virtual void TearDown()
    {
        Object.DestroyImmediate(scriptHolder);
    }

}