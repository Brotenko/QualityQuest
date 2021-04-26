

public enum StoryEventType
{
    /// <summary>
    /// This Story Event is a StoryFlowDecision which can be voted for or be chosen by the moderator.
    /// </summary>
    StoryDecision,
    
    /// <summary>
    /// This Story Event is a VotingOption for a StoryFlowDecision
    /// </summary>
    StoryDecisionOption,
    
    /// <summary>
    /// Story Event which happens between storyFlow decisions which can't be voted for
    /// </summary>
    StoryFlow,

    /// <summary>
    /// StoryEvent to change the background of the game between StoryEvents.
    /// </summary>
    StoryBackground,

    /// <summary>
    /// The first StoryEvent in the StoryGraph. Used to start the character pick phase.
    /// </summary>
    StoryRootEvent,

    /// <summary>
    /// The last StoryEvent of the game, when reaching the event, the game was successfully completed.
    /// Used to decide how the player did and to display the statistics.
    /// </summary>
    StoryEnd
}
