using UnityEngine;

/// <summary>
/// Method to save the gameState of the game even when switching scenes.
/// </summary>
public class GameState : MonoBehaviour
{

    public static bool gameIsOnline;

    /// <summary>
    /// Awake gets called upon first initialization of the script.
    /// The gameObject gets not destroyed while switching scenes.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
