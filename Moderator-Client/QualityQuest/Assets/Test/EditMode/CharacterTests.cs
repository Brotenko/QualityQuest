using NUnit.Framework;
using UnityEngine;

public class CharacterTests
{
    private Character testCharacterOne;

    private Character testCharacterTwo;

    private Character testCharacterThree;

    /// <summary>
    /// Setup for all the PlayerCharacters.
    /// </summary>
    [SetUp]
    public void CharacterTestsSimplePasses()
    {
        var sprite = Sprite.Create(Texture2D.blackTexture, Rect.zero, Vector2.zero);

        testCharacterOne = new Character(new Skills(1, 1, 1, 1), "Harald", sprite);
        testCharacterTwo = new Character(new Skills(2, 3, 4, 5), "Olus", sprite);
        testCharacterThree = new Character(new Skills(10, 9, 8, 7), "Teemo", sprite);
    }

    /// <summary>
    /// Test for the CalculateSkills method with the testCharacterOne.
    /// </summary>
    [Test]
    public void CalculateSkillsTestCharOne()
    {
        Assert.AreEqual(4, testCharacterOne.CalculateSkills());
    }

    /// <summary>
    /// Test for the CalculateSkills method with the testCharacterTwo.
    /// </summary>
    [Test]
    public void CalculateSkillsTestCharTwo()
    {
        Assert.AreEqual(14, testCharacterTwo.CalculateSkills());
    }

    /// <summary>
    /// Test for the CalculateSkills method with the testCharacterTwo.
    /// </summary>
    [Test]
    public void CalculateSkillsTestCharThree()
    {
        Assert.AreEqual(34, testCharacterThree.CalculateSkills());
    }
}
