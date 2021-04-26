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
    public Color lightBlue = new Color(0, 0.427451f, 0.6980392f);
    public Color darkBlue = new Color(0, 0.3372549f, 0.5490196f);
    public Color lightGreen = new Color(0.0509804f, 0.6980392f, 0.2980392f);
    public Color darkGreen = new Color(0, 0.5490196f, 0.2117647f);

    /// <summary>
    /// Updates the text and votes of each slider to match the options the audience was able to chose.
    /// Makes sure only the sliders that are needed are visible.
    /// Marks the winning option in green.
    /// </summary>
    /// <param name="currentEvent"></param> currently active storyevent
    /// <param name="currentEventChildren"></param> list which contains all children of the currently active storyevent
    /// <param name="votingResults"></param> dictionary witch contains the number of votes and the guid of the coresponding storyevent.
    /// <param name="countVotings"></param> list of all stroyevents the audicence was able to choose.
    public void LoadResult(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, Dictionary<Guid, int> votingResults, int countVotings, string winningOption)
    {
        this.prompt.text = currentEvent.Description;
        SetMaxValues(countVotings);
        HideAllSlider();
        if (currentEventChildren.Count >= 2)
        {
            resultA.gameObject.SetActive(true);
            resultB.gameObject.SetActive(true);
            titleA.text = currentEventChildren[0].Description;
            titleB.text = currentEventChildren[1].Description;
            resultA.value = votingResults[currentEventChildren[0].EventId];
            resultB.value = votingResults[currentEventChildren[1].EventId];
        }
        if (currentEventChildren.Count >= 3)
        {
            resultC.gameObject.SetActive(true);
            titleC.text = currentEventChildren[2].Description;
            resultC.value = votingResults[currentEventChildren[2].EventId];
        }
        if (currentEventChildren.Count >= 4)
        {
            resultD.gameObject.SetActive(true);
            titleD.text = currentEventChildren[3].Description;
            resultD.value = votingResults[currentEventChildren[3].EventId];
        }
        GetWinner(votingResults, currentEventChildren, winningOption);
    }

    /// <summary>
    /// Compares the descriptions of all storyevents of a decision the audience was able to choose and finds out which one matches the winning option.
    /// </summary>
    /// <param name="results"></param>  dictionary witch contains the number of votes and the guid of the coresponding storyevent.
    /// <param name="children"></param> list of all stroyevents the audicence was able to choose.
    public void GetWinner(Dictionary<Guid,int> results, List<StoryEvent> children, string winningOption)
    {

        int winner = 0;

        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].Description.Equals(winningOption))
            {
                winner = i;
                break;
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
