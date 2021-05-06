using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenuTest
{

    public AudioMenu gameAudio;

    private float masterLevel;
    private float musicLevel;
    private float effectsLevel;
    private float ambientLevel;

    [SetUp]
    public void SetUp()
    {

        gameAudio = GameObject.Instantiate(new GameObject()).AddComponent<AudioMenu>();

        gameAudio.mixer = Resources.Load<AudioMixer>("Sound");
        gameAudio.masterSlider = Resources.Load<Slider>("SliderA");
        gameAudio.musicSlider = Resources.Load<Slider>("SliderB");
        gameAudio.effectsSlider = Resources.Load<Slider>("SliderC");
        gameAudio.ambientSlider = Resources.Load<Slider>("SliderD");

        gameAudio.masterVolume = new GameObject().AddComponent<TextMeshPro>();
        gameAudio.musicVolume = new GameObject().AddComponent<TextMeshPro>();
        gameAudio.effectsVolume = new GameObject().AddComponent<TextMeshPro>();
        gameAudio.ambientVolume = new GameObject().AddComponent<TextMeshPro>();
    }

    [Test]
    public void UpdateSliderTest()
    {

        gameAudio.mixer.SetFloat("Master", -10);
        gameAudio.mixer.SetFloat("Music", -15);
        gameAudio.mixer.SetFloat("Effects", -20);
        gameAudio.mixer.SetFloat("Ambient", -25);

        gameAudio.UpdateSlider();

        Assert.AreEqual(Mathf.Pow(10,-0.5f),gameAudio.masterSlider.value);
        Assert.AreEqual(Mathf.Pow(10,-0.75f),gameAudio.musicSlider.value);
        Assert.AreEqual(Mathf.Pow(10,-1),gameAudio.effectsSlider.value);
        Assert.AreEqual(Mathf.Pow(10,-1.25f),gameAudio.ambientSlider.value);

        Assert.AreNotEqual(Mathf.Pow(10, -2), gameAudio.masterSlider.value);
        Assert.AreNotEqual(Mathf.Pow(10, -2), gameAudio.musicSlider.value);
        Assert.AreNotEqual(Mathf.Pow(10, -2), gameAudio.effectsSlider.value);
        Assert.AreNotEqual(Mathf.Pow(10, -2), gameAudio.ambientSlider.value);

    }

    [Test]
    public void SetMasterLevelTest()
    {

        gameAudio.SetMasterLevel(1);
        gameAudio.mixer.GetFloat("Master", out masterLevel);
        Assert.AreEqual(0, masterLevel);
        Assert.AreEqual("100%",gameAudio.masterVolume.text);

    }

    [Test]
    public void SetMusicLevelTest()
    {

        gameAudio.SetMusicLevel(1);
        gameAudio.mixer.GetFloat("Music", out musicLevel);
        Assert.AreEqual(0, musicLevel);
        Assert.AreEqual("100%",gameAudio.musicVolume.text);

    }

    [Test]
    public void SetEffectsLevelTest()
    {

        gameAudio.SetEffectsLevel(1);
        gameAudio.mixer.GetFloat("Effects", out effectsLevel);
        Assert.AreEqual(0, effectsLevel);
        Assert.AreEqual("100%",gameAudio.effectsVolume.text);

    }

    [Test]
    public void SetAmbientLevelTest()
    {

        gameAudio.SetAmbientLevel(1);
        gameAudio.mixer.GetFloat("Ambient", out ambientLevel);
        Assert.AreEqual(0, ambientLevel);
        Assert.AreEqual("100%",gameAudio.ambientVolume.text);

    }

}
