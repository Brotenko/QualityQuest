using System.Collections.Generic;

public class VotingResult
{
    private string votingDecision;
    private KeyValuePair<string, int> votingOptions;

    /// <summary>
    /// Constructs a new VotingResult.
    /// </summary>
    /// <param name="votingDecision">The decision which the audience voted for.</param>
    /// <param name="votingOptions">The options and number of votes as a KeyValuePair.</param>
    public VotingResult(string votingDecision, KeyValuePair<string, int> votingOptions)
    {
        this.votingDecision = votingDecision;
        this.votingOptions = votingOptions;
    }
}