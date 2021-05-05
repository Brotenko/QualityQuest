using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterSelectionTest
{

    private CharacterSelectionTestClass characterSelection;

    /// <summary>
    /// SetUp.
    /// </summary>
    [SetUp]
    public void LastTestSimplePasses()
    {
        characterSelection = new CharacterSelectionTestClass();
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
}
