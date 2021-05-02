using UnityEngine;

public class Character
{
    public Skills Abilities { get; }
    public string Name { get; }
    public Sprite Sprite { get; }

    /// <summary>
    /// Constructor for the character class.
    /// </summary>
    /// <param name="abilities">The skill set of the character.</param>
    /// <param name="name">The name of the character.</param>
    /// <param name="sprite">The sprite image of the character.</param>
    public Character(Skills abilities, string name, Sprite sprite)
    {
        this.Abilities = abilities;
        this.Name = name;
        this.Sprite = sprite;
    }
}
