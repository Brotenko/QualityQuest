

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
    /// StoryEvent to change the backgroundType of the game between StoryEvents.
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
    StoryEnd,

    /// <summary>
    /// StoryEvent to mark the RandomEvent to determine the next StoryEvent depending on the players performance.
    /// </summary>
    StoryWorkshop,

    /// <summary>
    /// StoryEvent if the player gets invited to the workshop.
    /// </summary>
    StoryWorkshopInvite,

    /// <summary>
    /// StoryEvent if the player gets fired at the end of the game.
    /// </summary>
    StoryFired,

    /// <summary>
    /// StoryEvent when the player is not invited to the workshop.
    /// </summary>
    StoryWorkshopNoInvite,

    /// <summary>
    /// StoryEvent with which the player can unlock additional StoryFlowDecisionOptions in later StoryFlowDecisions.
    /// </summary>
    StoryUnlockDecisionOption,


    /// <summary>
    /// Special StoryFlowDecision which has additional StoryFlowDecisionOptions to be unlocked by the player.
    /// </summary>
    StorySpecialDecision,

    /// <summary>
    /// The special StoryFlowDecisionOption.
    /// </summary>
    StorySpecialOption
}
