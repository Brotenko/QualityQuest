using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum BackgroundType { UNIVERSITY, INTERNSHIP, MEETING, PARTY, OFFICE, BEACH }

public class GameBackground : MonoBehaviour
{
    public VideoPlayer player;

    public VideoClip universityBackground;
    public VideoClip internshipBackground;
    public VideoClip meetingBackground;
    public VideoClip beachBackground;
    public VideoClip officeBackground;
    public VideoClip partyBackground;

    public AudioSource ambient;

    public AudioClip universitySound;
    public AudioClip internshipSound;
    public AudioClip meetingSound;
    public AudioClip partySound;
    public AudioClip officeSound;
    public AudioClip beachSound;

    public void SwitchBackground(BackgroundType backgroundType)
    {

        ambient.volume = 1;

        switch (backgroundType)
        {
            case BackgroundType.UNIVERSITY:

                ambient.clip = universitySound;
                ambient.Play();
                player.clip = universityBackground;
                break;

            case BackgroundType.INTERNSHIP:

                ambient.clip = internshipSound;
                ambient.volume = 0.7f;
                ambient.Play();
                player.clip = internshipBackground;
                break;

            case BackgroundType.MEETING:

                ambient.clip = meetingSound;
                ambient.Play();
                player.clip = meetingBackground;
                break;

            case BackgroundType.PARTY:

                ambient.clip = partySound;
                ambient.volume = 0.4f;
                ambient.Play();
                player.clip = partyBackground;
                break;

            case BackgroundType.OFFICE:

                ambient.clip = officeSound;
                ambient.Play();
                player.clip = officeBackground;
                break;

            case BackgroundType.BEACH:

                ambient.clip = beachSound;
                ambient.Play();
                player.clip = beachBackground;
                break;

        }

    }

}
