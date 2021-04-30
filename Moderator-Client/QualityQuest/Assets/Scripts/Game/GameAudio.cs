using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{

    public AudioSource effects;

    public void PlayClickSound()
    {
        //effects.PlayOneShot(click);
        effects.PlayOneShot(Resources.Load<AudioClip>("sounds/effects/click"));
    }

    public void PlayHoverSound()
    {
        //effects.PlayOneShot(hover);
        effects.PlayOneShot(Resources.Load<AudioClip>("sounds/effects/hover"));
    }

    public void PlayDiceSound()
    {
        //effects.clip = dice;
        effects.clip = Resources.Load<AudioClip>("sounds/effects/dice");
        effects.Play();
    }

}
