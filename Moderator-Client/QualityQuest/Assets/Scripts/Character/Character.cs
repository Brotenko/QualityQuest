using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{

    public Skills Abilities { get; }
    public string Name { get; }
    public Sprite Sprite { get; }

    /// <summary>
    /// Constructor for the Character class.
    /// </summary>
    /// <param Name="abilities">The Abilities of the Character.</param>
    /// <param Name="name">The Name of the Character.</param>
    public Character(Skills abilities, string name)
    {
        this.Abilities = abilities;
        this.Name = name;
    }

    public Character(Skills abilities, string name, Sprite sprite)
    {
        this.Abilities = abilities;
        this.Name = name;
        this.Sprite = sprite;
    }
}
