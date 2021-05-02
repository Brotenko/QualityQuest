using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.Video;

public class GameBackground : MonoBehaviour
{
    /// <summary>
    /// Elements which are used to play back video and sound within unity.
    /// </summary>
    public VideoPlayer player;
    public AudioSource ambient;
    public AudioSource music;

    /// <summary>
    /// The theme determines the type of music that is played in the background.
    /// The standard and party variables are used to check which AudioClip of a the corresponding theme were played before so the same track isn't played back to back.
    /// </summary>
    private Theme currentTheme = Theme.None;
    private int standard = 1;
    private int party = 1;

    /// <summary>
    /// Method which is called to switch the background, the background sounds, and the music of the game.
    /// </summary>
    /// <param name="backgroundType">Determines which background should be loaded.</param>
    public void SwitchBackground(BackgroundType backgroundType)
    {
        // Some of the tracks were louder than others so some adjustments had to be made.
        // The parameter ist reseted to 1 so those audioClips that need no adjustment stay unchanged.
        ambient.volume = 1;

        switch (backgroundType)
        {
            case BackgroundType.University:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/university");
                ambient.Play();
                PlayMusic(Theme.Normal);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/university");
                break;

            case BackgroundType.Internship:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/internship");
                ambient.volume = 0.7f;
                ambient.Play();
                PlayMusic(Theme.Normal);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/internship");
                break;

            case BackgroundType.Meeting:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/meeting");
                ambient.Play();
                PlayMusic(Theme.Normal);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/meeting");
                break;

            case BackgroundType.Party:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/party");
                ambient.volume = 0.4f;
                ambient.Play();
                PlayMusic(Theme.Party);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/party");
                break;

            case BackgroundType.Office:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/office");
                ambient.Play();
                PlayMusic(Theme.Normal);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/office");
                break;

            case BackgroundType.Beach:

                ambient.clip = Resources.Load<AudioClip>("sounds/background/beach");
                ambient.Play();
                PlayMusic(Theme.Beach);
                player.clip = Resources.Load<VideoClip>("videobackgrounds/beach");
                break;

        }

    }

    /// <summary>
    /// Method which switches between different AudioClips during the game.
    /// </summary>
    /// <param name="theme">This parameter is used to specifie what kind of music should be played.</param>
    public void PlayMusic(Theme theme)
    {
        if (currentTheme != theme || !music.isPlaying)
        {
            currentTheme = theme;

            switch (theme)
            {

                case Theme.Normal:

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

                case Theme.Party:

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

                case Theme.Beach:
                    music.clip = Resources.Load<AudioClip>("sounds/music/hawaii");
                    music.Play();
                    break;

            }

        }

    }

    /// <summary>
    /// This method makes starts playing the next music AudioClip once the current one has ended.
    /// </summary>
    private void Update()
    {
        if (!music.isPlaying && currentTheme != Theme.None)
        {
            PlayMusic(currentTheme);
        }
    }

}
