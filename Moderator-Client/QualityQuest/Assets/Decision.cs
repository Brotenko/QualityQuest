using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Decision : MonoBehaviour
{

    public TMP_Text titleA;
    public TMP_Text descriptionA;

    public TMP_Text titleB;
    public TMP_Text descriptionB;

    public TMP_Text titleC;
    public TMP_Text descriptionC;

    public TMP_Text titleD;
    public TMP_Text descriptionD;

    public void LoadDecision()
    {
        titleA.text = "Option A";
        descriptionA.text = "A sieht interessant aus";

        titleB.text = "Option B";
        descriptionB.text = "B sieht interessant aus";

        titleC.text = "Option C";
        descriptionC.text = "C sieht interessant aus";

        titleD.text = "Option D";
        descriptionD.text = "D sieht interessant aus";

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadDecision();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
