using System.ComponentModel.DataAnnotations.Schema;

namespace AgentsRest.Models
{
    
    public class AgentModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string NickName { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public StatusAgent StatusAgent { get; set; }
        [NotMapped]
        public List<MissionModel> Missions { get; set; } = [];

        
    }
    public enum StatusAgent
    {
        IsActive,
        IsNnotActive
    }
}
