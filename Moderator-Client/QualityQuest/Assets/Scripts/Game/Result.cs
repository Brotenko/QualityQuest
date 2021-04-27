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
    /// Text inside of the individual sliders that display the DecisionOption.
    /// </summary>
    public TMP_Text titleA;
    public TMP_Text titleB;
    public TMP_Text titleC;
    public TMP_Text titleD;

    /// <summary>
    /// Text inside of the individual sliders that display the amount of votes for each DecisionOption.
    /// </summary>
    public TMP_Text votesA;
    public TMP_Text votesB;
    public TMP_Text votesC;
    public TMP_Text votesD;

    /// <summary>
    /// Text in die navbar which displays the Question corresponding to the DecisionOptions.
    /// </summary>
    public TMP_Text prompt;

    /// <summary>
    /// Sliders that visually represent the number of votes for each DecisionOption.
    /// </summary>
    public Slider resultA;
    public Slider resultB;
    public Slider resultC;
    public Slider resultD;

    /// <summary>
    /// Defines all colors which are used for the sliders.
    /// All colors are set to public so they can be overwritten through the Inspector in unity.
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
<<<<<<< HEAD
    /// <param name="currentEvent">Represents the currently active StoryEvent.</param>
    /// <param name="currentEventChildren">List which contains all children of the currently active StoryEvent.</param>
    /// <param name="votingResults">Dictionary witch contains the number of votes and the guid of the corresponding StoryEvent.</param>
    /// <param name="countVotings">List of all StoryEvents the audience was able to choose.</param>
    public void LoadResult(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, Dictionary<Guid, int> votingResults, int countVotings, string winningOption)
=======
    /// <param name="currentEvent"></param> currently active storyevent
    /// <param name="currentEventChildren"></param> list which contains all children of the currently active storyevent
    /// <param name="VotingResults"></param> dictionary witch contains the number of votes and the guid of the coresponding storyevent.
    /// <param name="countVotings"></param> list of all stroyevents the audicence was able to choose.
    public void LoadResult(StoryEvent currentEvent, List<StoryEvent> currentEventChildren, Dictionary<Guid, int> VotingResults, int countVotings, string winningOption)
>>>>>>> parent of b8d79ce (Small refactoring since a switch case was not the best solution.)
    {
        this.prompt.text = currentEvent.Description;
        SetMaxValues(countVotings);
        switch (currentEventChildren.Count)
        {
<<<<<<< HEAD
            resultA.gameObject.SetActive(true);
            resultB.gameObject.SetActive(true);
            titleA.text = currentEventChildren[0].Description;
            titleB.text = currentEventChildren[1].Description;
            resultA.value = votingResults[currentEventChildren[0].EventId];
            resultB.value = votingResults[currentEventChildren[1].EventId];
            votesA.text = votingResults[currentEventChildren[0].EventId].ToString();
            votesB.text = votingResults[currentEventChildren[1].EventId].ToString();
        }
        if (currentEventChildren.Count >= 3)
        {
            resultC.gameObject.SetActive(true);
            titleC.text = currentEventChildren[2].Description;
            resultC.value = votingResults[currentEventChildren[2].EventId];
            votesC.text = votingResults[currentEventChildren[2].EventId].ToString();
        }
        if (currentEventChildren.Count >= 4)
        {
            resultD.gameObject.SetActive(true);
            titleD.text = currentEventChildren[3].Description;
            resultD.value = votingResults[currentEventChildren[3].EventId];
            votesD.text = votingResults[currentEventChildren[3].EventId].ToString();
=======
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
>>>>>>> parent of b8d79ce (Small refactoring since a switch case was not the best solution.)
        }
        GetWinner(VotingResults, currentEventChildren, winningOption);
    }

    /// <summary>
    /// Compares the descriptions of all StoryEvent of a decision the audience was able to choose and finds out which one matches the winning option.
    /// </summary>
    /// <param name="results">Dictionary witch contains the number of votes and the guid of the corresponding StoryEvent.</param>
    /// <param name="children">List of all StoryEvent the audience was able to choose.</param>
    /// <param name="winningOption">String of the StoryOption which is used to find the corresponding children in the list of StoryEvents.</param>
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
    /// Sets the color of all sliders to blue.
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
    /// <param name="result">Slider which will be set to green.</param>
    public void SetGreen(Slider result)
    {
        result.GetComponent<Image>().color = darkGreen;
        result.GetComponent<Slider>().fillRect.GetComponent<Image>().color = lightGreen;
    }

    /// <summary>
    /// Sets the color of a slider to blue.
    /// </summary>
    /// <param name="result">Slider which will be set to blue.</param>
    public void SetBlue(Slider result)
    {
        result.GetComponent<Image>().color = darkBlue;
        result.GetComponent<Slider>().fillRect.GetComponent<Image>().color = lightBlue;
    }

    /// <summary>
    /// Sets the maxValue of all sliders to the value of the result with the most votes.
    /// </summary>
    /// <param name="maxValue">Votes of the result with the most votes.</param>
    public void SetMaxValues(int maxValue)
    {
        resultA.maxValue = maxValue;
        resultB.maxValue = maxValue;
        resultC.maxValue = maxValue;
        resultD.maxValue = maxValue;
    }

    /// <summary>
    /// Hides all sliders to make sure none of them is active before re enabling those that are needed.
    /// </summary>
    public void HideAllSlider()
    {
        resultA.gameObject.SetActive(false);
        resultB.gameObject.SetActive(false);
        resultC.gameObject.SetActive(false);
        resultD.gameObject.SetActive(false);
    }
}
