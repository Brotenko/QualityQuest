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

    public TMP_Text titleB;
    public TMP_Text descriptionB;
    public Button optionB;

    public TMP_Text titleC;
    public TMP_Text descriptionC;
    public Button optionC;

    public TMP_Text titleD;
    public TMP_Text descriptionD;
    public Button optionD;

    public void LoadDecision(HashSet<StoryEvent> events)
    {
        List<StoryEvent> list = events.ToList();



        titleA.text = "Option A";
        if (events.Count() >= 1)
        {
            descriptionA.text = list[0].GetDescription();
            optionA.onClick.AddListener(delegate { CharacterSelection.CS.Pick(list[0]); });
        }

        titleB.text = "Option B";
        if (events.Count() >= 2)
        {
            descriptionB.text = list[1].GetDescription();
            optionB.onClick.AddListener(delegate { CharacterSelection.CS.Pick(list[1]); });
        }

        titleC.text = "Option C";
        if (events.Count() >= 3)
        {
            descriptionC.text = list[2].GetDescription();
            optionC.onClick.AddListener(delegate { CharacterSelection.CS.Pick(list[2]); });
        }

        titleD.text = "Option D";
        if (events.Count() >= 4)
        {
            descriptionD.text = list[3].GetDescription();
            optionD.onClick.AddListener(delegate { CharacterSelection.CS.Pick(list[3]); });
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
