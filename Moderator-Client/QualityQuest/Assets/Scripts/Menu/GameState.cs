using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool gameIsOnline;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
