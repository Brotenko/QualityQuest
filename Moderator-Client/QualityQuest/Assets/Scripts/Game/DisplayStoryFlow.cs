using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoryFlow : MonoBehaviour
{

    public GameObject storyflowElement;
    public Button storyflowButton;
    public TMP_Text storyflowText;

    public void LoadStoryFlow(StoryEvent storyevent)
    {
        storyflowElement.SetActive(true);
        storyflowText.text = storyevent.GetDescription();
        storyflowButton.onClick.AddListener(delegate { Contineu(); });

    }

    public void Contineu()
    {
        storyflowElement.SetActive(false);
        storyflowButton.onClick.RemoveAllListeners();
        OfflineGameManager.current.story.PlayGame();
    }


}
