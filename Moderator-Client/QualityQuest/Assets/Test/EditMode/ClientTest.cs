using System;
using System.Collections.Generic;
using MessageContainer.Messages;
using NUnit.Framework;

/// <summary>
/// Mostly integration tests.
/// </summary>
public class ClientTest
{
    private ClientTestClass client;
    private Character testCharacter;

    /// <summary>
    /// SetUp.
    /// </summary>
    [SetUp]
    public void ClientTestSimplePasses()
    {
        client = new ClientTestClass();
        client.activeScreenManager.HideAllMenus();
        client.activeScreenManager.loadingScreenPanel.SetActive(false);
        GameState.gameIsOnline = false;
        client.clientLogic.StoryGraph = new StoryGraph(null, null, null);
        client.characterSelection.selectOfflineKirogh.interactable = false;
        client.characterSelection.selectOfflineTurgal.interactable = false;
        client.characterSelection.selectOfflineLumati.interactable = false;
        client.characterSelection.selectOfflineNoruso.interactable = false;
    }

    /// <summary>
    /// Test for Start method.
    /// </summary>
    [Test]
    public void StartTest()
    {
        // Set buttons not intractable
        client.characterSelection.selectOfflineKirogh.interactable = false;
        client.characterSelection.selectOfflineTurgal.interactable = false;
        client.characterSelection.selectOfflineLumati.interactable = false;
        client.characterSelection.selectOfflineNoruso.interactable = false;

        // start test
        Assert.IsFalse(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsFalse(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineNoruso.interactable);
        client.Start();
        Assert.IsTrue(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsTrue(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineNoruso.interactable);
    }

    /// <summary>
    /// Test for the StartOfflinePlaythrough method.
    /// </summary>
    [Test]
    public void StartOfflinePlayThroughTest()
    {
        // Set buttons not intractable
        client.characterSelection.selectOfflineKirogh.interactable = false;
        client.characterSelection.selectOfflineTurgal.interactable = false;
        client.characterSelection.selectOfflineLumati.interactable = false;
        client.characterSelection.selectOfflineNoruso.interactable = false;

        // start test
        Assert.IsFalse(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsFalse(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineNoruso.interactable);
        client.StartOfflinePlaythrough();
        Assert.IsTrue(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsTrue(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineNoruso.interactable);
    }


    /// <summary>
    /// Test for the ReceivedGamePausedStatusChangeTest
    /// </summary>
    [Test]
    public void ReceivedGamePausedStatusChangeTest()
    {
        //Setup
        client.activeScreenManager.pauseScreenPanel.SetActive(false);
        client.clientLogic.Url = "www.test.unity";
        client.clientLogic.SessionKey = "NICEKEY";
        client.activeScreenManager.HideAllMenus();
        client.activeScreenManager.ShowCharacterSelection();
        var testMessage = new GamePausedStatusMessage(Guid.NewGuid(), false);

        //Test
        Assert.IsTrue(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsFalse(ActiveScreenManagerTestClass.paused);
        Assert.IsFalse(client.activeScreenManager.pauseScreenPanel.activeSelf);
        client.ReceivedGamePausedStatusChange(testMessage);
        Assert.IsFalse(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsTrue(ActiveScreenManagerTestClass.paused);
        Assert.IsTrue(client.activeScreenManager.pauseScreenPanel.activeSelf);
        

        // Second Message
        client.ReceivedGamePausedStatusChange(testMessage);
        Assert.IsTrue(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsFalse(ActiveScreenManagerTestClass.paused);
        Assert.IsFalse(client.activeScreenManager.pauseScreenPanel.activeSelf);
    }

    /// <summary>
    /// Test for the ReceivedReconnectSuccessfulMessage method.
    /// </summary>
    [Test]
    public void ReceivedReconnectSuccessfulMessage()
    {
        //Setup
        GameState.gameIsOnline = false;
        client.activeScreenManager.pauseButtonPanel.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(null, "haha", null);
        root.AddChild(child1);
        var testMessage = new ReconnectSuccessfulMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        // Test
        client.ReceivedReconnectSuccessfulMessage(testMessage);
        Assert.IsTrue(client.activeScreenManager.pauseButtonPanel.activeSelf);
        Assert.IsTrue(GameState.gameIsOnline);
        Assert.IsTrue(client.activeScreenManager.storyFlowPanel.activeSelf);
        Assert.AreEqual("root",client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for ReceivedSessionOpenedMessage method.
    /// </summary>
    [Test]
    public void ReceivedSessionOpenedMessage()
    {
        //Setup
        GameState.gameIsOnline = false;
        Assert.IsNull(client.clientLogic.Url);
        Assert.IsNull(client.clientLogic.SessionKey);
        Assert.IsFalse(client.activeScreenManager.qrCodePanel.activeSelf);
        var testMessage = new SessionOpenedMessage(Guid.NewGuid(), "KEYKEY", new Uri("https://www.google.de/"));

        //Test
        client.ReceivedSessionOpenedMessage(testMessage);
        Assert.AreEqual("KEYKEY", client.clientLogic.SessionKey);
        Assert.AreEqual("https://www.google.de/", client.clientLogic.Url);
        Assert.IsTrue(client.activeScreenManager.qrCodePanel.activeSelf);
        Assert.AreEqual("https://www.google.de/", client.activeScreenManager.websiteUrl.text);
        Assert.AreEqual("KEYKEY", client.activeScreenManager.sessionKey.text);
    }

    /// <summary>
    /// Test for the ReceivedGameStartedMessage method.
    /// </summary>
    [Test]
    public void ReceivedGameStartedMessage()
    {
        //Setup
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(null, "haha", null);
        root.AddChild(child1);
        var testMessage = new GameStartedMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        // Test
        client.ReceivedGameStartedMessage(testMessage);
        Assert.IsTrue(client.activeScreenManager.storyFlowPanel.activeSelf);
        Assert.AreEqual("root", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStoryTest method.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_StoryFlowWithoutSkillChange()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1,1,1,1), "haha", null);
        root.AddChild(child1);
        var testMessage = new ReconnectSuccessfulMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        // Test
        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOfflineStory(root);
        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("root",client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStoryTest method.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_StoryFlowWithSkillChange()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1,1,1,1));
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        var testMessage = new ReconnectSuccessfulMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        // Test
        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOfflineStory(root);
        Assert.AreEqual(2, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("root", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOnlineStoryTest method.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_StoryFlowWithoutSkillChange()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        var testMessage = new ReconnectSuccessfulMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        // Test
        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("root", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStoryTest method.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_StoryFlowWithSkillChange()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 1, 1, 1));
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        var testMessage = new ReconnectSuccessfulMessage(Guid.NewGuid());
        client.clientLogic.StoryGraph = new StoryGraph(testCharacter, root, root);

        Assert.AreEqual(1, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        // Test
        client.ContinueOnlineStory(root);
        Assert.AreEqual(2, client.clientLogic.StoryGraph.Character.Abilities.Programming);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("root", client.displayStoryFlow.storyflowText.text);
    }


    [Test]
    public void ContinueOfflineStoryTest_StoryRoot()
    {
        GameState.gameIsOnline = false;
        client.characterSelection.selectOfflineKirogh.interactable = false;
        client.characterSelection.selectOfflineTurgal.interactable = false;
        client.characterSelection.selectOfflineLumati.interactable = false;
        client.characterSelection.selectOfflineNoruso.interactable = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryRootEvent);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child2 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child3 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var child4 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        root.AddChild(child2);
        root.AddChild(child3);
        root.AddChild(child4);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsFalse(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsFalse(client.characterSelection.selectOfflineNoruso.interactable);
        client.ContinueOfflineStory(root);
        Assert.IsTrue(client.activeScreenManager.characterSelectionPanel.activeSelf);
        Assert.IsTrue(client.characterSelection.selectOfflineKirogh.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineTurgal.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineLumati.interactable);
        Assert.IsTrue(client.characterSelection.selectOfflineNoruso.interactable);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the workshop event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_Workshop()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "workshop", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshop);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOfflineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("workshop", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOnlineStory method with the workshop event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_Workshop()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "workshop", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshop);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("workshop", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the fired event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_FiredEvent()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "fired", new HashSet<StoryEvent>(), StoryEventType.StoryFired);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOfflineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("fired", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the fired event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_FiredEvent()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "fired", new HashSet<StoryEvent>(), StoryEventType.StoryFired);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("fired", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOnlineStory method with the no workshop invite event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_NoWorkshopEvent()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "noworkshop", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshop);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("noworkshop", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the no workshop invite event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_NoWorkshopEvent()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "noworkshop", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopNoInvite);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("noworkshop", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the no workshop invite event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_StoryEnd()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "the end is coming", new HashSet<StoryEvent>(), StoryEventType.StoryEnd);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOfflineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("the end is coming", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with the no workshop invite event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_StoryEnd()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "the end is coming", new HashSet<StoryEvent>(), StoryEventType.StoryEnd);
        var child1 = new StoryEvent(Guid.NewGuid(), "child", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        root.AddChild(child1);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual("the end is coming", client.displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with no child event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_NoChild()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        client.activeScreenManager.gameCrashPanel.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "big decision", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        Assert.IsFalse(client.activeScreenManager.gameCrashPanel.activeSelf);
        client.ContinueOnlineStory(root);
        Assert.IsTrue(client.activeScreenManager.gameCrashPanel.activeSelf);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with no child event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_NoChild()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        client.activeScreenManager.gameCrashPanel.SetActive(false);
        var root = new StoryEvent(Guid.NewGuid(), "big decision", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testCharacter = new Character(new Skills(1, 1, 1, 1), "haha", null);
        client.clientLogic.StoryGraph = new StoryGraph(null, root, root);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        Assert.IsFalse(client.activeScreenManager.gameCrashPanel.activeSelf);
        client.ContinueOfflineStory(root);
        Assert.IsTrue(client.activeScreenManager.gameCrashPanel.activeSelf);
    }

    /// <summary>
    /// Test for the ContinueOnlineStory method with no null event.
    /// </summary>
    [Test]
    public void ContinueOnlineStoryTest_Null()
    {
        GameState.gameIsOnline = true;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        client.activeScreenManager.gameCrashPanel.SetActive(false);
        client.clientLogic.StoryGraph = new StoryGraph(null, null, null);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        Assert.IsFalse(client.activeScreenManager.gameCrashPanel.activeSelf);
        client.ContinueOnlineStory(null);
        Assert.IsTrue(client.activeScreenManager.gameCrashPanel.activeSelf);
    }

    /// <summary>
    /// Test for the ContinueOfflineStory method with no child event.
    /// </summary>
    [Test]
    public void ContinueOfflineStoryTest_Null()
    {
        GameState.gameIsOnline = false;
        client.displayStoryFlow.storyflowElement.SetActive(false);
        client.activeScreenManager.gameCrashPanel.SetActive(false);
        client.clientLogic.StoryGraph = new StoryGraph(null, null, null);

        // start test
        Assert.IsFalse(client.displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(client.displayStoryFlow.storyflowText.text);
        Assert.IsFalse(client.activeScreenManager.gameCrashPanel.activeSelf);
        client.ContinueOfflineStory(null);
        Assert.IsTrue(client.activeScreenManager.gameCrashPanel.activeSelf);
    }

    /// <summary>
    /// Test for SetVotingTime method.
    /// </summary>
    [Test]
    public void SetVotingTimeTest_Input_80()
    {
        client.votingTime.text = "80";
        Assert.AreEqual(30, client.clientLogic.VotingTime);
        client.SetVotingTime();
        Assert.AreEqual(60, client.clientLogic.VotingTime);
    }

    /// <summary>
    /// Test for SetVotingTime method.
    /// </summary>
    [Test]
    public void SetVotingTimeTest_Input_Minus10()
    {
        client.votingTime.text = "-10";
        Assert.AreEqual(30, client.clientLogic.VotingTime);
        client.SetVotingTime();
        Assert.AreEqual(5, client.clientLogic.VotingTime);
    }

    /// <summary>
    /// Test for SetVotingTime method.
    /// </summary>
    [Test]
    public void SetVotingTimeTest_Input_WrongInput()
    {
        client.votingTime.text = "awdawd";
        Assert.AreEqual(30, client.clientLogic.VotingTime);
        client.SetVotingTime();
        Assert.AreEqual(30, client.clientLogic.VotingTime);
    }

    [Test]
    public void ServerIssueTest_Code1000()
    {
        client.activeScreenManager.errorScreenPanel.SetActive(false);
        Assert.IsFalse(client.activeScreenManager.errorScreenPanel.activeSelf);
        client.ServerIssues(1000);
        Assert.IsTrue(client.activeScreenManager.errorScreenPanel.activeSelf);
    }

    [Test]
    public void ServerIssueTest_Code1006()
    {
        client.activeScreenManager.errorScreenPanel.SetActive(false);
        Assert.IsFalse(client.activeScreenManager.errorScreenPanel.activeSelf);
        client.ServerIssues(1006);
        Assert.IsTrue(client.activeScreenManager.errorScreenPanel.activeSelf);
    }
}
