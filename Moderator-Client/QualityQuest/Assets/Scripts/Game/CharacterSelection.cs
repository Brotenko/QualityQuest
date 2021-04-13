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

    public Button selectNoruso;
    public Button selectLumati;
    public Button selectTurgal;
    public Button selectKirogh;

    Character noruso, lumati, turgal, kirogh;

    private void Awake()
    {
        noruso = new Character(new Skills(3, 1, 2, 1), "Noruso", norusoSprite);
        lumati = new Character(new Skills(1, 3, 0, 4), "Lumati", lumatiSprite);
        turgal = new Character(new Skills(2, 2, 2, 2), "Turgal", turgalSprite);
        kirogh = new Character(new Skills(2, 0, 5, 1), "Kirogh", kiroghSprite);
    }

    private void Start()
    {

        OfflineGameManager.current.monster1.UpdateCharacter(noruso);
        OfflineGameManager.current.monster2.UpdateCharacter(lumati);
        OfflineGameManager.current.monster3.UpdateCharacter(turgal);
        OfflineGameManager.current.monster4.UpdateCharacter(kirogh);

        selectNoruso.onClick.AddListener(delegate {
            InitializeCharacter(noruso);
        });

        selectLumati.onClick.AddListener(delegate {
            InitializeCharacter(lumati);
        });

        selectTurgal.onClick.AddListener(delegate {
            InitializeCharacter(turgal);
        });

        selectKirogh.onClick.AddListener(delegate {
            InitializeCharacter(kirogh);
        });
    }

    public void InitializeCharacter(Character character)
    {

        selectNoruso.onClick.RemoveAllListeners();
        selectLumati.onClick.RemoveAllListeners();
        selectTurgal.onClick.RemoveAllListeners();
        selectKirogh.onClick.RemoveAllListeners();

        OfflineGameManager.current.story.playThrough.SetCharacter(character);
        OfflineGameManager.current.statusbar.DisplaySkills(OfflineGameManager.current.story.playThrough.getCharacter().getAbilities());
        OfflineGameManager.current.statusbar.ShowStatusbar(true);
        OfflineGameManager.current.statusbar.SetImage(character.getSprite());
        OfflineGameManager.current.story.PlayGame();

    }

}
