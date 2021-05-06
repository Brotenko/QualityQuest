using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterSelectionTest
{

    private CharacterSelectionTestClass characterSelection;
    private DisplayStatusbarTestClass displayStatusbar;
    private Character testChar;
    private StoryGraph testGraph;

    /// <summary>
    /// SetUp.
    /// </summary>
    [SetUp]
    public void LastTestSimplePasses()
    {
        displayStatusbar = new DisplayStatusbarTestClass();
        characterSelection = new CharacterSelectionTestClass();
        testChar = new Character(new Skills(1, 2, 3, 4), "Tobi", null);
        testGraph = new StoryGraph(null, null, null);

    }

    /// <summary>
    /// Test for the Awake method.
    /// </summary>
    [Test]
    public void AwakeTest()
    {
        Assert.IsNull(characterSelection.kirogh);
        Assert.IsNull(characterSelection.lumati);
        Assert.IsNull(characterSelection.noruso);
        Assert.IsNull(characterSelection.turgal);
        characterSelection.Awake();
        Assert.AreEqual("Turgal", characterSelection.turgal.Name);
        Assert.AreEqual("Lumati", characterSelection.lumati.Name);
        Assert.AreEqual("Noruso", characterSelection.noruso.Name);
        Assert.AreEqual("Kirogh", characterSelection.kirogh.Name);
        Assert.AreEqual(1, characterSelection.noruso.Abilities.Programming);
        Assert.AreEqual(2, characterSelection.turgal.Abilities.Partying);
        Assert.AreEqual(1, characterSelection.lumati.Abilities.Communication);
        Assert.AreEqual(0, characterSelection.kirogh.Abilities.Analytics);

        // DisplayCharacter UpdateCharacter is already tested.

        Assert.AreEqual("1", characterSelection.displayNoruso.programming.text);
        Assert.AreEqual("2", characterSelection.displayTurgal.party.text);
        Assert.AreEqual("1", characterSelection.displayLumati.communcation.text);
        Assert.AreEqual("0", characterSelection.displayKirogh.analytics.text);
    }

    /// <summary>
    /// Test for the InitializeCharacter method.
    /// </summary>
    [Test]
    public void InitializeCharacterTest()
    {
        Assert.IsNull(testGraph.Character);
        Assert.IsNull(displayStatusbar.analyticsSkillValue.text);
        Assert.IsNull(displayStatusbar.programmingSkillValue.text);
        Assert.IsNull(displayStatusbar.communicationSkillValue.text);
        Assert.IsNull(displayStatusbar.partySkillValue.text);
        characterSelection.InitializeCharacter(testChar, testGraph, displayStatusbar);
        Assert.AreEqual("2", displayStatusbar.analyticsSkillValue.text);
        Assert.AreEqual("4", displayStatusbar.programmingSkillValue.text);
        Assert.AreEqual("1", displayStatusbar.communicationSkillValue.text);
        Assert.AreEqual("3", displayStatusbar.partySkillValue.text);
        Assert.IsTrue(displayStatusbar.statusbar.activeSelf);
    }
}
