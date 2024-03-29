using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DisplayDecisionTest
{

    private DisplayDecisionTestClass displayDecision;
    private StoryEvent currentEvent;
    private List<StoryEvent> twoOptions = new List<StoryEvent>();
    private List<StoryEvent> threeOptions = new List<StoryEvent>();
    private List<StoryEvent> fourOptions = new List<StoryEvent>();

    [SetUp]
    public void SetUp()
    {

        displayDecision = new DisplayDecisionTestClass();

        currentEvent = new StoryEvent(new Guid(), "Decision", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        twoOptions.Add(new StoryEvent(new Guid(), "Option1", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        twoOptions.Add(new StoryEvent(new Guid(), "Option2", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));

        threeOptions.Add(new StoryEvent(new Guid(), "Option1", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        threeOptions.Add(new StoryEvent(new Guid(), "Option2", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        threeOptions.Add(new StoryEvent(new Guid(), "Option3", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));

        fourOptions.Add(new StoryEvent(new Guid(), "Option1", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        fourOptions.Add(new StoryEvent(new Guid(), "Option2", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        fourOptions.Add(new StoryEvent(new Guid(), "Option3", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));
        fourOptions.Add(new StoryEvent(new Guid(), "Option4", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption));

    }

    /// <summary>
    /// Test for LoadDecision. Test for 2, 3 or 4 options.
    /// </summary>
    [Test]
    public void LoadDecisionTest()
    {
        displayDecision.LoadDecision(currentEvent,twoOptions);

        Assert.IsTrue(displayDecision.panelA.activeSelf);
        Assert.IsTrue(displayDecision.panelB.activeSelf);
        Assert.IsFalse(displayDecision.panelC.activeSelf);
        Assert.IsFalse(displayDecision.panelD.activeSelf);

        Assert.AreEqual("Option1",displayDecision.descriptionA.text);
        Assert.AreEqual("Option2",displayDecision.descriptionB.text);

        Assert.AreEqual("Option A", displayDecision.titleA.text);
        Assert.AreEqual("Option B", displayDecision.titleB.text);

        displayDecision.LoadDecision(currentEvent,threeOptions);

        Assert.IsTrue(displayDecision.panelA.activeSelf);
        Assert.IsTrue(displayDecision.panelB.activeSelf);
        Assert.IsTrue(displayDecision.panelC.activeSelf);
        Assert.IsFalse(displayDecision.panelD.activeSelf);

        Assert.AreEqual("Option1",displayDecision.descriptionA.text);
        Assert.AreEqual("Option2",displayDecision.descriptionB.text);
        Assert.AreEqual("Option3",displayDecision.descriptionC.text);

        Assert.AreEqual("Option A", displayDecision.titleA.text);
        Assert.AreEqual("Option B", displayDecision.titleB.text);
        Assert.AreEqual("Option C", displayDecision.titleC.text);

        displayDecision.LoadDecision(currentEvent,fourOptions);

        Assert.IsTrue(displayDecision.panelA.activeSelf);
        Assert.IsTrue(displayDecision.panelB.activeSelf);
        Assert.IsTrue(displayDecision.panelC.activeSelf);
        Assert.IsTrue(displayDecision.panelD.activeSelf);

        Assert.AreEqual("Option1",displayDecision.descriptionA.text);
        Assert.AreEqual("Option2",displayDecision.descriptionB.text);
        Assert.AreEqual("Option3",displayDecision.descriptionC.text);
        Assert.AreEqual("Option4",displayDecision.descriptionD.text);

        Assert.AreEqual("Option A",displayDecision.titleA.text);
        Assert.AreEqual("Option B",displayDecision.titleB.text);
        Assert.AreEqual("Option C",displayDecision.titleC.text);
        Assert.AreEqual("Option D",displayDecision.titleD.text);
    }
}
