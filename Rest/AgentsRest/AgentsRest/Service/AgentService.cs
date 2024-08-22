using AgentsRest.Date;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Service
{
    public class AgentService(ApplicationDbContext dbContext) : IAgentService
    {
        // יצירת סוכן חדש
        public async Task<AgentModel> CreateNewAgentAsync(AgentDto agentDto)
        {
            if (agentDto == null)
            {
                throw new Exception("The object is empty");
            }
            AgentModel agentModel = new()
            {
                Image = agentDto.Image,
                NickName = agentDto.NickName,
                x = -1,
                y = -1,
                StatusAgent = StatusAgent.IsNnotActive,

            };
            await dbContext.AddAsync(agentModel);
            await dbContext.SaveChangesAsync();
            return agentModel; 
        }
        // מחזיר את כל הסוכנים מהמאגר נתונים
        public async Task<List<AgentModel>> GetAllAgentAsync() =>
            await dbContext.Agents.ToListAsync();
        // עדכון מקום של הסוכן
        public async Task<AgentModel> UpdateLocationAgentAsync(LocationDto locationDto, int id)
        {
            var agentModel = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agentModel == null) { throw new Exception("not find"); }
            
            agentModel.x = locationDto.x;
            agentModel.y = locationDto.y;

            await dbContext.SaveChangesAsync();
            return agentModel;
        }
    }
}
