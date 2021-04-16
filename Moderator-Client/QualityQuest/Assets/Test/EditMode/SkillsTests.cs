using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SkillsTests
{

    // Test Setup
    Skills testSkills;

    /// <summary>
    /// Method to reset the test values.
    /// </summary>
    public void testSetup()
    {
        testSkills = new Skills(5, 5, 5, 5);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by increasing the value by one.
    /// </summary>
    [Test]
    public void communicationIncreasedByOne()
    {
        testSetup();
        testSkills.UpdateCommunicationSkill(1);

        Assert.AreEqual(6, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by increasing the value by six.
    /// </summary>
    [Test]
    public void communicationIncreasedBySix()
    {
        testSetup();
        testSkills.UpdateCommunicationSkill(6);

        Assert.AreEqual(11, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value by 1.
    /// </summary>
    [Test]
    public void communicationDecreasedByOne()
    {
        testSetup();
        testSkills.UpdateCommunicationSkill(-1);

        Assert.AreEqual(4, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void communicationDecreasedByFour()
    {
        testSetup();
        testSkills.UpdateCommunicationSkill(-4);

        Assert.AreEqual(1, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void communicationDecreasedBelowZero()
    {
        testSetup();
        testSkills.UpdateCommunicationSkill(-999);

        Assert.AreEqual(0, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by increasing the value by two.
    /// </summary>
    [Test]
    public void analyticsIncreasedByTwo()
    {
        testSetup();
        testSkills.UpdateAnalyticSkill(2);

        Assert.AreEqual(7, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by increasing the value by four.
    /// </summary>
    [Test]
    public void analyticsIncreasedByFive()
    {
        testSetup();
        testSkills.UpdateAnalyticSkill(5);

        Assert.AreEqual(10, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void analyticsDecreasedByTwo()
    {
        testSetup();
        testSkills.UpdateAnalyticSkill(-2);

        Assert.AreEqual(3, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void analyticsDecreasedByFive()
    {
        testSetup();
        testSkills.UpdateAnalyticSkill(-5);

        Assert.AreEqual(0, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void analyticsDecreasedBelowZero()
    {
        testSetup();
        testSkills.UpdateAnalyticSkill(-99);

        Assert.AreEqual(0, testSkills.Analytics);
    }


    /// <summary>
    /// Tests the UpdatePartyingSkill method by increasing the value by 1.
    /// </summary>
    [Test]
    public void partyingIncreasedByOne()
    {
        testSetup();
        testSkills.UpdatePartyingSkill(1);

        Assert.AreEqual(6, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by increasing the value by 3.
    /// </summary>
    [Test]
    public void partyingIncreasedByThree()
    {
        testSetup();
        testSkills.UpdatePartyingSkill(3);

        Assert.AreEqual(8, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value by 3.
    /// </summary>
    [Test]
    public void partyingDecreasingByThree()
    {
        testSetup();
        testSkills.UpdatePartyingSkill(-3);

        Assert.AreEqual(2, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void partyingDecreasingByFive()
    {
        testSetup();
        testSkills.UpdatePartyingSkill(-5);

        Assert.AreEqual(0, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void partyingDecreasingBelowZero()
    {
        testSetup();
        testSkills.UpdatePartyingSkill(-99);

        Assert.AreEqual(0, testSkills.Partying);
    }


    /// <summary>
    /// Tests the UpdateProgrammingSkill method by increasing the value by 6.
    /// </summary>
    [Test]
    public void programmingIncreasedBySix()
    {
        testSetup();
        testSkills.UpdateProgrammingSkill(6);

        Assert.AreEqual(11, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by increasing the value by 7.
    /// </summary>
    [Test]
    public void programmingIncreasedBySeven()
    {
        testSetup();
        testSkills.UpdateProgrammingSkill(7);

        Assert.AreEqual(12, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void programmingDecreasedByTwo()
    {
        testSetup();
        testSkills.UpdateProgrammingSkill(-2);

        Assert.AreEqual(3, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void programmingDecreasedByFour()
    {
        testSetup();
        testSkills.UpdateProgrammingSkill(-4);

        Assert.AreEqual(1, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void programmingDecreasedBelowZero()
    {
        testSetup();
        testSkills.UpdateProgrammingSkill(-999);

        Assert.AreEqual(0, testSkills.Programming);
    }
}
