using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{

    Skills abilities;
    string name;

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
}
