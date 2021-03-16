using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{

    private Skills abilities;
    private string name;

    /// <summary>
    /// Constructor for the character class.
    /// </summary>
    /// <param name="abilities">The abilities of the character.</param>
    /// <param name="name">The Name of the character.</param>
    public Character(Skills abilities, string name)
    {
        this.abilities = abilities;
        this.name = name;
    }

    /// <summary>
    /// Getter for the abilities of the Character.
    /// </summary>
    /// <returns>The Skills attribute of the Character.</returns>
    public Skills getAbilities()
    {
        return abilities;
    }

    /// <summary>
    /// Getter for the name of the Character.
    /// </summary>
    /// <returns>The Name of the character.</returns>
    public string getName()
    {
        return name;
    }
}
