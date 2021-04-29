using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum Background {UNIVERSITY,INTERNSHIP,MEETING,PARTY,OFFICE,BEACH}
public class GameAudio : MonoBehaviour
{

    public GameObject universityBackground;
    public GameObject internshipBackground;
    public GameObject meetingBackground;
    public GameObject beachBackground;
    public GameObject officeBackground;
    public GameObject partyBackground;

    public AudioClip universitySound;
    public AudioClip internshipSound;
    public AudioClip meetingSound;
    public AudioClip partySound;
    public AudioClip officeSound;
    public AudioClip beachSound;

    public AudioClip diceSound;

    public AudioClip music1;
    public AudioClip music2;

    public AudioMixer mixer;

    public AudioSource ambient;
    public AudioSource effects;
    public AudioSource music;

    public void Start()
    {
        SwitchBackground(Background.UNIVERSITY);
        PlayMusic1();
    }

    public void PlayDiceSound()
    {
        effects.clip = diceSound;
        effects.Play();
    }

    public void SwitchBackground(Background background)
    {

        HideAllBackgrounds();
        ambient.volume = 1;

        switch (background)
        {
            case Background.UNIVERSITY:

                ambient.clip = universitySound;
                ambient.Play();
                universityBackground.SetActive(true);
                break;

            case Background.INTERNSHIP:

                ambient.clip = internshipSound;
                ambient.volume = 0.7f;
                ambient.Play();
                internshipBackground.SetActive(true);
                break;

            case Background.MEETING:

                ambient.clip = meetingSound;
                ambient.Play();
                meetingBackground.SetActive(true);
                break;

            case Background.PARTY:

                ambient.clip = partySound;
                ambient.volume = 0.4f;
                ambient.Play();
                partyBackground.SetActive(true);
                break;

            case Background.OFFICE:

                ambient.clip = officeSound;
                ambient.Play();
                officeBackground.SetActive(true);
                break;

            case Background.BEACH:

                ambient.clip = beachSound;
                ambient.Play();
                beachBackground.SetActive(true);
                break;

        }

    }

    public void HideAllBackgrounds()
    {
        universityBackground.SetActive(false);
        internshipBackground.SetActive(false);
        meetingBackground.SetActive(false);
        beachBackground.SetActive(false);
        partyBackground.SetActive(false);
        officeBackground.SetActive(false);
    }

    public void SwitchBackgroundUniversity()
    {
        SwitchBackground(Background.UNIVERSITY);
    }
    public void SwitchBackgroundInternship()
    {
        SwitchBackground(Background.INTERNSHIP);
    }
    public void SwitchBackgroundMeeting()
    {
        SwitchBackground(Background.MEETING);
    }
    public void SwitchBackgroundParty()
    {
        SwitchBackground(Background.PARTY);
    }
    public void SwitchBackgroundOffice()
    {
        SwitchBackground(Background.OFFICE);
    }
    public void SwitchBackgroundBeach()
    {
        SwitchBackground(Background.BEACH);
    }
    public void PlayMusic1()
    {
        music.clip = music1;
        music.Play();
    }
    public void PlayMusic2()
    {
        music.clip = music2;
        music.Play();
    }

}
