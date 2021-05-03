using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for all the audio effects of the game, e.g. dice animation sound.
/// </summary>
public class GameAudio : MonoBehaviour
{
    /// <summary>
    /// Audio Source which is used to play back the effect sounds.
    /// </summary>
    public AudioSource effects;

    /// <summary>
    /// This method used to play a sound when a button is clicked.
    /// </summary>
    public void PlayClickSound()
    {
        effects.PlayOneShot(Resources.Load<AudioClip>("sounds/effects/click"));
    }

    /// <summary>
    /// This method used to play a sound when the moderator hovers over a button.
    /// </summary>
    public void PlayHoverSound()
    {
        effects.PlayOneShot(Resources.Load<AudioClip>("sounds/effects/hover"));
    }

    /// <summary>
    /// This method used to play a dice sound for the dice animation.
    /// </summary>
    public void PlayDiceSound()
    {
        effects.clip = Resources.Load<AudioClip>("sounds/effects/dice");
        effects.Play();
    }

    public void PlaySkillChangeSound()
    {
        effects.clip = Resources.Load<AudioClip>("sounds/effects/skillChange");
        effects.Play();
    }
}
