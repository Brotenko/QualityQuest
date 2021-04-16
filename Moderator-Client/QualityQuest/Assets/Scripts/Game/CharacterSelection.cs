using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    public Sprite norusoSprite;
    public Sprite lumatiSprite;
    public Sprite turgalSprite;
    public Sprite kiroghSprite;

    public Character noruso, lumati, turgal, kirogh;

    private void Awake()
    {
        noruso = new Character(new Skills(3, 1, 2, 1), "Noruso", norusoSprite);
        lumati = new Character(new Skills(1, 3, 0, 4), "Lumati", lumatiSprite);
        turgal = new Character(new Skills(2, 2, 2, 2), "Turgal", turgalSprite);
        kirogh = new Character(new Skills(2, 0, 5, 1), "Kirogh", kiroghSprite);
    }

    public void SetCharacters(DisplayCharacter monster1, DisplayCharacter monster2, DisplayCharacter monster3, DisplayCharacter monster4)
    {
        monster1.UpdateCharacter(noruso);
        monster2.UpdateCharacter(kirogh);
        monster3.UpdateCharacter(turgal);
        monster4.UpdateCharacter(lumati);
    }

    public void InitializeCharacter(Character character, GameStory story, DisplayStatusbar statusBar)
    {
        story.playThrough.Character = character ;
        statusBar.DisplaySkills(story.playThrough.Character.Abilities);
        statusBar.ShowStatusBar(true);
        statusBar.SetImage(character.Sprite);
    }
}
