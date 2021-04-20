using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDecision : MonoBehaviour
{

    public TMP_Text titleA;
    public TMP_Text descriptionA;
    public GameObject panelA;

    public TMP_Text titleB;
    public TMP_Text descriptionB;
    public GameObject panelB;

    public TMP_Text titleC;
    public TMP_Text descriptionC;
    public GameObject panelC;

    public TMP_Text titleD;
    public TMP_Text descriptionD;
    public GameObject panelD;

    public TMP_Text question;

    public void LoadDecision(StoryEvent currentEvent, List<StoryEvent> children)
    {
        descriptionA.text = "";
        descriptionB.text = "";
        descriptionC.text = "";
        descriptionD.text = "";

        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);

        question.text = currentEvent.Description;

        titleA.text = "Option A";
        if (children.Count() >= 1)
        {
            descriptionA.text = children[0].Description;
            panelA.SetActive(true);

        }

        titleB.text = "Option B";
        if (children.Count() >= 2)
        {
            descriptionB.text = children[1].Description;
            panelB.SetActive(true);
        }

        titleC.text = "Option C";
        if (children.Count() >= 3)
        {
            descriptionC.text = children[2].Description;
            panelC.SetActive(true);
        }

        titleD.text = "Option D";
        if (children.Count() >= 4)
        {
            descriptionD.text = children[3].Description;
            panelD.SetActive(true);
        }
    }

    public void LoadOnlineDecision(StoryEvent currentEvent, List<StoryEvent> children)
    {

        descriptionA.text = "";
        descriptionB.text = "";
        descriptionC.text = "";
        descriptionD.text = "";

        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);

        question.text = currentEvent.Description;

        titleA.text = "Option A";
        if (children.Any())
        {
            descriptionA.text = children[0].Description;
            panelA.SetActive(true);
        }

        titleB.text = "Option B";
        if (children.Count() >= 2)
        {
            descriptionB.text = children[1].Description;
            panelB.SetActive(true);
        }

        titleC.text = "Option C";
        if (children.Count() >= 3)
        {
            descriptionC.text = children[2].Description;
            panelC.SetActive(true);
        }

        titleD.text = "Option D";
        if (children.Count() >= 4)
        {
            descriptionD.text = children[3].Description;
            panelD.SetActive(true);
        }
    }

}
