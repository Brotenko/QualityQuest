using System;
using System.Collections.Generic;
using MessageContainer.Messages;
using NUnit.Framework;


public class ClientLogicTests
{
    private ClientLogic clientLogic;
    private RequestStartVotingMessage requestStartVotingMessage;
    private RequestOpenSessionMessage requestOpenSessionMessage;
    private ReconnectMessage reconnectMessage;
    private RequestGameStartMessage requestGameStartMessage;
    private SessionOpenedMessage sessionOpenedMessage;
    private RequestCloseSessionMessage requestCloseSessionMessage;
    private RequestGamePausedStatusChangeMessage requestGamePausedStatusChangeMessageTrue;
    private RequestGamePausedStatusChangeMessage requestGamePausedStatusChangeMessageFalse;
    private StoryEvent root;
    private StoryEvent childOne;
    private StoryEvent childTwo;
    private StoryEvent childThree;
    private StoryEvent childFour;
    private StoryEvent noChild;
    private StoryEvent nullEvent;
    private Character testCharacter;

    private StoryEvent testRandom;
    private StoryGraph testGraphOne;
    private StoryGraph testGraphTwo;

    private string url;
    private string sessionKey;
    private string prompt;
    private HashSet<StoryEvent> votingOptions;
    private Dictionary<Guid, int> votingResults;
    private int votingCount;



    /// <summary>
    /// Test setup.
    /// </summary>
    [SetUp]
    public void ClientLogicTestSetup()
    {
        testCharacter = new Character(new Skills(10, 10, 10, 10), "TestCharacter", null);
        clientLogic = new ClientLogic(30);
        root = new StoryEvent(Guid.NewGuid(), "Event1", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        childOne = new StoryEvent(Guid.NewGuid(), "child1", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionOne);
        childTwo = new StoryEvent(Guid.NewGuid(), "child2", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionOne);
        childThree = new StoryEvent(Guid.NewGuid(), "child3", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        childFour = new StoryEvent(Guid.NewGuid(), "child4", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        root.AddChild(childOne);
        root.AddChild(childTwo);
        root.AddChild(childThree);
        root.AddChild(childFour);

        requestStartVotingMessage = new RequestStartVotingMessage(clientLogic.ModeratorClientGuid,
            clientLogic.VotingTime, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[4])
        {
            VotingPrompt = new KeyValuePair<Guid, string>(root.EventId, root.Description),
            VotingOptions =
            {
                [0] = new KeyValuePair<Guid, string>(childOne.EventId, childOne.Description),
                [1] = new KeyValuePair<Guid, string>(childTwo.EventId, childTwo.Description),
                [2] = new KeyValuePair<Guid, string>(childThree.EventId, childThree.Description),
                [3] = new KeyValuePair<Guid, string>(childFour.EventId, childFour.Description)
            }
        };

        reconnectMessage = new ReconnectMessage(clientLogic.ModeratorClientGuid);
        requestOpenSessionMessage = new RequestOpenSessionMessage(clientLogic.ModeratorClientGuid, "Hallo");
        requestGameStartMessage = new RequestGameStartMessage(clientLogic.ModeratorClientGuid);
        sessionOpenedMessage = new SessionOpenedMessage(Guid.NewGuid(), "AAAAA", new Uri("https://www.google.de/"));
        requestCloseSessionMessage = new RequestCloseSessionMessage(clientLogic.ModeratorClientGuid, "AAAAA");
        requestGamePausedStatusChangeMessageTrue =
            new RequestGamePausedStatusChangeMessage(clientLogic.ModeratorClientGuid, true);
        requestGamePausedStatusChangeMessageFalse =
            new RequestGamePausedStatusChangeMessage(clientLogic.ModeratorClientGuid, false);
        sessionKey = "AAAAA";
        url = "https://www.google.de/";

        prompt = root.Description;
        votingOptions = root.Children;
        votingResults = new Dictionary<Guid, int>
        {
            {childOne.EventId, 10},
            {childTwo.EventId, 24},
            {childThree.EventId, 1},
            {childFour.EventId, 3}
        };

        votingCount = 38;
        noChild = new StoryEvent(Guid.NewGuid(), "Ich hab keine kinder =(", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        childThree.AddChild(noChild);
        testRandom = new StoryEvent(Guid.NewGuid(), "test123", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        testRandom.AddChild(childOne);
        testRandom.AddChild(childTwo);
        testGraphOne = new StoryGraph(testCharacter, testRandom, testRandom);
        testGraphTwo = new StoryGraph(testCharacter, childThree, childThree);
    }

    /// <summary>
    /// Test for the constructor.
    /// </summary>
    [Test]
    public void ClientLogicTest()
    {
        Assert.AreEqual(false, clientLogic.ActiveVoting);
        Assert.AreEqual(30, clientLogic.VotingTime);
        Assert.AreEqual(0, clientLogic.VotingStatistic.Statistic.Count);
        Assert.AreEqual("Mit welchem Charakter möchtest du das Spiel spielen?",
            clientLogic.StoryGraph.Root.Description);
        Assert.AreEqual(false, clientLogic.SpecialOption);
        Assert.AreEqual(null, clientLogic.SessionKey);
        Assert.AreEqual(null, clientLogic.Url);
    }

    /// <summary>
    /// Test for the SetNewModeratorGuid method.
    /// </summary>
    [Test]
    public void SetNewModeratorGuidTest()
    {
        var moderatorGuid = clientLogic.ModeratorClientGuid;
        clientLogic.SetNewModeratorClientGuid();

        Assert.AreNotEqual(moderatorGuid, clientLogic.ModeratorClientGuid);
    }

    /// <summary>
    /// Test for the InitializeRequestStartVotingMessage method.
    /// </summary>
    [Test]
    public void InitializeRequestStartVotingMessageTest()
    {
        var testMessage = clientLogic.InitializeRequestStartVotingMessage(root);
        Assert.AreEqual(requestStartVotingMessage.VotingPrompt, testMessage.VotingPrompt);
        Assert.AreEqual(requestStartVotingMessage.VotingOptions, testMessage.VotingOptions);
        Assert.AreEqual(requestStartVotingMessage.VotingTime, testMessage.VotingTime);
    }

    /// <summary>
    /// Test for the InitializeReconnectMessage method.
    /// </summary>
    [Test]
    public void InitializeReconnectMessageTest()
    {
        var testMessage = clientLogic.InitializeReconnectMessage();
        Assert.AreEqual(reconnectMessage.ModeratorID, testMessage.ModeratorID);
    }

    /// <summary>
    /// Test for InitializeReconnectMessage method with the correct password.
    /// </summary>
    [Test]
    public void InitializeRequestOpenSessionMessageTest_CorrectPw()
    {
        var testMessage = new RequestOpenSessionMessage(clientLogic.ModeratorClientGuid, "Hallo");
        Assert.AreEqual(requestOpenSessionMessage.Password, testMessage.Password);
        Assert.AreEqual(requestOpenSessionMessage.ModeratorID, testMessage.ModeratorID);
    }

    /// <summary>
    /// Test for InitializeReconnectMessage method with the wrong password.
    /// </summary>
    [Test]
    public void InitializeRequestOpenSessionMessage_IncorrectPw()
    {
        var testMessage = new RequestOpenSessionMessage(clientLogic.ModeratorClientGuid, "sup");
        Assert.AreNotEqual(requestOpenSessionMessage.Password, testMessage.Password);
        Assert.AreEqual(requestOpenSessionMessage.ModeratorID, testMessage.ModeratorID);
    }

    /// <summary>
    /// Test for the InitializeRequestGameStartMessage method.
    /// </summary>
    [Test]
    public void InitializeRequestGameStartMessageTest()
    {
        var testMessage = new RequestGameStartMessage(clientLogic.ModeratorClientGuid);
        Assert.AreEqual(requestGameStartMessage.ModeratorID, testMessage.ModeratorID);
    }

    /// <summary>
    /// Test for the SaveUrlAndSessionKey method.
    /// </summary>
    [Test]
    public void SaveUrlAndSessionKeyTest()
    {
        Assert.AreEqual(null, clientLogic.SessionKey);
        Assert.AreEqual(null, clientLogic.Url);

        clientLogic.SaveUrlAndSessionKey(sessionOpenedMessage);

        Assert.AreEqual(sessionKey, clientLogic.SessionKey);
        Assert.AreEqual(url, clientLogic.Url);
    }

    /// <summary>
    /// Test for the InitializeRequestCloseSessionMessage method.
    /// </summary>
    [Test]
    public void InitializeRequestCloseSessionMessageTest()
    {
        var testMessage = new RequestCloseSessionMessage(clientLogic.ModeratorClientGuid, sessionKey);
        Assert.AreEqual(requestCloseSessionMessage.ModeratorID, testMessage.ModeratorID);
        Assert.AreEqual(requestCloseSessionMessage.SessionKey, testMessage.SessionKey);
    }

    /// <summary>
    /// Test for the InitializeRequestGamePausedStatusChangeMessage method with true.
    /// </summary>
    [Test]
    public void InitializeRequestGamePausedStatusChangeMessageTrueTest()
    {
        var testMessage = new RequestGamePausedStatusChangeMessage(clientLogic.ModeratorClientGuid, true);
        Assert.AreEqual(requestGamePausedStatusChangeMessageTrue.ModeratorID, testMessage.ModeratorID);
        Assert.AreEqual(requestGamePausedStatusChangeMessageTrue.GamePaused, testMessage.GamePaused);
    }

    /// <summary>
    /// Test for the InitializeRequestGamePausedStatusChangeMessage method with false.
    /// </summary>
    [Test]
    public void InitializeRequestGamePausedStatusChangeMessageFalseTest()
    {
        var testMessage = new RequestGamePausedStatusChangeMessage(clientLogic.ModeratorClientGuid, false);
        Assert.AreEqual(requestGamePausedStatusChangeMessageFalse.ModeratorID, testMessage.ModeratorID);
        Assert.AreEqual(requestGamePausedStatusChangeMessageFalse.GamePaused, testMessage.GamePaused);
    }

    /// <summary>
    /// Test for the SaveStatistics method.
    /// </summary>
    [Test]
    public void SaveStatisticsTest()
    {
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic.Count == 0);
        clientLogic.SaveStatistics(prompt, votingOptions, votingResults, votingCount);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic.Count == 1);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingCount == 38);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingDecision.Equals("Event1"));
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingOptions[childOne.Description] == 10);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingOptions[childTwo.Description] == 24);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingOptions[childThree.Description] == 1);
        Assert.IsTrue(clientLogic.VotingStatistic.Statistic[0].VotingOptions[childFour.Description] == 3);
    }

    /// <summary>
    /// Test for the ContinueDecision method with 1 child StoryEvent.
    /// </summary>
    [Test]
    public void ContinueDecisionTest_OneChild()
    {
        var testEvent = clientLogic.ContinueDecision(testGraphTwo);

        Assert.AreEqual(noChild, testEvent);
    }

    /// <summary>
    /// Test for the ContinueDecision method with two child StoryEvents.
    /// </summary>
    [Test]
    public void ContinueDecisionTest_TwoChilds()
    {
        var testEvent = clientLogic.ContinueDecision(testGraphOne);

        Assert.AreEqual(childOne, testEvent);
    }

    /// <summary>
    /// Test for the ValidateVotingEndedMessage with a correct input.
    /// </summary>
    [Test]
    public void ValidateVotingEndedMessageTest_Success()
    {
        // Cant test it correctly since nothing happens with a correct input.
        clientLogic.ValidateVotingEndedMessage(root, votingResults);
    }

    /// <summary>
    /// Test for the ValidateVotingEndedMessage with a wrong input. Throws a WrongVotingEndedMessageException.
    /// </summary>
    [Test]
    public void ValidateVotingEndedMessageTest_Fail()
    {
        Assert.Throws<WrongVotingEndedMessage>(() => clientLogic.ValidateVotingEndedMessage(childThree, votingResults));
    }

    /// <summary>
    /// Test for the ValidateStoryEvent method with null as the StoryEvent.
    /// </summary>
    [Test]
    public void ValidateStoryEventTest_Null()
    {
        Assert.Throws<WrongStoryEvent>(() => clientLogic.ValidateStoryEvent(null));
    }

    /// <summary>
    /// Test for the ValidateStoryEvent method with a StoryEvent with zero children.
    /// </summary>
    [Test]
    public void ValidateStoryEventTest_NoChild()
    {
        Assert.Throws<WrongStoryEvent>(() => clientLogic.ValidateStoryEvent(noChild));
    }

    /// <summary>
    /// Test for the ValidateStoryEvent method with a valid StoryEvent as Input
    /// </summary>
    [Test]
    public void ValidateStoryEventTest_ValidStoryEvent()
    {
        // cant test it correctly since nothing happens.
        clientLogic.ValidateStoryEvent(root);
    }

    /// <summary>
    /// Test for the WorkshopDecision method. The PlayerCharacter gets fired.
    /// </summary>
    [Test]
    public void WorkshopDecisionTest_Fired()
    {
        testCharacter.Abilities.updateAbilities(new Skills(-10, -99, -99, -99));
        var testEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var workshop = new StoryEvent(Guid.NewGuid(), "invite", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopInvite);
        var noWorkshop = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopNoInvite);
        var fired = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFired);
        var wrongEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        clientLogic.StoryGraph = new StoryGraph(testCharacter, testEvent, testEvent);

        testEvent.AddChild(workshop);
        testEvent.AddChild(noWorkshop);
        testEvent.AddChild(fired);

        Assert.AreEqual(fired, clientLogic.WorkshopDecision(testEvent));
    }

    /// <summary>
    /// Test for the WorkshopDecision method. The PlayerCharacter no workshop invite.
    /// </summary>
    [Test]
    public void WorkshopDecisionTest_NoInvite()
    {
        testCharacter.Abilities.updateAbilities(new Skills(-10, -10, -5, 0));
        var testEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var workshop = new StoryEvent(Guid.NewGuid(), "invite", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopInvite);
        var noWorkshop = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopNoInvite);
        var fired = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFired);
        var wrongEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        clientLogic.StoryGraph = new StoryGraph(testCharacter, testEvent, testEvent);

        testEvent.AddChild(workshop);
        testEvent.AddChild(noWorkshop);
        testEvent.AddChild(fired);

        Assert.AreEqual(noWorkshop, clientLogic.WorkshopDecision(testEvent));
    }

    /// <summary>
    /// Test for the WorkshopDecision method. The PlayerCharacter gets fired.
    /// </summary>
    [Test]
    public void WorkshopDecisionTest_WorkshopInvite()
    {
        var testEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var workshop = new StoryEvent(Guid.NewGuid(), "invite", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopInvite);
        var noWorkshop = new StoryEvent(Guid.NewGuid(), "noWorkshop", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopNoInvite);
        var fired = new StoryEvent(Guid.NewGuid(), "fired", new HashSet<StoryEvent>(), StoryEventType.StoryFired);
        var wrongEvent = new StoryEvent(Guid.NewGuid(), "cant happen", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        clientLogic.StoryGraph = new StoryGraph(testCharacter, testEvent, testEvent);

        testEvent.AddChild(workshop);
        testEvent.AddChild(noWorkshop);
        testEvent.AddChild(fired);

        Assert.AreEqual(workshop, clientLogic.WorkshopDecision(testEvent));
    }

    /// <summary>
    /// Test for the WorkshopDecision method. Can not happen, since the Workshop event has the followup events, just in case.
    /// </summary>
    [Test]
    public void WorkshopDecisionTest_CanNotHappen()
    {
        var testEvent = new StoryEvent(Guid.NewGuid(), "root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        var wrongEvent = new StoryEvent(Guid.NewGuid(), "cant happen", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        clientLogic.StoryGraph = new StoryGraph(testCharacter, testEvent, testEvent);
        testEvent.AddChild(wrongEvent);

        Assert.AreEqual(wrongEvent, clientLogic.WorkshopDecision(testEvent));
    }

    /// <summary>
    /// Test for the ContinueSpecialDecision method. The Special options gets removed.
    /// </summary>
    [Test]
    public void ContinueSpecialDecisionTest_DeleteSpecialOption()
    {
        var testDecision = new StoryEvent(Guid.NewGuid(), "really special decision", new HashSet<StoryEvent>(), StoryEventType.StorySpecialDecision);
        var specialOption = new StoryEvent(Guid.NewGuid(), "really special option", new HashSet<StoryEvent>(), StoryEventType.StorySpecialOption);
        clientLogic.SpecialOption = false;
        testDecision.AddChild(specialOption);
        Assert.IsTrue(testDecision.Children.Contains(specialOption));
        clientLogic.ContinueSpecialDecision(testDecision);
        Assert.IsFalse(testDecision.Children.Contains(specialOption));
    }

    /// <summary>
    /// Test for the ContinueSpecialDecision method. Nothing happens.
    /// </summary>
    [Test]
    public void ContinueSpecialDecisionTest_DontDeleteSpecialOption()
    {
        var testDecision = new StoryEvent(Guid.NewGuid(), "really special decision", new HashSet<StoryEvent>(), StoryEventType.StorySpecialDecision);
        var specialOption = new StoryEvent(Guid.NewGuid(), "really special option", new HashSet<StoryEvent>(), StoryEventType.StorySpecialOption);
        clientLogic.SpecialOption = true;
        testDecision.AddChild(specialOption);
        Assert.IsTrue(testDecision.Children.Contains(specialOption));
        clientLogic.ContinueSpecialDecision(testDecision);
        Assert.IsTrue(testDecision.Children.Contains(specialOption));
    }

    /// <summary>
    /// Test for the UnlockDecision method.
    /// </summary>
    [Test]
    public void UnlockDecisionTest()
    {
        Assert.IsFalse(clientLogic.SpecialOption);
        clientLogic.UnlockDecision();
        Assert.IsTrue(clientLogic.SpecialOption);
    }
}

