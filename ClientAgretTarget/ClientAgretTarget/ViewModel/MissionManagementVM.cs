namespace ClientAgretTarget.ViewModel
{
    public class MissionManagementVM
    {
        public int ID { get; set; }
        public string ImageA { get; set; }
        public string NickName { get; set; }
        public int xA { get; set; }
        public int yA { get; set; }
        public StatusAgent StatusAgent { get; set; }
        public int AgentID { get; set; }

        public AgentVM Agent { get; set; }
        public int TargetId { get; set; }
        public TargetVM Target { get; set; }
        public double TimeRemaining { get; set; }
        public int ActualExecutionTime { get; set; }
        public float Distance {  get; set; }

        public StatusMission StatusMission { get; set; }
        public string ImageT { get; set; }
        public string Name { get; set; }
        public string position { get; set; }
        public int xT { get; set; }
        public int yT { get; set; }
        public StatusTarget StatusTarget { get; set; }
    }
}
