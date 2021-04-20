using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public TMP_Text titleA;
    public TMP_Text titleB;
    public TMP_Text titleC;
    public TMP_Text titleD;
    public TMP_Text prompt;

    public Slider resultA;
    public Slider resultB;
    public Slider resultC;
    public Slider resultD;

    public void LoadResult(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, Dictionary<Guid, int> VotingResults, int countVotings)
    {
        this.prompt.text = currentEvent.Description;
        SetMaxValues(countVotings);
        switch (currentEventChildren.Count)
        {
            case 2:
                HideAllSlider();
                resultA.gameObject.SetActive(true);
                resultB.gameObject.SetActive(true);
                titleA.text = currentEventChildren[0].Description;
                titleB.text = currentEventChildren[1].Description;
                resultA.value = VotingResults[currentEventChildren[0].EventId];
                resultB.value = VotingResults[currentEventChildren[1].EventId];
                break;
            case 3:
                HideAllSlider();
                resultA.gameObject.SetActive(true);
                resultB.gameObject.SetActive(true);
                resultC.gameObject.SetActive(true);
                titleA.text = currentEventChildren[0].Description;
                titleB.text = currentEventChildren[1].Description;
                titleC.text = currentEventChildren[2].Description;
                resultA.value = VotingResults[currentEventChildren[0].EventId];
                resultB.value = VotingResults[currentEventChildren[1].EventId];
                resultC.value = VotingResults[currentEventChildren[2].EventId];
                break;
            case 4:
                HideAllSlider();
                resultA.gameObject.SetActive(true);
                resultB.gameObject.SetActive(true);
                resultC.gameObject.SetActive(true);
                resultD.gameObject.SetActive(true);
                titleA.text = currentEventChildren[0].Description;
                titleB.text = currentEventChildren[1].Description;
                titleC.text = currentEventChildren[2].Description;
                titleD.text = currentEventChildren[3].Description;
                resultA.value = VotingResults[currentEventChildren[0].EventId];
                resultB.value = VotingResults[currentEventChildren[1].EventId];
                resultC.value = VotingResults[currentEventChildren[2].EventId];
                resultD.value = VotingResults[currentEventChildren[3].EventId];
                break;
            default:
                break;
        }
    }

    public void SetMaxValues(int maxValue)
    {
        resultA.maxValue = maxValue;
        resultB.maxValue = maxValue;
        resultC.maxValue = maxValue;
        resultD.maxValue = maxValue;
    }

    public void HideAllSlider()
    {
        resultA.gameObject.SetActive(false);
        resultB.gameObject.SetActive(false);
        resultC.gameObject.SetActive(false);
        resultD.gameObject.SetActive(false);
    }
}
