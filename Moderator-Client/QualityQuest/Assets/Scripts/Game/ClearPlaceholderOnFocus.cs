using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class to improve InputField functions.
/// </summary>
public class ClearPlaceholderOnFocus : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TMP_Text PlaceholderText;

    /// <summary>
    /// Hides the PlaceholderText when the user clicks on an InputField.
    /// </summary>
    /// <param name="data"></param>
    public void OnSelect(BaseEventData data)
    {
        PlaceholderText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows the PlaceholderText when the user leaves the InputField.
    /// But the Placeholder will onl be visible when the user hast entered nothing into the InputField.
    /// </summary>
    /// <param name="data"></param>
    public void OnDeselect(BaseEventData data)
    {
        PlaceholderText.gameObject.SetActive(true);
    }
}