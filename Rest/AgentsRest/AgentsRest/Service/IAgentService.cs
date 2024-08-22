using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface IAgentService
    {
        Task<List<AgentModel>> GetAllAgentAsync();
        Task<AgentModel> CreateNewAgentAsync(AgentDto agentDto);
        Task<AgentModel> UpdateLocationAgentAsync(LocationDto locationDto ,int id);
    }
}
