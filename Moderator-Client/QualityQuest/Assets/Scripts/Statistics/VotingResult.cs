using System.Collections.Generic;

public class VotingResult
{
    public string VotingDecision { get; }
    public KeyValuePair<string, int> VotingOptions { get; }

    /// <summary>
    /// Constructs a new VotingResult.
    /// </summary>
    /// <param name="votingDecision">The decision which the audience voted for.</param>
    /// <param name="votingOptions">The options and number of votes as a KeyValuePair.</param>
    public VotingResult(string votingDecision, KeyValuePair<string, int> votingOptions)
    {
        this.VotingDecision = votingDecision;
        this.VotingOptions = votingOptions;
    }
}