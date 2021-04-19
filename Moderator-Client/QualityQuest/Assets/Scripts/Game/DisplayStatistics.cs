using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStatistics : MonoBehaviour
{

    public GameObject myPrefab;
    public Transform content;

    public void Start()
    {

        Dictionary<string, int> dic1 = new Dictionary<string, int>();
        dic1.Add("Affe", 7);
        dic1.Add("Elefant", 4);

        Dictionary<string, int> dic2 = new Dictionary<string, int>();
        dic2.Add("Affe", 7);
        dic2.Add("Elefant", 4);
        dic2.Add("Giraffe", 8);

        Dictionary<string, int> dic3 = new Dictionary<string, int>();
        dic3.Add("Affe", 7);
        dic3.Add("Elefant", 4);
        dic3.Add("Giraffe", 8);
        dic3.Add("Tiger", 5);

        List<VotingResult> vr = new List<VotingResult>();
        vr.Add(new VotingResult("Lieblingstier", 11, dic1));
        vr.Add(new VotingResult("Lieblingstier", 19, dic2));
        vr.Add(new VotingResult("Lieblingstier", 24, dic3));

        VotingStatistics vs = new VotingStatistics(vr);

        DisplayAllDescisions(vs);

    }

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
