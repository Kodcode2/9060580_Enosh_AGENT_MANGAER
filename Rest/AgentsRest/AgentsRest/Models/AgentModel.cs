namespace AgentsRest.Models
{
    public enum Status
    {
        IsActive = 1,
        IsNotActive = 0
    }
    public class AgentModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string NickName { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Status Status { get; set; }
    }
}
