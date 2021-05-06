using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Class to display the character and his stats on the screen.
/// </summary>
public class DisplayCharacter : MonoBehaviour
{
    /// <summary>
    /// Text elements and images which are changed during the game.
    /// </summary>
    public new TMP_Text name;
    public Image image;
    public TMP_Text programming;
    public TMP_Text communcation;
    public TMP_Text analytics;
    public TMP_Text party;

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
