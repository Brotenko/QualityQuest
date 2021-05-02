using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class TestGameAudio : MonoBehaviour
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
        SwitchBackground(BackgroundType.University);
        PlayMusic1();
    }

    public void PlayDiceSound()
    {
        effects.clip = diceSound;
        effects.Play();
    }

    public void SwitchBackground(BackgroundType backgroundType)
    {

        HideAllBackgrounds();
        ambient.volume = 1;

        switch (backgroundType)
        {
            case BackgroundType.University:

                ambient.clip = universitySound;
                ambient.Play();
                universityBackground.SetActive(true);
                break;

            case BackgroundType.Internship:

                ambient.clip = internshipSound;
                ambient.volume = 0.7f;
                ambient.Play();
                internshipBackground.SetActive(true);
                break;

            case BackgroundType.Meeting:

                ambient.clip = meetingSound;
                ambient.Play();
                meetingBackground.SetActive(true);
                break;

            case BackgroundType.Party:

                ambient.clip = partySound;
                ambient.volume = 0.4f;
                ambient.Play();
                partyBackground.SetActive(true);
                break;

            case BackgroundType.Office:

                ambient.clip = officeSound;
                ambient.Play();
                officeBackground.SetActive(true);
                break;

            case BackgroundType.Beach:

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
        SwitchBackground(BackgroundType.University);
    }
    public void SwitchBackgroundInternship()
    {
        SwitchBackground(BackgroundType.Internship);
    }
    public void SwitchBackgroundMeeting()
    {
        SwitchBackground(BackgroundType.Meeting);
    }
    public void SwitchBackgroundParty()
    {
        SwitchBackground(BackgroundType.Party);
    }
    public void SwitchBackgroundOffice()
    {
        SwitchBackground(BackgroundType.Office);
    }
    public void SwitchBackgroundBeach()
    {
        SwitchBackground(BackgroundType.Beach);
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
