using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStatistics : MonoBehaviour
{
    /// <summary>
    /// Decision Prefab which is added to content and filled with the information of the individual decisions.
    /// </summary>
    public GameObject myPrefab;

    /// <summary>
    /// GameObject which contains all decisions.
    /// </summary>
    public Transform content;

    /// <summary>
    /// Goes through the list of decisions and adds them to the content GameObject.
    /// </summary>
    /// <param name="statistics"></param>
    public void DisplayAllDecisions(VotingStatistics statistics)
    {
        List<VotingResult> results = statistics.Statistic;

        for (int i = 0; i < results.Count; i++)
        {
            GameObject obj = Instantiate(myPrefab, content);
            obj.GetComponent<DisplayStatisticsDecision>().DisplayDecision(results[i]);
        }
    }


}
