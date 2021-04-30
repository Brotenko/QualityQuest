using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum BackgroundType { UNIVERSITY, INTERNSHIP, MEETING, PARTY, OFFICE, BEACH }

public class GameBackground : MonoBehaviour
{
    public VideoPlayer player;

    /*
    public VideoClip universityBackground;
    public VideoClip internshipBackground;
    public VideoClip meetingBackground;
    public VideoClip beachBackground;
    public VideoClip officeBackground;
    public VideoClip partyBackground;
    */

    public AudioSource ambient;

    /*
    public AudioClip universitySound;
    public AudioClip internshipSound;
    public AudioClip meetingSound;
    public AudioClip partySound;
    public AudioClip officeSound;
    public AudioClip beachSound;
    */

    public void SwitchBackground(BackgroundType backgroundType)
    {

        ambient.volume = 1;

        switch (backgroundType)
        {
            case BackgroundType.UNIVERSITY:

                //ambient.clip = universitySound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/university");
                ambient.Play();
                //player.clip = universityBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/university");
                break;

            case BackgroundType.INTERNSHIP:

                //ambient.clip = internshipSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/internship");
                ambient.volume = 0.7f;
                ambient.Play();
                //player.clip = internshipBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/internship");
                break;

            case BackgroundType.MEETING:

                //ambient.clip = meetingSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/meeting");
                ambient.Play();
                //player.clip = meetingBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/meeting");
                break;

            case BackgroundType.PARTY:

                //ambient.clip = partySound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/party");
                ambient.volume = 0.4f;
                ambient.Play();
                //player.clip = partyBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/party");
                break;

            case BackgroundType.OFFICE:

                //ambient.clip = officeSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/office");
                ambient.Play();
                //player.clip = officeBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/office");
                break;

            case BackgroundType.BEACH:

                //ambient.clip = beachSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/beach");
                ambient.Play();
                //player.clip = beachBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/beach");
                break;

        }

    }

}
