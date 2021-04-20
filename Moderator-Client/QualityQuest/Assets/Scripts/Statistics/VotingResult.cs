using System.Collections.Generic;

public class VotingResult
{

    public int VotingCount { get; }
    public string VotingDecision { get;  }
    public Dictionary<string, int> VotingOptions { get; set; }

    /// <summary>
    /// Constructs a new VotingResult.
    /// </summary>
    /// <param name="votingDecision">The decision which the audience voted for.</param>
    /// <param name="votingOptions">The options and number of votes as a KeyValuePair.</param>
    public VotingResult(string votingDecision, int VotingCount, Dictionary<string, int> votingOptions)
    {
        this.VotingCount = VotingCount;
        this.VotingDecision = votingDecision;
        this.VotingOptions = votingOptions;
    }
}