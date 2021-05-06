using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TestScript : MonoBehaviour
{
    public int a;
    public TMP_Text text;
    public GameObject harald;


    void Start()
    {
        harald.SetActive(false);
        a = 5;
    }

    public void SetText()
    {
        text.text = "haha";
    }

    public void Test()
    {
        harald.SetActive(true);
    }
}
