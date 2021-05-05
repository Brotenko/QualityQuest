using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DisplayStatusbarTest
{

    private DisplayStatusbarTestClass displayStatusBar;

    private Sprite sprite;
    private Skills skills;
    private Skills skillChangesPositive;
    private Skills skillChangesZero;
    private Skills skillChangesNegative;
    private int time;
    private Color positive;
    private Color negative;


    [SetUp]
    public void SetUp()
    {
        displayStatusBar = new DisplayStatusbarTestClass();
        sprite = Resources.Load<Sprite>("characters/kirogh");
        skills = new Skills(5, 2, 4, 3);
        skillChangesPositive = new Skills(2, 3, 1, 3);
        skillChangesZero = new Skills(0, 0, 0, 0);
        skillChangesNegative = new Skills(-3, -2, -1, -5);
        time = 5;
        positive = new Color(0.3215686f, 0.6352941f, 0.3411765f);
        negative = Color.red;
    }

    [Test]
    public void SetImageTest()
    {

        displayStatusBar.SetImage(sprite);
        Assert.AreSame(sprite,displayStatusBar.characterImage.sprite);

    }

    [Test]
    public void UpdateSkillChangesTest()
    {

        displayStatusBar.UpdateSkillChanges(skillChangesPositive);
        Assert.AreEqual(skillChangesPositive.Communication.ToString(),displayStatusBar.skillChangeCommunication.text);
        Assert.AreEqual(skillChangesPositive.Analytics.ToString(),displayStatusBar.skillChangeAnalytics.text);
        Assert.AreEqual(skillChangesPositive.Partying.ToString(), displayStatusBar.skillChangeParty.text);
        Assert.AreEqual(skillChangesPositive.Programming.ToString(), displayStatusBar.skillChangeProgramming.text);

    }

    [Test]
    public void ShowSkillChangeTest()
    {
        // positive SkillChange
        displayStatusBar.ShowSkillChange(displayStatusBar.skillChangeCommunication,skillChangesPositive.Communication);
        Assert.AreEqual(skillChangesPositive.Communication.ToString(), displayStatusBar.skillChangeCommunication.text);
        Assert.IsTrue(displayStatusBar.skillChangeCommunication.gameObject.activeSelf);
        Assert.AreEqual(positive,displayStatusBar.skillChangeCommunication.color);

        // negative SkillChange
        displayStatusBar.ShowSkillChange(displayStatusBar.skillChangeCommunication,skillChangesNegative.Communication);
        Assert.AreEqual(skillChangesNegative.Communication.ToString(), displayStatusBar.skillChangeCommunication.text);
        Assert.IsTrue(displayStatusBar.skillChangeCommunication.gameObject.activeSelf);
        Assert.AreEqual(negative,displayStatusBar.skillChangeCommunication.color);

        // no SkillChange
        Assert.IsFalse(displayStatusBar.ShowSkillChange(displayStatusBar.skillChangeCommunication,skillChangesZero.Communication));




    }

    [Test]
    public void DisplaySkillsTest()
    {

        displayStatusBar.DisplaySkills(skills);
        Assert.AreEqual(skills.Communication.ToString(),displayStatusBar.communicationSkillValue.text);
        Assert.AreEqual(skills.Analytics.ToString(),displayStatusBar.analyticsSkillValue.text);
        Assert.AreEqual(skills.Partying.ToString(),displayStatusBar.partySkillValue.text);
        Assert.AreEqual(skills.Programming.ToString(),displayStatusBar.programmingSkillValue.text);

    }

    [Test]
    public void ShowStatusBar()
    {

        displayStatusBar.ShowStatusBar(true);
        Assert.IsTrue(displayStatusBar.statusbar.activeSelf);

        displayStatusBar.ShowStatusBar(false);
        Assert.IsFalse(displayStatusBar.statusbar.activeSelf);

    }

    [Test]
    public void DisplayDice()
    {

        displayStatusBar.DisplayDice(time);
        Assert.IsTrue(displayStatusBar.dice.activeSelf);

    }

    [Test]
    public void DisplayTimer()
    {

        displayStatusBar.DisplayTimer(time);
        Assert.IsTrue(displayStatusBar.decision.activeSelf);

    }

    [Test]
    public void StartTest()
    {

        displayStatusBar.Start();
        Assert.IsFalse(displayStatusBar.statusbar.activeSelf);
        Assert.IsFalse(displayStatusBar.statusbar.activeSelf);
        Assert.IsFalse(displayStatusBar.decision.activeSelf);
        Assert.IsFalse(displayStatusBar.dice.activeSelf);

    }

}
