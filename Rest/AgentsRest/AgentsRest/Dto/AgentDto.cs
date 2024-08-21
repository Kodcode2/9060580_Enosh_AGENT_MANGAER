namespace AgentsRest.Dto
{
    public class AgentDto
    {
        public string Image { get; set; }
        public string NickName { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Status StatusAgent { get; set; }
        public enum Status
        {
            IsActive,
            IsNnotActive
        }
    }
}
