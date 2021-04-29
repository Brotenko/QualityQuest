using System.Collections.Generic;
using System;
using UnityEngine.Video;

public class StoryEvent
{
    public Guid EventId { get; }
    public string Description { get; }
    public HashSet<StoryEvent> Children { get; }
    public StoryEventType StoryType { get; }
    public Skills SkillChange { get; }
    public bool RandomOption { get; }
    public BackgroundType BackgroundType { get; }
    public RandomType Random { get; }

    /// <summary>
    /// Constructor of the StoryEvent class.
    /// </summary>
    /// <param name="eventId">The Id of the StoryEvent.</param>
    /// <param name="description">The Description of the StoryEvent.</param>
    /// <param name="children">The Children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="skillChange">The ammount by which the StoryEvent changes the skills of the Character.</param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, Skills skillChange)
    {
        this.EventId = eventId;
        this.Description = description;
        this.Children = children;
        this.StoryType = storyType;
        this.SkillChange = skillChange;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The Description of the StoryEvent.</param>
    /// <param name="children">The Children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType)
    {
        this.EventId = eventId;
        this.Description = description;
        this.Children = children;
        this.StoryType = storyType;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The Description of the StoryEvent.</param>
    /// <param name="children">The Children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="randomEvent"></param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, bool randomOption, RandomType randomType)
    {
        this.EventId = eventId;
        this.Description = description;
        this.Children = children;
        this.StoryType = storyType;
        this.RandomOption = randomOption;
        this.Random = randomType;
    }

    /// <summary>
    /// Constructor of the StoryEvent class. 
    /// </summary>
    /// <param name="eventId">The Guid of the StoryEvent.</param>
    /// <param name="description">The Description of the StoryEvent.</param>
    /// <param name="children">The Children StoryEvents of the StoryEvent.</param>
    /// <param name="storyType">The type of the StoryEvent.</param>
    /// <param name="skillChange">The ammount by which the StoryEvent changes the skills of the Character.</param>
    /// <param name="randomEvent"></param>
    public StoryEvent(Guid eventId, string description, HashSet<StoryEvent> children, StoryEventType storyType, Skills skillChange, bool randomOption, RandomType randomType)
    {
        this.EventId = eventId;
        this.Description = description;
        this.Children = children;
        this.StoryType = storyType;
        this.SkillChange = skillChange;
        this.RandomOption = randomOption;
        this.Random = randomType;
    }

    public StoryEvent(BackgroundType backgroundType,HashSet<StoryEvent> children, StoryEventType storyType)
    {
        this.Children = children;
        this.StoryType = storyType;
        this.BackgroundType = backgroundType;
    }

    /// <summary>
    /// Method to add a child to a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is added.</param>
    public void AddChild(StoryEvent child)
    {
        Children.Add(child);
    }

    /// <summary>
    /// Method to remove a child from a StoryEvent.
    /// </summary>
    /// <param name="child">The child that is removed.</param>
    public void RemoveChild(StoryEvent child)
    {
        Children.Remove(child);
    }
}
