using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class to display the statistics at the end of the game.
/// </summary>
public class DisplayStatisticsDecision : MonoBehaviour
{
    public GameObject optionPanelA;
    public GameObject optionPanelB;
    public GameObject optionPanelC;
    public GameObject optionPanelD;


    public TMP_Text question;
    public TMP_Text questionVotes;

    public TMP_Text answerA;
    public TMP_Text votesA;

    public TMP_Text answerB;
    public TMP_Text votesB;

    public TMP_Text answerC;
    public TMP_Text votesC;

    public TMP_Text answerD;
    public TMP_Text votesD;

    /// <summary>
    /// Hides all decision panels to make sure none of them are visible.
    /// </summary>
    public void HideAllOptions()
    {
        optionPanelA.SetActive(false);
        optionPanelB.SetActive(false);
        optionPanelC.SetActive(false);
        optionPanelD.SetActive(false);
    }

    /// <summary>
    /// Method to display the voting statistics on the screen.
    /// </summary>
    /// <param name="result">The votingResult of the play through.</param>
    public void DisplayDecision(VotingResult result)
    {
        HideAllOptions();

        question.text = result.VotingDecision;
        questionVotes.text = result.VotingCount.ToString();

        var keyValuePairs = new List<KeyValuePair<string, int>>();

        foreach (var option in result.VotingOptions)
        {
            keyValuePairs.Add(option);
        }

        if (keyValuePairs.Count > 0)
        {
            answerA.text = keyValuePairs[0].Key;
            votesA.text = keyValuePairs[0].Value.ToString();
            optionPanelA.SetActive(true);
        }

        if (keyValuePairs.Count > 1)
        {
            answerB.text = keyValuePairs[1].Key;
            votesB.text = keyValuePairs[1].Value.ToString();
            optionPanelB.SetActive(true);
        }

        if (keyValuePairs.Count > 2)
        {
            answerC.text = keyValuePairs[2].Key;
            votesC.text = keyValuePairs[2].Value.ToString();
            optionPanelC.SetActive(true);
        }

        if (keyValuePairs.Count > 3)
        {
            answerD.text = keyValuePairs[3].Key;
            votesD.text = keyValuePairs[3].Value.ToString();
            optionPanelD.SetActive(true);
        }
    }
}
