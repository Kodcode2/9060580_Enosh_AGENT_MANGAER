using ClientAgretTarget.Models;

namespace ClientAgretTarget.Services
{
    public interface IAgentService
    {
        Task<List<AgentModel>> GetAllAgents();
    }
}
