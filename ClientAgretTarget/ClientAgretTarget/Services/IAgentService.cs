using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface IAgentService
    {
        Task<List<AgentVM>> GetAll();
        Task<AgentVM> Details(int id);
        
    }
}
