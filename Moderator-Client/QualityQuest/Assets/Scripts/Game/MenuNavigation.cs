using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class for to improve the menu navigation.
/// </summary>
public class MenuNavigation : MonoBehaviour
{

    EventSystem system;
    
    /// <summary>
    /// Method to start the EventSystem.
    /// </summary>
    void Start()
    {
        system = EventSystem.current;
    }

    /// <summary>
    /// Allows the user after clicking into a InputField to navigate with tab to the next field
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && system.currentSelectedGameObject != null)
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
}
