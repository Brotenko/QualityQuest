using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoryFlow : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject storyflowElement;
    public TMP_Text storyflowText;
    public Button storyFlowButton;

    /// <summary>
    /// Sets the StoryFlowPanel to visible and updates the text with the storyFlow description.
    /// </summary>
    /// <param name="storyEvent">Contains the Description that is shown in the StoryFlowPanel.</param>
    public void SetStoryFlow(StoryEvent storyEvent)
    {
        storyflowElement.SetActive(true);
        storyflowText.text = storyEvent.Description;
    }

    /// <summary>
    /// Removes EventListener from the Button so clicks are not registering anymore.
    /// </summary>
    public void RemoveStoryFlowListeners()
    {
        storyFlowButton.onClick.RemoveAllListeners();
    }

}
