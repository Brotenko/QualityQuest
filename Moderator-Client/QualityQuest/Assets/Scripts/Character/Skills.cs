
public class Skills
{
    public int Communication { get; set; }
    public int Analytics { get; set; }
    public int Partying { get; set; }
    public int Programming { get; set; }

    /// <summary>
    /// Constructor of the class Skills.
    /// </summary>
    /// <param name="communication">The value of the Communication skill.</param>
    /// <param name="analytic">The value of the Analytics skill.</param>
    /// <param name="partying">The value of the Partying skill.</param>
    /// <param name="programming">The value of the Programming skill.</param>
    public Skills(int communication, int analytic, int partying, int programming)
    {
        this.Communication = communication;
        this.Analytics = analytic;
        this.Partying = partying;
        this.Programming = programming;
    }

    /// <summary>
    /// Method to update the value of Communication. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateCommunication">Update value.</param>
    public void UpdateCommunicationSkill(int updateCommunication)
    {
        Communication += updateCommunication;

        if (Communication < 0)
        {
            Communication = 0;
        }
    }

    /// <summary>
    /// Method to update4 the value of Analytic. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateAnalytics">Update value.</param>
    public void UpdateAnalyticSkill(int updateAnalytic)
    {
        Analytics += updateAnalytic;

        if (Analytics < 0)
        {
            Analytics = 0;
        }
    }

    /// <summary>
    /// Method to update the value of Partying. The value can be increased or decreased.
    /// </summary>
    /// <param name="updatePartying">Update value</param>
    public void UpdatePartyingSkill(int updatePartying)
    {
        Partying += updatePartying;

        if (Partying < 0)
        {
            Partying = 0;
        }
    }

    /// <summary>
    /// Method to update the value of Programming. The value can be increased or decreased.
    /// </summary>
    /// <param name="updateProgramming">Update value.</param>
    public void UpdateProgrammingSkill(int updateProgramming)
    {
        Programming += updateProgramming;

        if (Programming < 0)
        {
            Programming = 0;
        }
    }

    /// <summary>
    /// Method to update all skills.
    /// </summary>
    /// <param name="skills">The skill change.</param>
    public void UpdateAbilities(Skills skills)
    {
        UpdateProgrammingSkill(skills.Programming);
        UpdateCommunicationSkill(skills.Communication);
        UpdateAnalyticSkill(skills.Analytics);
        UpdatePartyingSkill(skills.Partying);
    }
}

