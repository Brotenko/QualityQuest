using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGraph 
{
    public Character Character { get; set; }
    public StoryEvent Root { get; }
    public StoryEvent CurrentEvent { get; set; }

    /// <summary>
    /// Constructor for the StoryGraph class.
    /// </summary>
    /// <param name="character">The main Character.</param>
    /// <param name="r">The Root StoryEvent of the StoryGraph</param>
    /// <param name="currentEvent">The CurrentEvent of the StoryGraph</param>
    public StoryGraph(Character character, StoryEvent r, StoryEvent currentEvent)
    {
        this.Character = character;
        this.Root = r;
        this.CurrentEvent = currentEvent;
    }

    
    /// <summary>
    /// Method to set a new current StoryEvent
    /// </summary>
    /// <param name="newCurrentEvent">The new current StoryEvent.</param>
    public void setCurrentEvent(StoryEvent newCurrentEvent)
    {
        CurrentEvent = newCurrentEvent;
    }
}
