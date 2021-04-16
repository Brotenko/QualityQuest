using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayStatusbar : MonoBehaviour
{
    public GameObject statusbar;

    public Image characterImage;

    public TMP_Text programmingSkillValue;
    public TMP_Text communicationSkillValue;
    public TMP_Text analyticsSkillValue;
    public TMP_Text partySkillValue;

    public TMP_Text skillChangeProgramming;
    public TMP_Text skillChangeCommunication;
    public TMP_Text skillChangeAnalytics;
    public TMP_Text skillChangeParty;

    private float programmingSkillChangeTimer;
    private float communicationSkillChangeTimer;
    private float analyticsSkillChangeTimer;
    private float partySkillChangeTimer;

    public float skillHideTimerDuration = 5;

    public void SetImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    /// <summary>
    /// displays the skill changes in the statusbar
    /// timer to hide the skill is reset if ShowSkillChange returns true
    /// </summary>
    public void UpdateSkillChanges(Skills skills)
    {

        if (ShowSkillChange(skillChangeProgramming, skills.Programming))
        { programmingSkillChangeTimer = skillHideTimerDuration; };
        if (ShowSkillChange(skillChangeCommunication, skills.Communication))
        { communicationSkillChangeTimer = skillHideTimerDuration; };
        if (ShowSkillChange(skillChangeAnalytics, skills.Analytics))
        { analyticsSkillChangeTimer = skillHideTimerDuration; };
        if (ShowSkillChange(skillChangeParty, skills.Partying))
        { partySkillChangeTimer = skillHideTimerDuration; };

    }

    /// <summary>
    /// displays skill change values in green if positive or red when negativ or hides them if they are 0
    /// whenn the skill is not changed the method returns false otherwise it returns true
    /// </summary>
    /// <param name="text"></param> text element in the ui that displays the skill change
    /// <param name="value"></param> value of the skill change
    public bool ShowSkillChange(TMP_Text text,int value)
    {

        if (value > 0)
        {
            text.gameObject.SetActive(true);
            text.text = value.ToString();
            text.color = new Color(0.3215686f, 0.6352941f, 0.3411765f);
            return true;
        }
        else if (value < 0)
        {
            text.gameObject.SetActive(true);
            text.text = value.ToString();
            text.color = Color.red;
            return true;
        }
        else
        {
            //text.gameObject.SetActive(false);
            return false;
        }

    }

    /// <summary>
    /// displays the amount of point of each skill the Character currently has
    /// </summary>
    public void DisplaySkills(Skills skills)
    {
        this.programmingSkillValue.text = skills.Programming.ToString();
        this.communicationSkillValue.text = skills.Communication.ToString();
        this.analyticsSkillValue.text = skills.Analytics.ToString();
        this.partySkillValue.text = skills.Partying.ToString();
    }

    /// <summary>
    /// shows or hides the statusbar
    /// </summary>
    /// <param name="status"></param> if set to true the statusbar is visible
    public void ShowStatusBar(bool status)
    {
        statusbar.SetActive(status);
    }

    public void Start()
    {
        statusbar.SetActive(false);
    }

    public void Update()
    {
        // hides programming skillchange when timer hits 0
        if (programmingSkillChangeTimer > 0)
        {
            programmingSkillChangeTimer -= Time.deltaTime;
            if(programmingSkillChangeTimer < 0)
            {
                skillChangeProgramming.gameObject.SetActive(false);
            }
        }

        // hides communication skillchange when timer hits 0
        if (communicationSkillChangeTimer > 0)
        {
            communicationSkillChangeTimer -= Time.deltaTime;
            if(communicationSkillChangeTimer < 0)
            {
                skillChangeCommunication.gameObject.SetActive(false);
            }
        }

        // hides analytics skillchange when timer hits 0
        if (analyticsSkillChangeTimer > 0)
        {
            analyticsSkillChangeTimer -= Time.deltaTime;
            if(analyticsSkillChangeTimer < 0)
            {
                skillChangeAnalytics.gameObject.SetActive(false);
            }
        }

        // hides partying skillchange when timer hits 0
        if (partySkillChangeTimer > 0)
        {
            partySkillChangeTimer -= Time.deltaTime;
            if(partySkillChangeTimer < 0)
            {
                skillChangeParty.gameObject.SetActive(false);
            }
        }
    }
}
