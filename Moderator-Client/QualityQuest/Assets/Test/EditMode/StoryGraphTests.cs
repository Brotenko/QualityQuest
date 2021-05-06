using System;
using System.Collections.Generic;
using NUnit.Framework;


public class StoryGraphTests
{

    private StoryEvent newCurrentEvent;

    private StoryGraph story;

    private Character characterOne;
    private Character characterTwo;

    private StoryGraph randomStory;

    private StoryEvent randomRoot;

    private StoryEvent randomOneOptionOne;
    private StoryEvent randomOneOptionTwo;

    private StoryEvent randomTwoOptionOne;
    private StoryEvent randomTwoOptionTwo;

    private StoryEvent randomThreeOptionOne;
    private StoryEvent randomThreeOptionTwo;

    private StoryEvent randomSixOptionOne;
    private StoryEvent randomSixOptionTwo;

    private StoryEvent randomDefaultOptionOne;
    private StoryEvent randomDefaultOptionTwo;

    [SetUp]
    public void TestSetup()
    {

        story = new StoryGraph();

        newCurrentEvent = new StoryEvent(new Guid(), "NewCurrentEvent", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        characterOne = new Character(new Skills(0, 0, 0, 0), "Alex", null);
        characterTwo = new Character(new Skills(10, 10, 10, 10), "Elias", null);

        randomRoot = new StoryEvent(new Guid(), "Root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        randomOneOptionOne = new StoryEvent(new Guid(), "DecisionOneFalse", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false,RandomType.RandomDecisionOne);
        randomOneOptionTwo = new StoryEvent(new Guid(), "DecisionOneTrue", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionOne);

        randomTwoOptionOne = new StoryEvent(new Guid(), "DecisionTwoFalse", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionTwo);
        randomTwoOptionTwo = new StoryEvent(new Guid(), "DecisionTwoTrue", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionTwo);

        randomThreeOptionOne = new StoryEvent(new Guid(), "DecisionThreeFalse", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionThree);
        randomThreeOptionTwo = new StoryEvent(new Guid(), "DecisionThreeTrue", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionThree);

        randomSixOptionOne = new StoryEvent(new Guid(), "DecisionSixFalse", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionSix);
        randomSixOptionTwo = new StoryEvent(new Guid(), "DecisionSixTrue", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionSix);

        randomDefaultOptionOne = new StoryEvent(new Guid(), "DefaultOne",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);
        randomDefaultOptionTwo = new StoryEvent(new Guid(), "DefaultTwo",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

    }

    [Test]
    public void StoryGraphTest()
    {

        Assert.AreEqual(story.Root,story.CurrentEvent);
        Assert.AreEqual(null,story.Character);

    }

    [Test]
    public void SetCurrentEventTest()
    {

        Assert.AreNotEqual(newCurrentEvent, story.CurrentEvent);
        story.SetCurrentEvent(newCurrentEvent);
        Assert.AreEqual(newCurrentEvent,story.CurrentEvent);

    }

    [Test]

    public void GetRandomOptionTest_False_RandomDecisionOne()
    {

        randomRoot.AddChild(randomOneOptionOne);
        randomRoot.AddChild(randomOneOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        Assert.AreEqual(randomOneOptionOne, randomStory.GetRandomOption());

    }

    [Test]

    public void GetRandomOptionTest_True_RandomDecisionOne()
    {

        randomRoot.AddChild(randomOneOptionOne);
        randomRoot.AddChild(randomOneOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        Assert.AreEqual(randomOneOptionTwo, randomStory.GetRandomOption());
    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionTwo()
    {

        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        Assert.AreEqual(randomTwoOptionOne, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_True_RandomDecisionTwo()
    {

        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        Assert.AreEqual(randomTwoOptionTwo, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionThree()
    {

        randomRoot.AddChild(randomThreeOptionOne);
        randomRoot.AddChild(randomThreeOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        Assert.AreEqual(randomThreeOptionOne, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_True_RandomDecisionThree()
    {

        randomRoot.AddChild(randomThreeOptionOne);
        randomRoot.AddChild(randomThreeOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        Assert.AreEqual(randomThreeOptionTwo, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionSix()
    {

        randomRoot.AddChild(randomSixOptionOne);
        randomRoot.AddChild(randomSixOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        Assert.AreEqual(randomSixOptionOne, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_True_RandomDecisionSix()
    {

        randomRoot.AddChild(randomSixOptionOne);
        randomRoot.AddChild(randomSixOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        Assert.AreEqual(randomSixOptionTwo, randomStory.GetRandomOption());

    }

    [Test]
    public void GetRandomOptionTest_Default()
    {

        randomRoot.AddChild(randomDefaultOptionOne);
        randomRoot.AddChild(randomDefaultOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        Assert.AreEqual(randomDefaultOptionOne, randomStory.GetRandomOption());

    }

}
