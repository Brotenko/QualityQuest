using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ActiveScreenTest
{

    private ActiveScreenManager screenManager;
    private GameObject scriptHolderScreenManager;
    private GameObject scriptHolderQr;
    private QRCode qrCode;

    [SetUp]
    public void SetUp()
    {
        scriptHolderScreenManager = Object.Instantiate(new GameObject());
        screenManager = scriptHolderScreenManager.AddComponent<ActiveScreenManager>();

        scriptHolderQr = Object.Instantiate(new GameObject());
        qrCode = scriptHolderQr.AddComponent<QRCode>();

        screenManager.characterSelectionPanel = Object.Instantiate(new GameObject());
        screenManager.decisionPanel = Object.Instantiate(new GameObject());
        screenManager.storyFlowPanel = Object.Instantiate(new GameObject());
        screenManager.resultPanel = Object.Instantiate(new GameObject());
        screenManager.pauseScreenPanel = Object.Instantiate(new GameObject());
        screenManager.gameMenuPanel = Object.Instantiate(new GameObject());
        screenManager.connectPanel = Object.Instantiate(new GameObject());
        screenManager.qrCodePanel = Object.Instantiate(new GameObject());
        screenManager.statisticsPanel = Object.Instantiate(new GameObject());
        screenManager.errorScreenPanel = Object.Instantiate(new GameObject());
        screenManager.loadingScreenPanel = Object.Instantiate(new GameObject());
        screenManager.optionsPanel = Object.Instantiate(new GameObject());
        screenManager.pauseButtonPanel = Object.Instantiate(new GameObject());
        screenManager.gameCrashPanel = Object.Instantiate(new GameObject());
        screenManager.qrCodeGenerator = qrCode;
        screenManager.gameMenuSwitchModeButton = new GameObject().AddComponent<TextMeshPro>();
        screenManager.audienceCount = new GameObject().AddComponent<TextMeshPro>();
        screenManager.websiteUrl = new GameObject().AddComponent<TextMeshPro>();
        screenManager.sessionKey = new GameObject().AddComponent<TextMeshPro>();
        screenManager.pauseUrl = new GameObject().AddComponent<TextMeshPro>();
        screenManager.pauseKey = new GameObject().AddComponent<TextMeshPro>();
        screenManager.errorMessage = new GameObject().AddComponent<TextMeshPro>();
        qrCode.connectionPanel = Resources.Load<RawImage>("RawImage");
        qrCode.pausePanel = Resources.Load<RawImage>("RawImage");
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

    [UnityTest]
    public IEnumerator ShowErrorScreenTest()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        screenManager.ShowErrorScreen("Error 0815");
        screenManager.errorMessage.text = "Error0815";
        Assert.AreEqual(true, screenManager.errorScreenPanel.activeSelf);
        Assert.AreEqual("Error 0815", screenManager.errorMessage.text);
    }

    [Test]
    public void ShowCharacterSelectionTest()
    {
        screenManager.ShowCharacterSelection();
        Assert.AreEqual(true, screenManager.characterSelectionPanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator ShowQrCodePanelTest()
    {
        yield return new WaitForEndOfFrame();
        screenManager.ShowQrCodePanel("www.qualityquest.de", "1234");
        Assert.AreEqual(true, screenManager.qrCodePanel.activeSelf);
        Assert.AreEqual("1234", screenManager.pauseKey.text);
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

    [UnityTest]
    public IEnumerator ShowPauseMenuTest()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        screenManager.HideAllMenus();
        screenManager.ShowPauseMenu("www.qualityquest.de", "1234");
        Assert.AreEqual(true, screenManager.pauseScreenPanel.activeSelf);
        Assert.AreEqual("www.qualityquest.de", screenManager.pauseUrl.text);
        Assert.AreEqual("1234", screenManager.pauseKey.text);
    }

    [Test]
    public void ShowPauseButtonTest()
    {
        screenManager.ShowPauseButton(true);
        Assert.AreEqual(true, screenManager.pauseButtonPanel.activeSelf);
    }

    [Test]
    public void GameCrashTest()
    {
        screenManager.GameCrash();
        Assert.IsTrue(screenManager.errorScreenPanel.activeSelf);
    }

}