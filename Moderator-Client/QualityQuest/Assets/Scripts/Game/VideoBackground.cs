using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoBackground : MonoBehaviour
{

    public VideoClip praktika;
    public VideoClip party;
    public VideoClip meeting;
    public VideoClip desk;
    public VideoClip beach;

    public VideoPlayer player;

    public void SwitchBackground(VideoClip clip)
    {
        player.clip = clip;
    }


}
