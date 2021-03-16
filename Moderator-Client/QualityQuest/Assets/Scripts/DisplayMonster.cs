using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayMonster : MonoBehaviour
{

    public Monster monster;
    public new TMP_Text name;
    public Image image;
    public TMP_Text programming;
    public TMP_Text communcation;
    public TMP_Text analysis;
    public TMP_Text party;


    // Start is called before the first frame update
    void Start()
    {
        name.text = monster.name;
        image.sprite = monster.image;
        programming.text = monster.programming.ToString();
        communcation.text = monster.communcation.ToString();
        analysis.text = monster.analysis.ToString();
        party.text = monster.party.ToString();
    }

}
