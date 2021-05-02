using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDecision : MonoBehaviour
{
    /// <summary>
    /// Buttons which are used to select a decision in OfflineMode.
    /// </summary>
    public Button selectOfflineA;
    public Button selectOfflineB;
    public Button selectOfflineC;
    public Button selectOfflineD;

    /// <summary>
    /// Buttons which are used to select a decision in OnlineMode.
    /// </summary>
    public Button selectOnlineA;
    public Button selectOnlineB;
    public Button selectOnlineC;
    public Button selectOnlineD;

    /// <summary>
    /// Decision panels with their corresponding title and description.
    /// </summary>

    public TMP_Text titleA;
    public TMP_Text descriptionA;
    public GameObject panelA;

    public TMP_Text titleB;
    public TMP_Text descriptionB;
    public GameObject panelB;

    public TMP_Text titleC;
    public TMP_Text descriptionC;
    public GameObject panelC;

    public TMP_Text titleD;
    public TMP_Text descriptionD;
    public GameObject panelD;

    public TMP_Text question;

    /// <summary>
    /// Hides all decision panels and only sets those to visible that are needed.
    /// Updates the description and title of the individual decision panels.
    /// </summary>
    /// <param name="currentEvent"></param>
    /// <param name="children"></param>
    public void LoadDecision(StoryEvent currentEvent, List<StoryEvent> children)
    {
        descriptionA.text = "";
        descriptionB.text = "";
        descriptionC.text = "";
        descriptionD.text = "";

        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);

        question.text = currentEvent.Description;

        titleA.text = "Option A";
        if (children.Any())
        {
            descriptionA.text = children[0].Description;
            panelA.SetActive(true);
        }

        titleB.text = "Option B";
        if (children.Count() >= 2)
        {
            descriptionB.text = children[1].Description;
            panelB.SetActive(true);
        }

        titleC.text = "Option C";
        if (children.Count() >= 3)
        {
            descriptionC.text = children[2].Description;
            panelC.SetActive(true);
        }

        titleD.text = "Option D";
        if (children.Count() >= 4)
        {
            descriptionD.text = children[3].Description;
            panelD.SetActive(true);
        }
    }

    /// <summary>
    /// Removes all EventListeners of the OfflineMode buttons so clicks on the buttons ar no longer registered.
    /// </summary>
    public void RemoveOfflineDecisionListeners()
    {
        selectOfflineA.onClick.RemoveAllListeners();
        selectOfflineB.onClick.RemoveAllListeners();
        selectOfflineC.onClick.RemoveAllListeners();
        selectOfflineD.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Removes all EventListeners of the OnlineMode buttons so clicks on the buttons ar no longer registered.
    /// </summary>
    public void RemoveOnlineDecisionListeners()
    {
        selectOnlineA.onClick.RemoveAllListeners();
        selectOnlineB.onClick.RemoveAllListeners();
        selectOnlineC.onClick.RemoveAllListeners();
        selectOnlineD.onClick.RemoveAllListeners();
    }
}
