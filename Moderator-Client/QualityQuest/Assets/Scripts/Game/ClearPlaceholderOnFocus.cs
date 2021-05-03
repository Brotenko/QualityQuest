using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearPlaceholderOnFocus : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TMP_Text PlaceholderText;

    public void OnSelect(BaseEventData data)
    {
        PlaceholderText.gameObject.SetActive(false);
    }

    public void OnDeselect(BaseEventData data)
    {
        PlaceholderText.gameObject.SetActive(true);
    }
}