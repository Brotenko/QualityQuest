using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OfflineGameManager : MonoBehaviour
{

    public static OfflineGameManager current;

    public DisplayStatusbar statusbar;
    public CharacterSelection characterSelection;
    public GameStory story;
    public VideoBackground video;
    public DisplayDecision decision;
    public DisplayStoryFlow storyflow;
    public ActiveScreenManager screenmanager;

    public DisplayCharacter monster1;
    public DisplayCharacter monster2;
    public DisplayCharacter monster3;
    public DisplayCharacter monster4;




    private void Awake()
    {
        
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else if (current != null)
        {
            Destroy(gameObject);
        }
    }

}
