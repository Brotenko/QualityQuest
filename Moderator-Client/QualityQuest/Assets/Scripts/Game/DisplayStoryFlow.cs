using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoryFlow : MonoBehaviour
{

    public GameObject storyflowElement;
    public TMP_Text storyflowText;

    public void LoadStoryFlow(StoryEvent stroyEvent)
    {
        storyflowElement.SetActive(true);
        storyflowText.text = stroyEvent.Description;
    }

    public void SetStoryFlow(StoryEvent storyEvent)
    {
        storyflowElement.SetActive(true);
        storyflowText.text = storyEvent.Description;
    }

}
