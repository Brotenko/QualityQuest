using System.Collections;
using System.Collections.Generic;

public class Skills
{
    int communication, analytics, partying, programming;

    /// <summary>
    /// Constructor of the class Skills.
    /// </summary>
    /// <param name="communication">The value of the communication skill.</param>
    /// <param name="analytics">The value of the analytics skill.</param>
    /// <param name="partying">The value of the partying skill.</param>
    /// <param name="programming">The value of the programming skill.</param>
    public Skills(int communication, int analytics, int partying, int programming)
    {
        this.communication = communication;
        this.analytics = analytics;
        this.partying = partying;
        this.programming = programming;
    }

    /// <summary>
    /// Getter for the communication value.
    /// </summary>
    /// <returns>The communication value.</returns>
    public int getCommunication()
    {
        return communication;
    }

    /// <summary>
    /// Getter for the analytics value.
    /// </summary>
    /// <returns>The analytics value.</returns>
    public int getAnalytics()
    {
        return analytics;
    }

    /// <summary>
    /// Getter for the partying value.
    /// </summary>
    /// <returns>The partying value.</returns>
    public int getPartying()
    {
        return partying;
    }

    /// <summary>
    /// Getter for the programming value.
    /// </summary>
    /// <returns>The programming value.</returns>
    public int getProgramming()
    {
        return programming;
    }

    /// <summary>
    /// Method to update the value of communication. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateCommunication">Update value.</param>
    public void updateCommunicationSkill(int updateCommunication)
    {
        communication += updateCommunication;

        if (communication < 0)
        {
            communication = 0;
        }
    }

    /// <summary>
    /// Method to update4 the value of analytics. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateAnalytics">Update value.</param>
    public void updateAnalyticsSkill(int updateAnalytics)
    {
        analytics += updateAnalytics;

        if (analytics < 0)
        {
            analytics = 0;
        }
    }

    /// <summary>
    /// Method to update the value of partying. The value can be increased or decreased.
    /// </summary>
    /// <param name="updatePartying">Update value</param>
    public void updatePartyingSkill(int updatePartying)
    {
        partying += updatePartying;

        if (partying < 0)
        {
            partying = 0;
        }
    }

    /// <summary>
    /// Method to update the value of programming. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateProgramming">Update value.</param>
    public void updateProgrammingSkill(int updateProgramming)
    {
        programming += updateProgramming;

        if (programming < 0)
        {
            programming = 0;
        }
    }

    public void updateAbilities(Skills skills)
    {
        updateProgrammingSkill(skills.getProgramming());
        updateCommunicationSkill(skills.getCommunication());
        updateAnalyticsSkill(skills.getAnalytics());
        updatePartyingSkill(skills.getPartying());
    }

}

