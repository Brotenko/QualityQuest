using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// TEST CLASS
/// </summary>
public class DisplayCharacterTestClass
{
    /// <summary>
    /// Text elements and images which are changed during the game.
    /// </summary>
    public TMP_Text name;
    public Image image;
    public TMP_Text programming;
    public TMP_Text communcation;
    public TMP_Text analytics;
    public TMP_Text party;

    /// <summary>
    /// Test constructor.
    /// </summary>
    public DisplayCharacterTestClass()
    {
        name = new GameObject().AddComponent<TextMeshPro>();
        image = Resources.Load<Image>("Image");
        programming = new GameObject().AddComponent<TextMeshPro>();
        communcation = new GameObject().AddComponent<TextMeshPro>();
        analytics = new GameObject().AddComponent<TextMeshPro>();
        party = new GameObject().AddComponent<TextMeshPro>();
    }

    /// <summary>
    /// Changes the Text and Image of a Character in the CharacterSelection.
    /// </summary>
    /// <param name="character">Character which contains the name, the sprite and the skills of the character.</param>
    public void UpdateCharacter(Character character)
    {
        name.text = character.Name;
        image.sprite = character.Sprite;
        programming.text = character.Abilities.Programming.ToString();
        communcation.text = character.Abilities.Communication.ToString();
        analytics.text = character.Abilities.Analytics.ToString();
        party.text = character.Abilities.Partying.ToString();
    }

}

