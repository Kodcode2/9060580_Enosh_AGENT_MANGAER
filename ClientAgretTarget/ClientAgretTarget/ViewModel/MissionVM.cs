namespace ClientAgretTarget.ViewModel
{
    public class MissionVM
    {
        public int Id { get; set; }
        public int AgentID { get; set; }

       
        public int TargetId { get; set; }
        
        public double TimeRemaining { get; set; }
        public int ActualExecutionTime { get; set; }

        public StatusMission StatusMission { get; set; }

    }
    public enum StatusMission
    {
        offer,
        assignToAMission,
        Ended
    }
}

