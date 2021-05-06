using System;
using System.Collections.Generic;
using NUnit.Framework;


public class DisplayStoryFlowTest
{
    private DisplayStoryFlowTestClass displayStoryFlow;
    private StoryEvent testEvent;

    /// <summary>
    /// SetUp.
    /// </summary>
    [SetUp]
    public void DisplayStoryFlowTestSimplePasses()
    {
        displayStoryFlow = new DisplayStoryFlowTestClass();
        displayStoryFlow.storyflowElement.SetActive(false);
        testEvent = new StoryEvent(Guid.NewGuid(), "Test", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
    }

    /// <summary>
    /// Test for the SetStoryFlow method.
    /// </summary>
    [Test]
    public void SetStoryFlowTest()
    {
        Assert.IsFalse(displayStoryFlow.storyflowElement.activeSelf);
        Assert.IsNull(displayStoryFlow.storyflowText.text);
        displayStoryFlow.SetStoryFlow(testEvent);
        Assert.IsTrue(displayStoryFlow.storyflowElement.activeSelf);
        Assert.AreEqual(testEvent.Description, displayStoryFlow.storyflowText.text);
    }

    /// <summary>
    /// Test for the RemoveStoryFlowListenersTest method.
    /// Cant test correctly, so just checking if the method runs through.
    /// </summary>
    [Test]
    public void RemoveStoryFlowListenersTest()
    {
        displayStoryFlow.RemoveStoryFlowListeners();
    }
}
