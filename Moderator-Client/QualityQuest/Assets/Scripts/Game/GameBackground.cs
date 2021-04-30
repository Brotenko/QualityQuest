using UnityEngine;
using UnityEngine.Video;

public enum BackgroundType
{
    UNIVERSITY,
    INTERNSHIP,
    MEETING,
    PARTY,
    OFFICE,
    BEACH
}

public enum Theme
{
    NONE,
    NORMAL,
    PARTY,
    BEACH
}

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
    public AudioSource music;

    /*
    public AudioClip universitySound;
    public AudioClip internshipSound;
    public AudioClip meetingSound;
    public AudioClip partySound;
    public AudioClip officeSound;
    public AudioClip beachSound;
    */

    private Theme currentTheme = Theme.NONE;

    private int standard = 1;
    private int party = 1;

    public void SwitchBackground(BackgroundType backgroundType)
    {

        ambient.volume = 1;

        switch (backgroundType)
        {
            case BackgroundType.UNIVERSITY:

                //ambient.clip = universitySound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/university");
                ambient.Play();
                PlayMusic(Theme.NORMAL);
                //player.clip = universityBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/university");
                break;

            case BackgroundType.INTERNSHIP:

                //ambient.clip = internshipSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/internship");
                ambient.volume = 0.7f;
                ambient.Play();
                PlayMusic(Theme.NORMAL);
                //player.clip = internshipBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/internship");
                break;

            case BackgroundType.MEETING:

                //ambient.clip = meetingSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/meeting");
                ambient.Play();
                PlayMusic(Theme.NORMAL);
                //player.clip = meetingBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/meeting");
                break;

            case BackgroundType.PARTY:

                //ambient.clip = partySound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/party");
                ambient.volume = 0.4f;
                ambient.Play();
                PlayMusic(Theme.PARTY);
                //player.clip = partyBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/party");
                break;

            case BackgroundType.OFFICE:

                //ambient.clip = officeSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/office");
                ambient.Play();
                PlayMusic(Theme.NORMAL);
                //player.clip = officeBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/office");
                break;

            case BackgroundType.BEACH:

                //ambient.clip = beachSound;
                ambient.clip = Resources.Load<AudioClip>("sounds/background/beach");
                ambient.Play();
                PlayMusic(Theme.BEACH);
                //player.clip = beachBackground;
                player.clip = Resources.Load<VideoClip>("videobackgrounds/beach");
                break;

        }

    }

    public void PlayMusic(Theme theme)
    {
        if (currentTheme != theme || !music.isPlaying)
        {
            currentTheme = theme;

            switch (theme)
            {

                case Theme.NORMAL:

                    if (standard == 1)
                    {
                        music.clip = Resources.Load<AudioClip>("sounds/music/standard1");
                        standard = 2;
                    }
                    else
                    {
                        music.clip = Resources.Load<AudioClip>("sounds/music/standard2");
                        standard = 1;
                    }
                    music.Play();
                    break;

                case Theme.PARTY:

                    if (party == 1)
                    {
                        music.clip = Resources.Load<AudioClip>("sounds/music/party1");
                        party = 2;
                    }
                    else
                    {
                        music.clip = Resources.Load<AudioClip>("sounds/music/party2");
                        party = 1;
                    }
                    music.Play();
                    break;

                case Theme.BEACH:
                    music.clip = Resources.Load<AudioClip>("sounds/music/hawaii");
                    music.Play();
                    break;

            }

        }

    }

    private void Update()
    {
        if (!music.isPlaying && currentTheme != Theme.NONE)
        {
            PlayMusic(currentTheme);
        }
    }

}
