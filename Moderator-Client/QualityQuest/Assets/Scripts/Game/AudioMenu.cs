using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioMenu : MonoBehaviour
{
    /// <summary>
    /// Text elements which are used to display the individual volume of each slider in percent.
    /// Text elements only appear in the in game menu.
    /// </summary>
    public TMP_Text masterVolume;
    public TMP_Text musicVolume;
    public TMP_Text effectsVolume;
    public TMP_Text ambientVolume;

    /// <summary>
    /// Sliders which are used to control the volume individualy.
    /// </summary>
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider ambientSlider;

    /// <summary>
    /// the AudioMixer is used to control the volume of individual sounds of the game.
    /// </summary>
    public AudioMixer mixer;

    /// <summary>
    /// Ensures the volume sliders match the AudioMixer after switching scenes.
    /// </summary>
    private void Start()
    {
        UpdateSlider();
    }

    /// <summary>
    /// Sets the individual Sliders to the corresponding values of the AudioMixer.
    /// </summary>
    public void UpdateSlider()
    {
        float mavol, muvol, efvol, amvol;
        mixer.GetFloat("Master", out mavol);
        mixer.GetFloat("Music", out muvol);
        mixer.GetFloat("Effects", out efvol);
        mixer.GetFloat("Ambient", out amvol);
        masterSlider.value = Mathf.Pow(10, mavol / 20);
        musicSlider.value = Mathf.Pow(10, muvol / 20);
        effectsSlider.value = Mathf.Pow(10, efvol / 20);
        ambientSlider.value = Mathf.Pow(10, amvol / 20);
    }

    /// <summary>
    /// Sets the master volume in the AudioMixer.
    /// </summary>
    /// <param name="sliderValue">new master volume</param>
    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        if (masterVolume ?? false)
        {
            masterVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    /// <summary>
    /// Sets the volume of the effects sound in the AudioMixer.
    /// </summary>
    /// <param name="sliderValue">new effects volume</param>
    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
        if (effectsVolume ?? false)
        {
            effectsVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    /// <summary>
    /// Sets the volume of the music sound in the AudioMixer.
    /// </summary>
    /// <param name="sliderValue">new music volume</param>
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        if (musicVolume ?? false)
        {
            musicVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    /// <summary>
    /// Sets the volume of the ambient sound in the AudioMixer.
    /// </summary>
    /// <param name="sliderValue">new ambient volume</param>
    public void SetAmbientLevel(float sliderValue)
    {
        mixer.SetFloat("Ambient", Mathf.Log10(sliderValue) * 20);
        if (ambientVolume ?? false)
        {
            ambientVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }
}
