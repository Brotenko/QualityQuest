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
        name.text = character.getName();
        image.sprite = character.getSprite();
        programming.text = character.getAbilities().getProgramming().ToString();
        communcation.text = character.getAbilities().getCommunication().ToString();
        analytics.text = character.getAbilities().getAnalytics().ToString();
        party.text = character.getAbilities().getPartying().ToString();
    }

}
