using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    // scripts which update the displayed characters in the character selection screen
    public DisplayCharacter displayNoruso;
    public DisplayCharacter displayKirogh;
    public DisplayCharacter displayTurgal;
    public DisplayCharacter displayLumati;

    // buttons which are used by the moderator to select a character while in offline mode
    public Button selectOfflineLumati;
    public Button selectOfflineTurgal;
    public Button selectOfflineKirogh;
    public Button selectOfflineNoruso;

    // images of the characters
    public Sprite norusoSprite;
    public Sprite lumatiSprite;
    public Sprite turgalSprite;
    public Sprite kiroghSprite;

    // playable characters
    public Character noruso, lumati, turgal, kirogh;

    /// <summary>
    /// Awake is called when the script is loaded
    /// </summary>
    private void Awake()
    {
        // initialising all playable characters with associated skills name and image
        noruso = new Character(new Skills(3, 1, 2, 1), "Noruso", norusoSprite);
        lumati = new Character(new Skills(1, 3, 0, 4), "Lumati", lumatiSprite);
        turgal = new Character(new Skills(2, 2, 2, 2), "Turgal", turgalSprite);
        kirogh = new Character(new Skills(2, 0, 5, 1), "Kirogh", kiroghSprite);
        SetCharacters();
    }

    /// <summary>
    /// update characterselection screen with created characters
    /// </summary>
    public void SetCharacters()
    {
        displayNoruso.UpdateCharacter(noruso);
        displayKirogh.UpdateCharacter(kirogh);
        displayTurgal.UpdateCharacter(turgal);
        displayLumati.UpdateCharacter(lumati);
    }

    /// <summary>
    /// Sets the character for the storygraph.
    /// Displays the statusbar.
    /// Updates the skills and the character image in the statusbar.
    /// </summary>
    /// <param name="character"></param> character which has been selected for the game by the audience or by the moderator
    /// <param name="story"></param> script which contains the story graph
    /// <param name="statusBar"></param> script which updates skills, skillchanges and the character image in the satusbar
    public void InitializeCharacter(Character character, GameStory story, DisplayStatusbar statusBar)
    {
        story.playThrough.Character = character ;
        statusBar.DisplaySkills(story.playThrough.Character.Abilities);
        statusBar.ShowStatusBar(true);
        statusBar.SetImage(character.Sprite);
    }

    /// <summary>
    /// enables the buttons for selecting a character in offline mode
    /// </summary>
    public void ActivateOfflineCharacterPickButtons()
    {
        selectOfflineKirogh.gameObject.SetActive(true);
        selectOfflineLumati.gameObject.SetActive(true);
        selectOfflineTurgal.gameObject.SetActive(true);
        selectOfflineNoruso.gameObject.SetActive(true);
    }

    /// <summary>
    /// disables the buttons for selecting a character in offline mode
    /// </summary>
    public void RemoveOfflinePickButtons()
    {
        selectOfflineKirogh.gameObject.SetActive(false);
        selectOfflineLumati.gameObject.SetActive(false);
        selectOfflineTurgal.gameObject.SetActive(false);
        selectOfflineNoruso.gameObject.SetActive(false);
    }
}
