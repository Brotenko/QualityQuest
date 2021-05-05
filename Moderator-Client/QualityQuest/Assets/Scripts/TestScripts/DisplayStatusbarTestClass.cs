using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Class for the statusBar.
/// </summary>
public class DisplayStatusbarTestClass
{

    /// <summary>
    /// UI elements which can be hidden during some parts of the game.
    /// </summary>
    public GameObject statusbar;
    public GameObject decision;
    public GameObject dice;

    /// <summary>
    /// Image of the character that was selected during the CharacterSelection.
    /// </summary>
    public Image characterImage;

    /// <summary>
    /// Text elements for displaying Skills and SkillChange.
    /// </summary>
    public TMP_Text programmingSkillValue;
    public TMP_Text communicationSkillValue;
    public TMP_Text analyticsSkillValue;
    public TMP_Text partySkillValue;

    public TMP_Text skillChangeProgramming;
    public TMP_Text skillChangeCommunication;
    public TMP_Text skillChangeAnalytics;
    public TMP_Text skillChangeParty;

    public TMP_Text decisionTimerTime;

    /// <summary>
    /// Floats which hold the time of each individual timer.
    /// </summary>
    private float programmingSkillChangeTimer;
    private float communicationSkillChangeTimer;
    private float analyticsSkillChangeTimer;
    private float partySkillChangeTimer;
    private float diceTimer = 0;
    private float decisionTimer;
    public float skillHideTimerDuration = 5;

    public DisplayStatusbarTestClass()
    {

        statusbar = new GameObject();
        decision = new GameObject();
        dice = new GameObject();

        characterImage = new GameObject().AddComponent<Image>();

        programmingSkillValue = new GameObject().AddComponent<TextMeshPro>();
        communicationSkillValue = new GameObject().AddComponent<TextMeshPro>();
        analyticsSkillValue = new GameObject().AddComponent<TextMeshPro>();
        partySkillValue = new GameObject().AddComponent<TextMeshPro>();

        skillChangeProgramming = new GameObject().AddComponent<TextMeshPro>();
        skillChangeCommunication = new GameObject().AddComponent<TextMeshPro>();
        skillChangeAnalytics = new GameObject().AddComponent<TextMeshPro>();
        skillChangeParty = new GameObject().AddComponent<TextMeshPro>();

    }

    /// <summary>
    /// Updates the image of the Character in the StatusBar.
    /// </summary>
    /// <param name="sprite">Image of the Selected Character.</param>
    public void SetImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    /// <summary>
    /// Displays the skill changes in the StatusBar.
    /// The timer to hide the skill is reset if ShowSkillChange returns true.
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
    /// Displays skill change values in green if positive or red when negative or hides them if they are 0.
    /// When the skill is not changed the method returns false otherwise it returns true.
    /// </summary>
    /// <param name="text">Text element in the ui that displays the skill change.</param>
    /// <param name="value">Value of the skill change.</param>
    public bool ShowSkillChange(TMP_Text text, int value)
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
            return false;
        }

    }

    /// <summary>
    /// Displays the amount of point of each skill the Character currently has.
    /// </summary>
    public void DisplaySkills(Skills skills)
    {
        this.programmingSkillValue.text = skills.Programming.ToString();
        this.communicationSkillValue.text = skills.Communication.ToString();
        this.analyticsSkillValue.text = skills.Analytics.ToString();
        this.partySkillValue.text = skills.Partying.ToString();
    }

    /// <summary>
    /// Shows or hides the StatusBar.
    /// </summary>
    /// <param name="status">If set to true the StatusBar is visible.</param>
    public void ShowStatusBar(bool status)
    {
        statusbar.SetActive(status);
    }

    /// <summary>
    /// Shows dice icon and sets duration
    /// </summary>
    /// <param name="time">Duration for visibility of dice icon.</param>
    public void DisplayDice(int time)
    {
        diceTimer = time;
        dice.SetActive(true);
    }

    /// <summary>
    /// Shows timer for decision and sets duration.
    /// </summary>
    /// <param name="time">Duration for the decision.</param>
    public void DisplayTimer(int time)
    {
        decisionTimer = time;
        decision.SetActive(true);
    }

    /// <summary>
    /// Gets called at the start of the game.
    /// Disables the statusBar before the character pick phase.
    /// </summary>
    public void Start()
    {
        statusbar.SetActive(false);
        decision.SetActive(false);
        dice.SetActive(false);
    }

    /// <summary>
    /// Method gets called every frame. Updates the timers.
    /// </summary>
    public void Update()
    {

        // Hides programming SkillChange when timer hits 0.
        if (programmingSkillChangeTimer > 0)
        {
            programmingSkillChangeTimer -= Time.deltaTime;
            if (programmingSkillChangeTimer <= 0)
            {
                skillChangeProgramming.gameObject.SetActive(false);
            }
        }

        // Hides communication SkillChange when timer hits 0.
        if (communicationSkillChangeTimer > 0)
        {
            communicationSkillChangeTimer -= Time.deltaTime;
            if (communicationSkillChangeTimer <= 0)
            {
                skillChangeCommunication.gameObject.SetActive(false);
            }
        }

        // Hides analytics SkillChange when timer hits 0.
        if (analyticsSkillChangeTimer > 0)
        {
            analyticsSkillChangeTimer -= Time.deltaTime;
            if (analyticsSkillChangeTimer <= 0)
            {
                skillChangeAnalytics.gameObject.SetActive(false);
            }
        }

        // Hides partying SkillChange when timer hits 0.
        if (partySkillChangeTimer > 0)
        {
            partySkillChangeTimer -= Time.deltaTime;
            if (partySkillChangeTimer <= 0)
            {
                skillChangeParty.gameObject.SetActive(false);
            }
        }

        // Hides dice icon when timer hits 0.
        if (diceTimer > 0)
        {
            diceTimer -= Time.deltaTime;
            if (diceTimer <= 0)
            {
                dice.gameObject.SetActive(false);
            }
        }

        if (!ActiveScreenManager.paused)
        {
            // Hides dice icon when timer hits 0.
            if (decisionTimer > 0)
            {
                decisionTimer -= Time.deltaTime;
                decisionTimerTime.text = ((int)decisionTimer).ToString();
                if (decisionTimer <= 0)
                {
                    decision.gameObject.SetActive(false);
                }
            }
        }
    }
}
