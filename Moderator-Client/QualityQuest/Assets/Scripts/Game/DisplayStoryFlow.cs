using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoryFlow : MonoBehaviour
{

    public GameObject storyflowElement;
    public TMP_Text storyflowText;
    public Button storyFlowButton;

    public void SetStoryFlow(StoryEvent storyEvent)
    {
        storyflowElement.SetActive(true);
        storyflowText.text = storyEvent.Description;
    }

    public void RemoveStoryFlowListeners()
    {
        storyFlowButton.onClick.RemoveAllListeners();
    }

}
