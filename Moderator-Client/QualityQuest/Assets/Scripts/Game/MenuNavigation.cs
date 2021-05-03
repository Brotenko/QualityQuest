using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{

    EventSystem system;
    
    void Start()
    {

        system = EventSystem.current;

    }

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
