using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent
{
    private int eventId;
    private string description;
    private HashSet<StoryEvent> children;
    private StoryEventType storyType;
    private Skills skillChange;

    /// <summary>
    /// Constructor of the StoryEvent class.
    /// </summary>
    /// <param name="eventId">The Id of the StoryEvent.</param>
    /// <param name="description">The description of the StoryEvent.</param>
    /// <param name="children">The children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="skillChange">The ammount by which the StoryEvent changes the skills of the character.</param>
    public StoryEvent(int eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, Skills skillChange)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
        this.skillChange = skillChange;
    }

    public StoryEvent(int eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
        this.skillChange = null;
    }

    /// <summary>
    /// Getter for the eventId of a StoryEvent.
    /// </summary>
    /// <returns>The eventId of the StoryEvent.</returns>
    public int getEventId()
    {
        return eventId;
    }

    /// <summary>
    /// Getter for the destription of a StoryEvent.
    /// </summary>
    /// <returns>The description of the StoryEvent</returns>
    public string getDescription()
    {
        return description;
    }

    /// <summary>
    /// Getter for the children of a StoryEvent.
    /// </summary>
    /// <returns>The children of the StoryEvent.</returns>
    public HashSet<StoryEvent> getChildren()
    {
        return children;
    }

    /// <summary>
    /// Method to add a child to a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is added.</param>
    public void addChild(StoryEvent child)
    {
        children.Add(child);
    }

    /// <summary>
    /// Method to remove a child from a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is removed.</param>
    public void removeChild(StoryEvent child)
    {
        children.Remove(child);
    }
 
}
