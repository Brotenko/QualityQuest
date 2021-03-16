using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{

    public GameObject menu;
    public GameObject selectchar;

    /// <summary>
    /// Shows and hides the ingame menu.
    /// </summary>
    public void DisplayMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            selectchar.SetActive(true);
        }
        else
        {
            menu.SetActive(true);
            selectchar.SetActive(false);
        }
    }


}
