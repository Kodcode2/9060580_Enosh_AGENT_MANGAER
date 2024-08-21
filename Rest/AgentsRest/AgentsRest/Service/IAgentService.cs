using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface IAgentService
    {
        Task<List<AgentModel>> GetAllAgentAsync();
        Task<AgentModel> CreateNewAgentAsync(AgentModel agentModel);
    }
}
