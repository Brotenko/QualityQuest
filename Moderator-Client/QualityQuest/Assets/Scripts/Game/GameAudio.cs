using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{

    public AudioClip dice;
    public AudioClip hover;
    public AudioClip click;

    public AudioSource effects;

    public void PlayClickSound()
    {
        effects.PlayOneShot(click);
    }

    public void PlayHoverSound()
    {
        effects.PlayOneShot(hover);
    }

    public void PlayDiceSound()
    {
        effects.clip = dice;
        effects.Play();
    }

}
