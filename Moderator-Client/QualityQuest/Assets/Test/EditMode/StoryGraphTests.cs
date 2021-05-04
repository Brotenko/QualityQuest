using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Random = System.Random;

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

    [SetUp]
    public void TestSetup()
    {
        story = new StoryGraph();
        newCurrentEvent = new StoryEvent(new Guid(), "This is a Description", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        characterOne = new Character(new Skills(0, 0, 0, 0), "Alex", null);
        characterTwo = new Character(new Skills(10, 10, 10, 10), "Elias", null);

        randomRoot = new StoryEvent(Guid.NewGuid(), "Root", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        randomOneOptionOne = new StoryEvent(Guid.NewGuid(), "DecisionOneFalse", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false,RandomType.RandomDecisionOne);
        randomOneOptionTwo = new StoryEvent(Guid.NewGuid(), "DecisionOneTrue", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionOne);

        randomTwoOptionOne = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionTwo);
        randomTwoOptionTwo = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionTwo);

        randomThreeOptionOne = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionThree);
        randomThreeOptionTwo = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionThree);

        randomSixOptionOne = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionSix);
        randomSixOptionTwo = new StoryEvent(new Guid(), "This is a description.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionSix);


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
        randomStory.GetRandomOption();
        //randomStory.SetCurrentEvent(randomOneOptionOne);
        //Assert.AreEqual(2,randomRoot.Children.Count);
        Assert.AreEqual(randomOneOptionOne, randomStory.CurrentEvent);
        //Assert.AreEqual(randomOneOptionOne.Description,randomStory.CurrentEvent.Description);

    }

    [Test]
    public void GetRandomOptionTest_True_RandomDecisionOne()
    {
        randomRoot.AddChild(randomOneOptionOne);
        randomRoot.AddChild(randomOneOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomOneOptionTwo, randomStory.CurrentEvent);
    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionTwo()
    {
        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomTwoOptionOne, randomStory.CurrentEvent);

    }
    [Test]
    public void GetRandomOptionTest_True_RandomDecisionTwo()
    {
        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomTwoOptionTwo, randomStory.CurrentEvent);

    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionThree()
    {
        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomThreeOptionOne, randomStory.CurrentEvent);

    }
    [Test]
    public void GetRandomOptionTest_True_RandomDecisionThree()
    {
        randomRoot.AddChild(randomTwoOptionOne);
        randomRoot.AddChild(randomTwoOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomThreeOptionTwo, randomStory.CurrentEvent);

    }

    [Test]
    public void GetRandomOptionTest_False_RandomDecisionSix()
    {
        randomRoot.AddChild(randomThreeOptionOne);
        randomRoot.AddChild(randomThreeOptionTwo);
        randomStory = new StoryGraph(characterOne, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomSixOptionOne, randomStory.CurrentEvent);

    }
    [Test]
    public void GetRandomOptionTest_True_RandomDecisionSix()
    {
        randomRoot.AddChild(randomSixOptionOne);
        randomRoot.AddChild(randomSixOptionTwo);
        randomStory = new StoryGraph(characterTwo, randomRoot, randomRoot);
        randomStory.GetRandomOption();
        Assert.AreEqual(randomSixOptionTwo, randomStory.CurrentEvent);

    }

}
