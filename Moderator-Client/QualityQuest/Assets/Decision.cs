using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Decision : MonoBehaviour
{

    public static Decision current;

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

    public void LoadDecision(HashSet<StoryEvent> events)
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

        titleA.text = "Option A";
        if (events.Count() >= 1)
        {
            descriptionA.text = list[0].GetDescription();
            optionA.onClick.AddListener(delegate { CharacterSelection.current.Pick(list[0]); });
            panelA.SetActive(true);

        }

        titleB.text = "Option B";
        if (events.Count() >= 2)
        {
            descriptionB.text = list[1].GetDescription();
            optionB.onClick.AddListener(delegate { CharacterSelection.current.Pick(list[1]); });
            panelB.SetActive(true);
        }

        titleC.text = "Option C";
        if (events.Count() >= 3)
        {
            descriptionC.text = list[2].GetDescription();
            optionC.onClick.AddListener(delegate { CharacterSelection.current.Pick(list[2]); });
            panelC.SetActive(true);
        }

        titleD.text = "Option D";
        if (events.Count() >= 4)
        {
            descriptionD.text = list[3].GetDescription();
            optionD.onClick.AddListener(delegate { CharacterSelection.current.Pick(list[3]); });
            panelD.SetActive(true);
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Decision.current == null)
        {
            Decision.current = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
