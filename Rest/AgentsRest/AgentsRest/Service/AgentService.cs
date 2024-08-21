using AgentsRest.Date;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Service
{
    public class AgentService(ApplicationDbContext dbContext) : IAgentService
    {
        public async Task<AgentModel> CreateNewAgentAsync(AgentModel agentModel)
        {
            if (agentModel == null)
            {
                throw new Exception("The object is empty");
            }
            await dbContext.AddAsync(agentModel);
            await dbContext.SaveChangesAsync();
            return agentModel; 
        }

        public async Task<List<AgentModel>> GetAllAgentAsync() =>
            await dbContext.Agents.ToListAsync();
        
        
    }
}
