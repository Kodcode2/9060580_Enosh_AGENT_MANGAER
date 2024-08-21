using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgentsRest.Models
{
    public class MissionModel
    {

        public int Id { get; set; }
        public int AgentID { get; set; }

        public AgentModel Agent { get; set; }
        public int TargetId { get; set; }
        public TargetModel Target { get; set; }
        public int TimeRemaining { get; set; }
        public int ActualExecutionTime { get; set; }

        public Status StatusMission { get; set; }
     
    }
        public enum Status
        {
            offer,
            assignToAMission,
            Ended
        }
    
}
