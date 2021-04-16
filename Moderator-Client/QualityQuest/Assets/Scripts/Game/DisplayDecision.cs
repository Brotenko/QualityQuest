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
    public Button optionA;
    public GameObject panelA;

    public TMP_Text titleB;
    public TMP_Text descriptionB;
    public Button optionB;
    public GameObject panelB;

    public TMP_Text titleC;
    public TMP_Text descriptionC;
    public Button optionC;
    public GameObject panelC;

    public TMP_Text titleD;
    public TMP_Text descriptionD;
    public Button optionD;
    public GameObject panelD;

    public TMP_Text question;

    public void LoadDecision(StoryEvent currentEvent, HashSet<StoryEvent> events)
    {
        List<StoryEvent> list = events.ToList();

        optionA.onClick.RemoveAllListeners();
        optionB.onClick.RemoveAllListeners();
        optionC.onClick.RemoveAllListeners();
        optionD.onClick.RemoveAllListeners();

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
        if (events.Count() >= 1)
        {
            descriptionA.text = list[0].Description;
            optionA.onClick.AddListener(delegate { OfflineGameManager.current.story.SetCurrentEvent(list[0]); });
            panelA.SetActive(true);

        }

        titleB.text = "Option B";
        if (events.Count() >= 2)
        {
            descriptionB.text = list[1].Description;
            optionB.onClick.AddListener(delegate { OfflineGameManager.current.story.SetCurrentEvent(list[1]); });
            panelB.SetActive(true);
        }

        titleC.text = "Option C";
        if (events.Count() >= 3)
        {
            descriptionC.text = list[2].Description;
            optionC.onClick.AddListener(delegate { OfflineGameManager.current.story.SetCurrentEvent(list[2]); });
            panelC.SetActive(true);
        }

        titleD.text = "Option D";
        if (events.Count() >= 4)
        {
            descriptionD.text = list[3].Description;
            optionD.onClick.AddListener(delegate { OfflineGameManager.current.story.SetCurrentEvent(list[3]); });
            panelD.SetActive(true);
        }

    }

}