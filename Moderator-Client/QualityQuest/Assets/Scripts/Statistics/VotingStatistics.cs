using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to save the voting statistics as a list.
/// </summary>
public class VotingStatistics
{
    public  List<VotingResult> Statistic { get; }

    /// <summary>
    /// Constructs a new Statistic.
    /// </summary>
    /// <param name="statistic"></param>
    public VotingStatistics(List<VotingResult> statistic)
    {
        this.Statistic = statistic;
    }
}
