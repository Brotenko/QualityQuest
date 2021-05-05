using NUnit.Framework;

public class SkillsTests
{

    // Test Setup
    private Skills testSkills;
    private Skills testUpdateOne;
    private Skills testUpdateTwo;
    private Skills testUpdateThree;
    private Skills testUpdateFour;
    private Skills testUpdateFive;

    /// <summary>
    /// Method to reset the test values.
    /// </summary>
    [SetUp]
    public void TestSetup()
    {
        testSkills = new Skills(5, 5, 5, 5);
        testUpdateOne = new Skills(1, 1, 1, 1);
        testUpdateTwo = new Skills(0, 0, 1, 2);
        testUpdateThree = new Skills(-1, -2, 0, -8);
        testUpdateFour = new Skills(1, 2, 3, 4);
        testUpdateFive = new Skills(0, 0, 0, 0);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by increasing the value by one.
    /// </summary>
    [Test]
    public void CommunicationIncreasedByOne()
    {
        testSkills.UpdateCommunicationSkill(1);

        Assert.AreEqual(6, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by increasing the value by six.
    /// </summary>
    [Test]
    public void CommunicationIncreasedBySix()
    {
        testSkills.UpdateCommunicationSkill(6);

        Assert.AreEqual(11, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value by 1.
    /// </summary>
    [Test]
    public void CommunicationDecreasedByOne()
    {
        testSkills.UpdateCommunicationSkill(-1);

        Assert.AreEqual(4, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void CommunicationDecreasedByFour()
    {
        testSkills.UpdateCommunicationSkill(-4);

        Assert.AreEqual(1, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateCommunicationSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void CommunicationDecreasedBelowZero()
    {
        testSkills.UpdateCommunicationSkill(-999);

        Assert.AreEqual(0, testSkills.Communication);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by increasing the value by two.
    /// </summary>
    [Test]
    public void AnalyticsIncreasedByTwo()
    {
        testSkills.UpdateAnalyticSkill(2);

        Assert.AreEqual(7, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by increasing the value by four.
    /// </summary>
    [Test]
    public void AnalyticsIncreasedByFive()
    {
        testSkills.UpdateAnalyticSkill(5);

        Assert.AreEqual(10, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void AnalyticsDecreasedByTwo()
    {
        testSkills.UpdateAnalyticSkill(-2);

        Assert.AreEqual(3, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void AnalyticsDecreasedByFive()
    {
        testSkills.UpdateAnalyticSkill(-5);

        Assert.AreEqual(0, testSkills.Analytics);
    }

    /// <summary>
    /// Tests the UpdateAnalyticsSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void AnalyticsDecreasedBelowZero()
    {
        testSkills.UpdateAnalyticSkill(-99);

        Assert.AreEqual(0, testSkills.Analytics);
    }


    /// <summary>
    /// Tests the UpdatePartyingSkill method by increasing the value by 1.
    /// </summary>
    [Test]
    public void PartyingIncreasedByOne()
    {
        testSkills.UpdatePartyingSkill(1);

        Assert.AreEqual(6, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by increasing the value by 3.
    /// </summary>
    [Test]
    public void PartyingIncreasedByThree()
    {
        testSkills.UpdatePartyingSkill(3);

        Assert.AreEqual(8, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value by 3.
    /// </summary>
    [Test]
    public void PartyingDecreasingByThree()
    {
        testSkills.UpdatePartyingSkill(-3);

        Assert.AreEqual(2, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value by 5.
    /// </summary>
    [Test]
    public void PartyingDecreasingByFive()
    {
        testSkills.UpdatePartyingSkill(-5);

        Assert.AreEqual(0, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdatePartyingSkill method by decreasing the value below zero.
    /// </summary>
    [Test]
    public void PartyingDecreasingBelowZero()
    {
        testSkills.UpdatePartyingSkill(-99);

        Assert.AreEqual(0, testSkills.Partying);
    }


    /// <summary>
    /// Tests the UpdateProgrammingSkill method by increasing the value by 6.
    /// </summary>
    [Test]
    public void ProgrammingIncreasedBySix()
    {
        testSkills.UpdateProgrammingSkill(6);

        Assert.AreEqual(11, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by increasing the value by 7.
    /// </summary>
    [Test]
    public void ProgrammingIncreasedBySeven()
    {
        testSkills.UpdateProgrammingSkill(7);

        Assert.AreEqual(12, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value by 2.
    /// </summary>
    [Test]
    public void ProgrammingDecreasedByTwo()
    {
        testSkills.UpdateProgrammingSkill(-2);

        Assert.AreEqual(3, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value by 4.
    /// </summary>
    [Test]
    public void ProgrammingDecreasedByFour()
    {
        testSkills.UpdateProgrammingSkill(-4);

        Assert.AreEqual(1, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateProgrammingSkill method by decreasing the value below 0.
    /// </summary>
    [Test]
    public void ProgrammingDecreasedBelowZero()
    {
        testSkills.UpdateProgrammingSkill(-999);

        Assert.AreEqual(0, testSkills.Programming);
    }

    /// <summary>
    /// Tests the UpdateAbilities method with testUpdateOne.
    /// </summary>
    [Test]
    public void UpdateAbilitiesWithUpdateOne()
    {
        testSkills.UpdateAbilities(testUpdateOne);

        Assert.AreEqual(6, testSkills.Programming);
        Assert.AreEqual(6, testSkills.Analytics);
        Assert.AreEqual(6, testSkills.Communication);
        Assert.AreEqual(6, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdateAbilities method with testUpdateTwo.
    /// </summary>
    [Test]
    public void UpdateAbilitiesWithUpdateTwo()
    {
        testSkills.UpdateAbilities(testUpdateTwo);

        Assert.AreEqual(7, testSkills.Programming);
        Assert.AreEqual(5, testSkills.Analytics);
        Assert.AreEqual(5, testSkills.Communication);
        Assert.AreEqual(6, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdateAbilities method with testUpdateThree.
    /// </summary>
    [Test]
    public void UpdateAbilitiesWithUpdateThree()
    {
        testSkills.UpdateAbilities(testUpdateThree);

        Assert.AreEqual(0, testSkills.Programming);
        Assert.AreEqual(3, testSkills.Analytics);
        Assert.AreEqual(4, testSkills.Communication);
        Assert.AreEqual(5, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdateAbilities method with testUpdateFour.
    /// </summary>
    [Test]
    public void UpdateAbilitiesWithUpdateFour()
    {
        testSkills.UpdateAbilities(testUpdateFour);

        Assert.AreEqual(9, testSkills.Programming);
        Assert.AreEqual(7, testSkills.Analytics);
        Assert.AreEqual(6, testSkills.Communication);
        Assert.AreEqual(8, testSkills.Partying);
    }

    /// <summary>
    /// Tests the UpdateAbilities method with testUpdateFive.
    /// </summary>
    [Test]
    public void UpdateAbilitiesWithUpdateFive()
    {
        testSkills.UpdateAbilities(testUpdateFive);

        Assert.AreEqual(5, testSkills.Programming);
        Assert.AreEqual(5, testSkills.Analytics);
        Assert.AreEqual(5, testSkills.Communication);
        Assert.AreEqual(5, testSkills.Partying);
    }
}
