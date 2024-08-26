using System.ComponentModel.DataAnnotations.Schema;
using ClientAgretTarget.Models;

namespace ClientAgretTarget.ViewModel
{
    public class TargetVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string position { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public StatusTarget StatusTarget { get; set; }
        
       

    }
    public enum StatusTarget
    {
        Live,
        Dead
    }
}

