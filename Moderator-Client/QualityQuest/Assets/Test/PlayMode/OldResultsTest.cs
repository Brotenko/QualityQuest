using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// Test class for Result
/// </summary>
public class OldResultsTest
{
    private Result result;
    private GameObject scriptHolder;
    private StoryEvent votingPrompt;
    private StoryEvent votingOptionOne;
    private StoryEvent votingOptionTwo;
    private StoryEvent votingOptionThree;
    private StoryEvent votingOptionFour;
    private Dictionary<Guid, int> votingResults;
    private int countVoting;
    private string winningOption;

    /// <summary>
    /// Setup for the MonoBehaviorScript and all attributes.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        scriptHolder = GameObject.Instantiate(new GameObject("Result"));
        result = scriptHolder.AddComponent<Result>();
        result.titleA = new GameObject().AddComponent<TextMeshPro>();
        result.titleB = new GameObject().AddComponent<TextMeshPro>();
        result.titleC = new GameObject().AddComponent<TextMeshPro>();
        result.titleD = new GameObject().AddComponent<TextMeshPro>();

        result.votesA = new GameObject().AddComponent<TextMeshPro>();
        result.votesB = new GameObject().AddComponent<TextMeshPro>();
        result.votesC = new GameObject().AddComponent<TextMeshPro>();
        result.votesD = new GameObject().AddComponent<TextMeshPro>();

        result.prompt = new GameObject().AddComponent<TextMeshPro>();

        result.resultA = Resources.Load<Slider>("SliderA");
        result.resultB = Resources.Load<Slider>("SliderB");
        result.resultC = Resources.Load<Slider>("SliderC");
        result.resultD = Resources.Load<Slider>("SliderD");

        votingPrompt = new StoryEvent(Guid.NewGuid(), "Mit welchem Charakter möchtest du das Spiel spielen?", new HashSet<StoryEvent>(), StoryEventType.StoryRootEvent);

        votingOptionOne = new StoryEvent(Guid.NewGuid(), "Noruso", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        votingOptionTwo = new StoryEvent(Guid.NewGuid(), "Lumati", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        votingOptionThree = new StoryEvent(Guid.NewGuid(), "Turgal", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        votingOptionFour = new StoryEvent(Guid.NewGuid(), "Kirogh", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

    }

    /// <summary>
    /// Test for the HideAllSliders method.
    /// </summary>
    [Test]
    public void HideAllSliderTest()
    {
        result.HideAllSlider();
        Assert.IsFalse(result.resultA.gameObject.activeSelf);
        Assert.IsFalse(result.resultB.gameObject.activeSelf);
        Assert.IsFalse(result.resultC.gameObject.activeSelf);
        Assert.IsFalse(result.resultD.gameObject.activeSelf);
    }

    /// <summary>
    /// Test for the SetAllBlue method. Cant really test the colors,
    /// so just checking if the method runs through without any errors.
    /// </summary>
    [Test]
    public void SetAllBlue() 
    {
        result.SetAllBlue();
    }

    /// <summary>
    /// Test for the SetMaxValues method
    /// </summary>
    [Test]
    public void SetMaxValuesTest()
    {
        result.SetMaxValues(20);
        Assert.AreEqual(20.0f, result.resultA.maxValue);
        Assert.AreEqual(20.0f, result.resultB.maxValue);
        Assert.AreEqual(20.0f, result.resultC.maxValue);
        Assert.AreEqual(20.0f, result.resultD.maxValue);
    }

    /// <summary>
    /// Test for the SetGreen method. Cant really test the colors, just checking if the method
    /// runs through without any errors. Colors are tested in play mode.
    /// </summary>
    [Test]
    public void SetGreenTest()
    {
        result.SetGreen(result.resultA);
        result.SetGreen(result.resultB);
        result.SetGreen(result.resultC);
        result.SetGreen(result.resultD);
    }

    /// <summary>
    /// Test for the SetBlue method. Cant really test the colors, just checking if the method
    /// runs through without any errors. Colors are tested in play mode.
    /// </summary>
    [Test]
    public void SetBlueTest()
    {
        result.SetBlue(result.resultA);
        result.SetBlue(result.resultB);
        result.SetBlue(result.resultC);
        result.SetBlue(result.resultD);
    }

    /// <summary>
    /// Test for the LoadResult method with 2 votingOptions.
    /// </summary>
    [Test]
    public void LoadResultTest_TwoVotingOptions()
    {
        votingResults = new Dictionary<Guid, int>
        {
            {votingOptionOne.EventId, 10},
            {votingOptionTwo.EventId, 24},
        };

        votingPrompt.AddChild(votingOptionOne);
        votingPrompt.AddChild(votingOptionTwo);
        winningOption = votingOptionTwo.Description;
        countVoting = 34;

        var children = votingPrompt.Children.ToList();

        result.LoadResult(votingPrompt, children, votingResults, countVoting, winningOption);
        
        Assert.AreEqual(votingPrompt.Description, result.prompt.text);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsFalse(result.resultC.gameObject.activeSelf);
        Assert.IsFalse(result.resultD.gameObject.activeSelf);
        Assert.AreEqual(34.0f, result.resultA.maxValue);
        Assert.AreEqual(34.0f, result.resultB.maxValue);
        Assert.AreEqual(children[0].Description, result.titleA.text);
        Assert.AreEqual(children[1].Description, result.titleB.text);
        Assert.AreEqual(10.0f, result.resultA.value);
        Assert.AreEqual(24.0f, result.resultB.value);
        Assert.AreEqual("10", result.votesA.text);
        Assert.AreEqual("24", result.votesB.text);
    }

    /// <summary>
    /// Test for the LoadResult method with 3 votingOptions.
    /// </summary>
    [Test]
    public void LoadResultTest_ThreeVotingOptions()
    {
        votingResults = new Dictionary<Guid, int>
        {
            {votingOptionOne.EventId, 12},
            {votingOptionTwo.EventId, 14},
            {votingOptionThree.EventId, 10}
        };

        votingPrompt.AddChild(votingOptionOne);
        votingPrompt.AddChild(votingOptionTwo);
        votingPrompt.AddChild(votingOptionThree);
        winningOption = votingOptionTwo.Description;
        countVoting = 36;

        var children = votingPrompt.Children.ToList();

        result.LoadResult(votingPrompt, children, votingResults, countVoting, winningOption);

        Assert.AreEqual(votingPrompt.Description, result.prompt.text);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsTrue(result.resultC.gameObject.activeSelf);
        Assert.IsFalse(result.resultD.gameObject.activeSelf);
        Assert.AreEqual(36.0f, result.resultA.maxValue);
        Assert.AreEqual(36.0f, result.resultB.maxValue);
        Assert.AreEqual(36.0f, result.resultC.maxValue);
        Assert.AreEqual(children[0].Description, result.titleA.text);
        Assert.AreEqual(children[1].Description, result.titleB.text);
        Assert.AreEqual(children[2].Description, result.titleC.text);
        Assert.AreEqual(12.0f, result.resultA.value);
        Assert.AreEqual(14.0f, result.resultB.value);
        Assert.AreEqual(10.0f, result.resultC.value);
        Assert.AreEqual("12", result.votesA.text);
        Assert.AreEqual("14", result.votesB.text);
        Assert.AreEqual("10", result.votesC.text);
    }

    /// <summary>
    /// Test for the LoadResult method with 4 votingOptions.
    /// </summary>
    [Test]
    public void LoadResultTest_FourVotingOptions()
    {
        votingResults = new Dictionary<Guid, int>
        {
            {votingOptionOne.EventId, 1},
            {votingOptionTwo.EventId, 3},
            {votingOptionThree.EventId, 9},
            {votingOptionFour.EventId, 16}
        };

        votingPrompt.AddChild(votingOptionOne);
        votingPrompt.AddChild(votingOptionTwo);
        votingPrompt.AddChild(votingOptionThree);
        votingPrompt.AddChild(votingOptionFour);
        winningOption = votingOptionTwo.Description;
        countVoting = 29;

        var children = votingPrompt.Children.ToList();

        result.LoadResult(votingPrompt, children, votingResults, countVoting, winningOption);

        Assert.AreEqual(votingPrompt.Description, result.prompt.text);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsTrue(result.resultA.gameObject.activeSelf);
        Assert.IsTrue(result.resultC.gameObject.activeSelf);
        Assert.IsTrue(result.resultD.gameObject.activeSelf);
        Assert.AreEqual(29.0f, result.resultA.maxValue);
        Assert.AreEqual(29.0f, result.resultB.maxValue);
        Assert.AreEqual(29.0f, result.resultC.maxValue);
        Assert.AreEqual(29.0f, result.resultD.maxValue);
        Assert.AreEqual(children[0].Description, result.titleA.text);
        Assert.AreEqual(children[1].Description, result.titleB.text);
        Assert.AreEqual(children[2].Description, result.titleC.text);
        Assert.AreEqual(children[3].Description, result.titleD.text);
        Assert.AreEqual(1.0f, result.resultA.value);
        Assert.AreEqual(3.0f, result.resultB.value);
        Assert.AreEqual(9.0f, result.resultC.value);
        Assert.AreEqual(16.0f, result.resultD.value);
        Assert.AreEqual("1", result.votesA.text);
        Assert.AreEqual("3", result.votesB.text);
        Assert.AreEqual("9", result.votesC.text);
        Assert.AreEqual("16", result.votesD.text);
    }

    /// <summary>
    /// Method to destroy the GameObject after testing.
    /// </summary>
    [TearDown]
    public virtual void TearDown()
    {
        Object.DestroyImmediate(scriptHolder);
    }
}
