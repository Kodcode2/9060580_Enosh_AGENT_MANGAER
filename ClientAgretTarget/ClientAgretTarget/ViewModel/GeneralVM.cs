using Humanizer;

namespace ClientAgretTarget.ViewModel
{
    public class GeneralVM
    {
        public int SumAgents { get; set; }
        public int SumTargets { get; set; }
        public int SumMissions { get; set; }
        // יחס סוכנים למטרות
        public float AttitudeOfAgentsToGoals { get; set; }

        // יחס סוכנים למטרות שאפשר לצוות

        public double AgentRatioToGoalsThatAllowedStaff { get; set; }
    }
}
