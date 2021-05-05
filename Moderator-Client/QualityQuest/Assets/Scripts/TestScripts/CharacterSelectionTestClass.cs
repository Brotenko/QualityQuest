using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TEST CLASS.
/// </summary>
public class CharacterSelectionTestClass
{
    // Scripts which update the displayed characters in the character selection screen.
    public DisplayCharacterTestClass displayNoruso;
    public DisplayCharacterTestClass displayKirogh;
    public DisplayCharacterTestClass displayTurgal;
    public DisplayCharacterTestClass displayLumati;

    // Buttons which are used by the moderator to select a character while in offline mode.
    public Button selectOfflineLumati;
    public Button selectOfflineTurgal;
    public Button selectOfflineKirogh;
    public Button selectOfflineNoruso;

    // Images of the characters
    public Sprite norusoSprite;
    public Sprite lumatiSprite;
    public Sprite turgalSprite;
    public Sprite kiroghSprite;

    // playable characters
    public Character noruso, lumati, turgal, kirogh;

    public CharacterSelectionTestClass()
    {
        displayKirogh = new DisplayCharacterTestClass();
        displayLumati = new DisplayCharacterTestClass();
        displayNoruso = new DisplayCharacterTestClass();
        displayTurgal = new DisplayCharacterTestClass();

        selectOfflineKirogh = new GameObject().AddComponent<Button>();
        selectOfflineTurgal = new GameObject().AddComponent<Button>();
        selectOfflineLumati = new GameObject().AddComponent<Button>();
        selectOfflineNoruso = new GameObject().AddComponent<Button>();
        norusoSprite = null;
        lumatiSprite = null;
        turgalSprite = null;
        kiroghSprite = null;
    }

    /// <summary>
    /// Awake is called when the script is loaded
    /// </summary>
    public void Awake()
    {
        // Initializing all playable characters with associated skills name and image.
        noruso = new Character(new Skills(3, 1, 2, 1), "Noruso", norusoSprite);
        lumati = new Character(new Skills(1, 3, 0, 4), "Lumati", lumatiSprite);
        turgal = new Character(new Skills(2, 2, 2, 2), "Turgal", turgalSprite);
        kirogh = new Character(new Skills(2, 0, 5, 1), "Kirogh", kiroghSprite);
        SetCharacters();
    }

    /// <summary>
    /// Update CharacterSelection screen with created characters.
    /// </summary>
    public void SetCharacters()
    {
        displayNoruso.UpdateCharacter(noruso);
        displayKirogh.UpdateCharacter(kirogh);
        displayTurgal.UpdateCharacter(turgal);
        displayLumati.UpdateCharacter(lumati);
    }

    /// <summary>
    /// Sets the character for the StoryGraph.
    /// Displays the StatusBar.
    /// Updates the skills and the character image in the StatusBar.
    /// </summary>
    /// <param name="character">Character which has been selected for the game by the audience or by the moderator.</param>
    /// <param name="story">Script which contains the StoryGraph.</param> 
    /// <param name="statusBar">Script which updates skills, SkillChanges and the character image in the StatusBar.</param>
    public void InitializeCharacter(Character character, StoryGraph storyGraph, DisplayStatusbar statusBar)
    {
        storyGraph.Character = character;
        statusBar.DisplaySkills(storyGraph.Character.Abilities);
        statusBar.ShowStatusBar(true);
        statusBar.SetImage(character.Sprite);
    }

    /// <summary>
    /// Enables the buttons for selecting a character in offline mode.
    /// </summary>
    public void ActivateOfflineCharacterPickButtons()
    {
        selectOfflineKirogh.interactable = true;
        selectOfflineNoruso.interactable = true;
        selectOfflineTurgal.interactable = true;
        selectOfflineLumati.interactable = true;
    }

    /// <summary>
    /// Disables the buttons for selecting a character in Offline mode.
    /// </summary>
    public void RemoveOfflinePickButtons()
    {
        selectOfflineKirogh.interactable = false;
        selectOfflineNoruso.interactable = false;
        selectOfflineTurgal.interactable = false;
        selectOfflineLumati.interactable = false;
    }
}