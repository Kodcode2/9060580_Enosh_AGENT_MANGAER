using System.ComponentModel.DataAnnotations.Schema;
using ClientAgretTarget.Models;

namespace ClientAgretTarget.ViewModel
{
    public class AgentVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string NickName { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public StatusAgent StatusAgent { get; set; }
        


    }
    public enum StatusAgent
    {
        IsActive,
        IsNnotActive
    }
}

