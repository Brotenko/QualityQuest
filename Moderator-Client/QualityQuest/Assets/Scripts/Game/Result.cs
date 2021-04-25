using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    /// <summary>
    /// text inside of the individual sliders that display the decisionoption
    /// </summary>
    public TMP_Text titleA;
    public TMP_Text titleB;
    public TMP_Text titleC;
    public TMP_Text titleD;

    public TMP_Text prompt;

    /// <summary>
    /// sliders that visualy represent the number of votes for each decisionoption
    /// </summary>
    public Slider resultA;
    public Slider resultB;
    public Slider resultC;
    public Slider resultD;

    /// <summary>
    /// colors for the sliders.
    /// Green represents the winning option and blue is used for the remaining options.
    /// </summary>
    public Color lightBlue;
    public Color darkBlue;
    public Color lightGreen;
    public Color darkGreen;

    /// <summary>
    /// Updates the text and votes of each slider to match the options the audience was able to chose.
    /// Makes sure only the sliders that are needed are visible.
    /// Marks the winning option in green.
    /// </summary>
    /// <param name="currentEvent"></param> currently active storyevent
    /// <param name="currentEventChildren"></param> list which contains all children of the currently active storyevent
    /// <param name="VotingResults"></param> dictionary witch contains the number of votes and the guid of the coresponding storyevent.
    /// <param name="countVotings"></param> list of all stroyevents the audicence was able to choose.
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

    /// <summary>
    /// Compares all results for all storyevents of a decision the audience was able to choose and picks the one with the most votes.
    /// If two or more decisions have the same ammount of votes the first one of those decisions is picked.
    /// </summary>
    /// <param name="results"></param>  dictionary witch contains the number of votes and the guid of the coresponding storyevent.
    /// <param name="children"></param> list of all stroyevents the audicence was able to choose.
    public void GetWinner(Dictionary<Guid,int> results, List<StoryEvent> children)
    {

        int winner = 0;

        for (int i = 0; i < children.Count; i++)
        {
            if (results[children[winner].EventId] < results[children[i].EventId])
            {
                winner = i;
            }
        }

        SetAllBlue();

        switch (winner)
        {
            case 0: SetGreen(resultA);
                break;
            case 1: SetGreen(resultB);
                break;
            case 2: SetGreen(resultC);
                break;
            case 3: SetGreen(resultD);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// Sets the color of all sliders to blue
    /// </summary>
    public void SetAllBlue()
    {
        SetBlue(resultA);
        SetBlue(resultB);
        SetBlue(resultC);
        SetBlue(resultD);
    }

    /// <summary>
    /// Sets the color of a slider to green
    /// </summary>
    /// <param name="result"></param> slider which will be set to green
    public void SetGreen(Slider result)
    {
        result.GetComponent<Image>().color = darkGreen;
        result.GetComponent<Slider>().fillRect.GetComponent<Image>().color = lightGreen;
    }

    /// <summary>
    /// Sets the color of a slider to blue.
    /// </summary>
    /// <param name="result"></param> slider which will be set to blue.
    public void SetBlue(Slider result)
    {
        result.GetComponent<Image>().color = darkBlue;
        result.GetComponent<Slider>().fillRect.GetComponent<Image>().color = lightBlue;
    }

    /// <summary>
    /// Sets the maxValue of all sliders to the value of the result with the most votes.
    /// </summary>
    /// <param name="maxValue"></param> votes of the result with the most votes.
    public void SetMaxValues(int maxValue)
    {
        resultA.maxValue = maxValue;
        resultB.maxValue = maxValue;
        resultC.maxValue = maxValue;
        resultD.maxValue = maxValue;
    }

    /// <summary>
    /// hides all sliders to make sure none of them is active before renabling those that are needed.
    /// </summary>
    public void HideAllSlider()
    {
        resultA.gameObject.SetActive(false);
        resultB.gameObject.SetActive(false);
        resultC.gameObject.SetActive(false);
        resultD.gameObject.SetActive(false);
    }
}
