using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGraph 
{
    private Character character;
    private StoryEvent root, currentEvent;

    /// <summary>
    /// Constructor for the StoryGraph class.
    /// </summary>
    /// <param name="character">The main character.</param>
    /// <param name="root">The root StoryEvent of the StoryGraph</param>
    /// <param name="currentEvent">The currentEvent of the StoryGraph</param>
    public StoryGraph(Character character, StoryEvent root, StoryEvent currentEvent)
    {
        this.character = character;
        this.root = root;
        this.currentEvent = currentEvent;
    }

    /// <summary>
    /// Getter for the character.
    /// </summary>
    /// <returns>The main character.</returns>
    public Character getCharacter()
    {
        return character;
    }

    /// <summary>
    /// Getter for the root StoryEvent.
    /// </summary>
    /// <returns>The root StoryEvent.</returns>
    public StoryEvent getRoot()
    {
        return root;
    }

    /// <summary>
    /// Getter for the currentEvent.
    /// </summary>
    /// <returns>The current StoryEvent.</returns>
    public StoryEvent getCurrentEvent()
    {
        return currentEvent;
    }

    /// <summary>
    /// Getter for the next possible StoryEvents
    /// </summary>
    /// <returns>The next possible StoryEvents as HashSet.</returns>
    public Hashset<StoryEvent> getNextPossibleEvents()
    {
        return currentEvent.getChildren
    }

    /// <summary>
    /// Method to set a new current StoryEvent
    /// </summary>
    /// <param name="newCurrentEvent">The new current StoryEvent.</param>
    public void setCurrentEvent(StoryEvent newCurrentEvent)
    {
        currentEvent = newCurrentEvent;
    }
}
