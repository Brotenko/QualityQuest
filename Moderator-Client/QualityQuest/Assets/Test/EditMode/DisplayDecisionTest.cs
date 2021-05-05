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

    [Test]
    public void LoadDecisionTest()
    {
        displayDecision.LoadDecision(currentEvent,twoOptions);

        Assert.AreEqual(true,displayDecision.panelA.activeSelf);
        Assert.AreEqual(true,displayDecision.panelB.activeSelf);
        Assert.AreEqual(false,displayDecision.panelC.activeSelf);
        Assert.AreEqual(false,displayDecision.panelD.activeSelf);

        Assert.AreEqual("Option1",displayDecision.descriptionA.text);
        Assert.AreEqual("Option2",displayDecision.descriptionB.text);

        Assert.AreEqual("Option A", displayDecision.titleA.text);
        Assert.AreEqual("Option B", displayDecision.titleB.text);

        displayDecision.LoadDecision(currentEvent,threeOptions);

        Assert.AreEqual(true,displayDecision.panelA.activeSelf);
        Assert.AreEqual(true,displayDecision.panelB.activeSelf);
        Assert.AreEqual(true,displayDecision.panelC.activeSelf);
        Assert.AreEqual(false,displayDecision.panelD.activeSelf);

        Assert.AreEqual("Option1",displayDecision.descriptionA.text);
        Assert.AreEqual("Option2",displayDecision.descriptionB.text);
        Assert.AreEqual("Option3",displayDecision.descriptionC.text);

        Assert.AreEqual("Option A", displayDecision.titleA.text);
        Assert.AreEqual("Option B", displayDecision.titleB.text);
        Assert.AreEqual("Option C", displayDecision.titleC.text);

        displayDecision.LoadDecision(currentEvent,fourOptions);

        Assert.AreEqual(true,displayDecision.panelA.activeSelf);
        Assert.AreEqual(true,displayDecision.panelB.activeSelf);
        Assert.AreEqual(true,displayDecision.panelC.activeSelf);
        Assert.AreEqual(true,displayDecision.panelD.activeSelf);

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
