using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script, representing the settings menu.
/// </summary>
public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

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
            string option = resolutions[i].width + " x " + resolutions[i].height + " - " + resolutions[i].refreshRate + " Hz";
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
}
