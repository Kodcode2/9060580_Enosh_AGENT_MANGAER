using System.ComponentModel.DataAnnotations.Schema;

namespace AgentsRest.Models
{
    public class TargetModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Status StatusTarget { get; set; }
        [NotMapped]
        public MissionModel Mission { get; set; }
        public enum Status
        {
            Live,
            Dead
        }
    }
}
