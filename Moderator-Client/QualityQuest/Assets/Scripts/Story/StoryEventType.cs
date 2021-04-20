using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryEventType
{
    /// <summary>
    /// This Story Event is a Storyflowdecision which can be voted for or be choosen by the moderator
    /// </summary>
    StoryDecision,
    
    /// <summary>
    /// This Story Event is a votingoption for a storyflowdecision
    /// </summary>
    StoryDecisionOption,
    
    /// <summary>
    /// Story Event which happens between storyFlow decisions which can't be voted for
    /// </summary>
    StoryFlow,

    StoryBackground,

    StoryRootEvent
}
