using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public TMP_Text titleA;
    public TMP_Text titleB;
    public TMP_Text titleC;
    public TMP_Text titleD;

    public Slider resultA;
    public Slider resultB;
    public Slider resultC;
    public Slider resultD;

    [Range(0.0f, 1.0f)]
    public float valueA = 0.5F;

    [Range(0.0f, 1.0f)]
    public float valueB = 0.5f;

    [Range(0.0f, 1.0f)]
    public float valueC = 0.5f;

    [Range(0.0f, 1.0f)]
    public float valueD = 0.5f;

    public void LoadResult()
    {
        titleA.text = "Option A";
        titleB.text = "Option B";
        titleC.text = "Option C";
        titleD.text = "Option D";
        resultA.value = valueA;
        resultB.value = valueB;
        resultC.value = valueC;
        resultD.value = valueD;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadResult();
    }

    // Update is called once per frame
    void Update()
    {
        //LoadResult();
    }
}
