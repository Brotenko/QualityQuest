using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCharacter : MonoBehaviour
{

    public new TMP_Text name;
    public Image image;
    public TMP_Text programming;
    public TMP_Text communcation;
    public TMP_Text analytics;
    public TMP_Text party;

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
