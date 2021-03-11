using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script, representing the settings menu.
/// </summary>
public class Settings : MonoBehaviour
{
    
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public Toggle muteAllToggle;

    Resolution[] resolutions;

    /// <summary>
    /// On entering of a scene, the current setting will be set, so the game objects represent the same values.
    /// </summary>
    private void Awake()
    {
        float musicVol, soundVol, allVol;
        audioMixer.GetFloat("volumeMusic", out musicVol);
        audioMixer.GetFloat("volumeSound", out soundVol);
        musicVolumeSlider.value = Mathf.Pow(10, (musicVol / 20));
        soundVolumeSlider.value = Mathf.Pow(10, (soundVol / 20));

        audioMixer.GetFloat("volume", out allVol);
        if (allVol == -80)
        {
            muteAllToggle.isOn = true;
        }
        else
        {
            muteAllToggle.isOn = false;
        }
    }

    /// <summary>
    /// Upon the first initialization, the resolution drop down will be filled.
    /// The resolution will be set to max.
    /// </summary>
    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
            
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Method, that is called when setting a new resolution.
    /// </summary>
    /// <param name="resolutionIndex">Index of the nre resolution</param>
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Method, used to set the window size to fullscreen and back to window mode.
    /// </summary>
    /// <param name="isFullScreen">Bool determines if window size will be full screen.</param>
    public void SetFullscreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /// <summary>
    /// Method used to set the overall volume.
    /// </summary>
    /// <param name="volume">Value bigger than 0 and 1 at max.</param>
    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Method used to set the music volume.
    /// </summary>
    /// <param name="volume">Value bigger than 0 and 1 at max.</param>
    public void SetVolumeMusic(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volumeMusic", Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Method used to set the sound effect volume.
    /// </summary>
    /// <param name="volume">Value bigger than 0 and 1 at max.</param>
    public void SetVolumeSound(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volumeSound", Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Method used to mute all audio.
    /// </summary>
    /// <param name="muted">Bool that determines if audio is muted or not.</param>
    public void MuteAllAudio(bool muted)
    {
        if (muted)
        {
            audioMixer.SetFloat("volume", -80);
        }
        else
        {
            audioMixer.SetFloat("volume", 0);
        }
    }
}
