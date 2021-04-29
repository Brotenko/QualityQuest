using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoBackground : MonoBehaviour
{

    /// <summary>
    /// VideoPlayer which is visible in the background and plays the clips with different background animations.
    /// </summary>
    public VideoPlayer player;

    /// <summary>
    /// Method which switches to another background video clips.
    /// </summary>
    /// <param name="clip">New VideoClip which will be visible in the background of the game after the method is called.</param>
    public void SwitchBackground(VideoClip clip)
    {
        player.clip = clip;
    }
}
