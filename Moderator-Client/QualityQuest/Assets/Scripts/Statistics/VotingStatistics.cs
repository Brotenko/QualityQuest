using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to save the voting statistics as a list.
/// </summary>
public class VotingStatistics
{
    private List<VotingResult> votingStatistics;

    /// <summary>
    /// Constructs a new VotingStatistics.
    /// </summary>
    /// <param name="votingStatistics"></param>
    public VotingStatistics(List<VotingResult> votingStatistics)
    {
        this.votingStatistics = votingStatistics;
    }
}
