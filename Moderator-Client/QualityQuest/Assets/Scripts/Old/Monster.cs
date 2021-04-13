using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class Monster : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int programming;
    public int communcation;
    public int analysis;
    public int party;
}
