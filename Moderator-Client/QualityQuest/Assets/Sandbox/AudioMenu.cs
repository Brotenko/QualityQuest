using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioMenu : MonoBehaviour
{
    public TMP_Text masterVolume;
    public TMP_Text musicVolume;
    public TMP_Text effectsVolume;
    public TMP_Text ambientVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider ambientSlider;

    public AudioMixer mixer;

    void Start()
    {
        UpdateSlider();
    }

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

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        if (masterVolume ?? false)
        {
            masterVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
        if (effectsVolume ?? false)
        {
            effectsVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        if (musicVolume ?? false)
        {
            musicVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

    public void SetAmbientLevel(float sliderValue)
    {
        mixer.SetFloat("Ambient", Mathf.Log10(sliderValue) * 20);
        if (ambientVolume ?? false)
        {
            ambientVolume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
    }

}
