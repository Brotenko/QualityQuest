using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryEvent
{
    private Guid eventId;
    private string description;
    private HashSet<StoryEvent> children;
    private StoryEventType storyType;
    private Skills skillChange;
    private bool randomOption;

    /// <summary>
    /// Constructor of the StoryEvent class.
    /// </summary>
    /// <param name="eventId">The Id of the StoryEvent.</param>
    /// <param name="description">The description of the StoryEvent.</param>
    /// <param name="children">The children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="skillChange">The ammount by which the StoryEvent changes the skills of the character.</param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, Skills skillChange)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
        this.skillChange = skillChange;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The description of the StoryEvent.</param>
    /// <param name="children">The children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The description of the StoryEvent.</param>
    /// <param name="children">The children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="randomEvent"></param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, bool randomOption)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
        this.randomOption = randomOption;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The description of the StoryEvent.</param>
    /// <param name="children">The children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="skillChange">The ammount by which the StoryEvent changes the skills of the character.</param>
    /// <param name="randomEvent"></param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, Skills skillChange, bool randomOption)
    {
        this.eventId = eventId;
        this.description = description;
        this.children = children;
        this.storyType = storyType;
        this.skillChange = skillChange;
        this.randomOption = randomOption;
    }

    /// <summary>
    /// Getter for the eventId of a StoryEvent.
    /// </summary>
    /// <returns>The eventId of the StoryEvent.</returns>
    public Guid GetEventId()
    {
        return eventId;
    }

    /// <summary>
    /// Getter for the destription of a StoryEvent.
    /// </summary>
    /// <returns>The description of the StoryEvent</returns>
    public string GetDescription()
    {
        return description;
    }

    /// <summary>
    /// Getter for the children of a StoryEvent.
    /// </summary>
    /// <returns>The children of the StoryEvent.</returns>
    public HashSet<StoryEvent> GetChildren()
    {
        return children;
    }

    /// <summary>
    /// Getter for the skillsChange of a StoryEvent.
    /// </summary>
    /// <returns></returns>
    public Skills GetSkills()
    {
        return skillChange;
    }

    /// <summary>
    /// Getter for the randomOption of a StoryEvent
    /// </summary>
    /// <returns></returns>
    public bool GetRandomOption()
    {
        return randomOption;
    }

    public StoryEventType GetStoryType()
    {
        return storyType;
    }

    /// <summary>
    /// Method to add a child to a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is added.</param>
    public void AddChild(StoryEvent child)
    {
        children.Add(child);
    }

    /// <summary>
    /// Method to remove a child from a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is removed.</param>
    public void RemoveChild(StoryEvent child)
    {
        children.Remove(child);
    }
 
}
