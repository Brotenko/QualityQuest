using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void HideAllOptions()
    {
        optionPanelA.SetActive(false);
        optionPanelB.SetActive(false);
        optionPanelC.SetActive(false);
        optionPanelD.SetActive(false);
    }

    public void DisplayDecision(VotingResult result)
    {
        HideAllOptions();

        question.text = result.VotingDecision;
        questionVotes.text = "100% (" + result.VotingCount + ")";

        List<KeyValuePair<string, int>> optionen = new List<KeyValuePair<string, int>>();

        foreach (KeyValuePair<string, int> option in result.VotingOptions)
        {
            optionen.Add(option);
        }

        if (optionen.Count > 0)
        {
            answerA.text = optionen[0].Key;
            votesA.text = (100 * optionen[0].Value / result.VotingCount) + "% (" + optionen[0].Value + ")";
            optionPanelA.SetActive(true);
        }

        if (optionen.Count > 1)
        {
            answerB.text = optionen[1].Key;
            votesB.text = (100 * optionen[1].Value / result.VotingCount) + "% (" + optionen[1].Value + ")";
            optionPanelB.SetActive(true);
        }

        if (optionen.Count > 2)
        {
            answerC.text = optionen[2].Key;
            votesC.text = (100 * optionen[2].Value / result.VotingCount) + "% (" + optionen[2].Value + ")";
            optionPanelC.SetActive(true);
        }

        if (optionen.Count > 3)
        {
            answerD.text = optionen[3].Key;
            votesD.text = (100 * optionen[3].Value / result.VotingCount) + "% (" + optionen[3].Value + ")";
            optionPanelD.SetActive(true);
        }

    }

}
