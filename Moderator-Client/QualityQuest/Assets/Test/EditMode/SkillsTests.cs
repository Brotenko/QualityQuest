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
    /// Tests the updateCommunicationSkill method by increasing the value by one.
    /// </summary>
    [Test]
    public void communicationIncreasedByOne()
    {
        testSetup();
        testSkills.updateCommunicationSkill(1);

        Assert.AreEqual(6, testSkills.getCommunication());
    }

    /// <summary>
    /// Tests the updateCommunicationSkill method by increasing the value by six.
    /// </summary>
    [Test]
    public void communicationIncreasedBySix()
    {
        testSetup();
        testSkills.updateCommunicationSkill(6);

        Assert.AreEqual(11, testSkills.getCommunication());
    }

    /// <summary>
    /// Tests the updateCommunicationSkill method by decreasing the value by 1.
    /// </summary>
    [Test]
    public void communicationDecreasedByOne()
    {
        testSetup();
        testSkills.updateCommunicationSkill(-1);

        Assert.AreEqual(4, testSkills.getCommunication());
    }

    /// <summary>
    /// Tests the updateCommunicationSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void communicationDecreasedByFour()
    {
        testSetup();
        testSkills.updateCommunicationSkill(-4);

        Assert.AreEqual(1, testSkills.getCommunication());
    }

    /// <summary>
    /// Tests the updateCommunicationSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void communicationDecreasedBelowZero()
    {
        testSetup();
        testSkills.updateCommunicationSkill(-999);

        Assert.AreEqual(0, testSkills.getCommunication());
    }

    /// <summary>
    /// Tests the updateAnalyticsSkill method by increasing the value by two.
    /// </summary>
    [Test]
    public void analyticsIncreasedByTwo()
    {
        testSetup();
        testSkills.updateAnalyticsSkill(2);

        Assert.AreEqual(7, testSkills.getAnalytics());
    }

    /// <summary>
    /// Tests the updateAnalyticsSkill method by increasing the value by four.
    /// </summary>
    [Test]
    public void analyticsIncreasedByFive()
    {
        testSetup();
        testSkills.updateAnalyticsSkill(5);

        Assert.AreEqual(10, testSkills.getAnalytics());
    }

    /// <summary>
    /// Tests the updateAnalyticsSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void analyticsDecreasedByTwo()
    {
        testSetup();
        testSkills.updateAnalyticsSkill(-2);

        Assert.AreEqual(3, testSkills.getAnalytics());
    }

    /// <summary>
    /// Tests the updateAnalyticsSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void analyticsDecreasedByFive()
    {
        testSetup();
        testSkills.updateAnalyticsSkill(-5);

        Assert.AreEqual(0, testSkills.getAnalytics());
    }

    /// <summary>
    /// Tests the updateAnalyticsSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void analyticsDecreasedBelowZero()
    {
        testSetup();
        testSkills.updateAnalyticsSkill(-99);

        Assert.AreEqual(0, testSkills.getAnalytics());
    }


    /// <summary>
    /// Tests the updatePartyingSkill method by increasing the value by 1.
    /// </summary>
    [Test]
    public void partyingIncreasedByOne()
    {
        testSetup();
        testSkills.updatePartyingSkill(1);

        Assert.AreEqual(6, testSkills.getPartying());
    }

    /// <summary>
    /// Tests the updatePartyingSkill method by increasing the value by 3.
    /// </summary>
    [Test]
    public void partyingIncreasedByThree()
    {
        testSetup();
        testSkills.updatePartyingSkill(3);

        Assert.AreEqual(8, testSkills.getPartying());
    }

    /// <summary>
    /// Tests the updatePartyingSkill method by decreasing the value by 3.
    /// </summary>
    [Test]
    public void partyingDecreasingByThree()
    {
        testSetup();
        testSkills.updatePartyingSkill(-3);

        Assert.AreEqual(2, testSkills.getPartying());
    }

    /// <summary>
    /// Tests the updatePartyingSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void partyingDecreasingByFive()
    {
        testSetup();
        testSkills.updatePartyingSkill(-5);

        Assert.AreEqual(0, testSkills.getPartying());
    }

    /// <summary>
    /// Tests the updatePartyingSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void partyingDecreasingBelowZero()
    {
        testSetup();
        testSkills.updatePartyingSkill(-99);

        Assert.AreEqual(0, testSkills.getPartying());
    }


    /// <summary>
    /// Tests the updateProgrammingSkill method by increasing the value by 6.
    /// </summary>
    [Test]
    public void programmingIncreasedBySix()
    {
        testSetup();
        testSkills.updateProgrammingSkill(6);

        Assert.AreEqual(11, testSkills.getProgramming());
    }

    /// <summary>
    /// Tests the updateProgrammingSkill method by increasing the value by 7.
    /// </summary>
    [Test]
    public void programmingIncreasedBySeven()
    {
        testSetup();
        testSkills.updateProgrammingSkill(7);

        Assert.AreEqual(12, testSkills.getProgramming());
    }

    /// <summary>
    /// Tests the updateProgrammingSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void programmingDecreasedByTwo()
    {
        testSetup();
        testSkills.updateProgrammingSkill(-2);

        Assert.AreEqual(3, testSkills.getProgramming());
    }

    /// <summary>
    /// Tests the updateProgrammingSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void programmingDecreasedByFour()
    {
        testSetup();
        testSkills.updateProgrammingSkill(-4);

        Assert.AreEqual(1, testSkills.getProgramming());
    }

    /// <summary>
    /// Tests the updateProgrammingSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void programmingDecreasedBelowZero()
    {
        testSetup();
        testSkills.updateProgrammingSkill(-999);

        Assert.AreEqual(0, testSkills.getProgramming());
    }
}
