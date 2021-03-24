using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoryEventTests
{
    /*
    public StoryEvent event1;
    public StoryEvent event2;
    public StoryEvent event3;

    /// <summary>
    /// Method to initialize the test setup.
    /// </summary>
    public void TestInitialize()
    {
        event1 = new StoryEvent(0, "StoryEvent 1", null, new HashSet<StoryEvent>());
        event2 = new StoryEvent(1, "StoryEvent 2", null, new HashSet<StoryEvent>());
        event3 = new StoryEvent(2, "StoryEvent 3", event1, new HashSet<StoryEvent>());
        event1.getChildren().Add(event3);
    }

    /// <summary>
    /// Tests the addChild method by adding a child.
    /// </summary>
    [Test]
    public void addChildTest()
    {
        TestInitialize();
        Assert.IsFalse(event1.getChildren().Contains(event2));
        Assert.IsTrue(event2.getParent() is null);
        event1.addChild(event2);
        Assert.IsTrue(event1.getChildren().Contains(event2));
        Assert.IsTrue(event2.getParent().Equals(event1));
    }

    /// <summary>
    /// Tests the removeChild method by removing a child.
    /// </summary>
    [Test]
    public void removeChildTest()
    {
        TestInitialize();
        Assert.IsTrue(event3.getParent().Equals(event1));
        Assert.IsTrue(event1.getChildren().Contains(event3));
        event1.removeChild(event3);
        Assert.IsFalse(event1.getChildren().Contains(event3));
        Assert.IsTrue(event3.getParent() is null);
   