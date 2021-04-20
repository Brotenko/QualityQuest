using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStatistics : MonoBehaviour
{

    public GameObject myPrefab;
    public Transform content;


    public void DisplayAllDescisions(VotingStatistics statistics)
    {
        List<VotingResult> results = statistics.Statistic;

        for (int i = 0; i < results.Count; i++)
        {
            GameObject obj = Instantiate(myPrefab, content);
            obj.GetComponent<DisplayStatisticsDecision>().DisplayDecision(results[i]);
        }
    }


}
