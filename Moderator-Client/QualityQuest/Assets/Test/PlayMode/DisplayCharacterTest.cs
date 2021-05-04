using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

[TestFixture]
public class DisplayCharacterTest
{
    private GameObject scriptHolder;
    private DisplayCharacter displayCharacter;
    private Character testChar;

    // A Test behaves as an ordinary method
    [SetUp]
    public void DisplayCharacterTestSimplePasses()
    {
        scriptHolder = GameObject.Instantiate(new GameObject("Holding"));
        displayCharacter = scriptHolder.AddComponent<DisplayCharacter>();

        displayCharacter.name = new TextMeshPro();
        displayCharacter.programming = new TextMeshPro();
        displayCharacter.communcation = new TextMeshPro();
        displayCharacter.analytics = new TextMeshPro();
        displayCharacter.party = new TextMeshPro();
        displayCharacter.image = Resources.Load<Image>("Image");
        
        testChar = new Character(new Skills(10, 11, 16, 17), "Harald", null);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void TestUpdateCharacterMethod()
    {
        Assert.AreEqual(null, displayCharacter.name.text);
        Assert.AreEqual(null, displayCharacter.communcation.text);
        Assert.AreEqual(null, displayCharacter.analytics.text);
        Assert.AreEqual(null, displayCharacter.party.text);
        Assert.AreEqual(null, displayCharacter.programming.text);
        displayCharacter.UpdateCharacter(testChar);
        Assert.AreEqual("Harald", displayCharacter.name.text);
        Assert.AreEqual("10", displayCharacter.communcation.text);
        Assert.AreEqual("11", displayCharacter.analytics.text);
        Assert.AreEqual("16", displayCharacter.party.text);
        Assert.AreEqual("17", displayCharacter.programming.text);
    }
}
