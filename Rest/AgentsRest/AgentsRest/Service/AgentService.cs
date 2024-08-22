using AgentsRest.Date;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Service
{
    
    public class AgentService(ApplicationDbContext dbContext) : IAgentService
    {
        private readonly Dictionary<string, (int, int)> _direction = new()
        {
            {"nw",(-1,1) },
            {"n",(0,1) },
            {"ne",(1,1) },
            {"w",(-1,0) },
            {"e",(1,0) },
            {"sw",(-1,-1) },
            {"s",(0,-1) },
            {"se",(1,-1) },

        };
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
            // מביא את המודל של הסוכן בעזרת ה id
            var agentModel = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agentModel == null) { throw new Exception($"not find Agent by id {id}"); }
            
            agentModel.x = locationDto.x;
            agentModel.y = locationDto.y;

            await dbContext.SaveChangesAsync();
            return agentModel;
        }

        // להזיז סוכן
        public async Task<AgentModel> moveAgentAsync(DirectionDto directionDto, int id)
        {
            // מושך את המודל של הסוכן
            var agentModel = await dbContext.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agentModel == null) { throw new Exception($"not find Agent by id {id}"); }
            // מושך הדיקשינרי את הצעדים של הסוכן ובודק האם הכיון קיים
            var a = _direction.TryGetValue(directionDto.Direction,out var risult);
            if (!a) { throw new Exception($"The direction '{directionDto.Direction}' is not correct"); }
            var (x, y) = risult;
            agentModel.x += x;
            agentModel.y += y;
            await dbContext.SaveChangesAsync();
            return agentModel;




        }
    }
}
