using AgentsRest.Date;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;
using static AgentsRest.Utels.Calculations;

namespace AgentsRest.Service
{
    public class TargetService(ApplicationDbContext dbContext,IMissionService missionService) : ITargetService
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
        // יצירת מטרה חדשה
        public async Task<TargetModel> CreateNewTargetAsync(TargetDto targetDto)
        {
            if (targetDto == null)
            {
                throw new Exception("The object is empty");
            }
            TargetModel targetModel = new()
            {
                Image = targetDto.PhotoUrl,
                Name = targetDto.Name,
                position = targetDto.Position,
                x = -1,
                y = -1,
                StatusTarget = StatusTarget.Live,

            };
            await dbContext.AddAsync(targetModel);
            await dbContext.SaveChangesAsync();
            return targetModel;
        }
        // מחזיר את כל המטרות מהמאגר נתונים
        public async Task<List<TargetModel>> GetAllTargetAsync() =>
            await dbContext.Targets.ToListAsync();

        // עדכון מקום של מטרה
        public async Task<TargetModel> UpdateLocationTargetAsync(LocationDto locationDto, int id)
        {
            var IfDirectionInRange = IsInRange1000(locationDto.x,locationDto.y);
            if (!IfDirectionInRange) { throw new Exception($"Locations out of range of the clipboard"); }
            // מביא את המודל של המטרה בעזרת ה id
            var targetModel = await dbContext.Targets.FirstOrDefaultAsync(x => x.Id == id);
            if (targetModel == null) { throw new Exception($"not find targett by id {id}"); }

            targetModel.x = locationDto.x;
            targetModel.y = locationDto.y;

            await dbContext.SaveChangesAsync();
            missionService.CreateMissionByTargetAsync(targetModel);
            return targetModel;
        }

        // להזיז מטרה
        public async Task<TargetModel> moveTargetAsync(DirectionDto directionDto, int id)
        {
            // מושך את המודל של המטרה
            var targetModel = await dbContext.Targets.FirstOrDefaultAsync(x => x.Id == id);
            if (targetModel == null) { throw new Exception($"not find target by id {id}"); }

            // מושך הדיקשינרי את הצעדים של המטרה ובודק האם הכיון קיים
            var IfDirectionExists = _direction.TryGetValue(directionDto.direction, out var risult);
            if (!IfDirectionExists) { throw new Exception($"The direction '{directionDto.direction}' is not correct"); }
            var (x, y) = risult;
            var IfDirectionInRange = IsInRange1000(targetModel.x += x, targetModel.y += y);
            if (!IfDirectionInRange) { throw new Exception($"Locations out of range of the clipboard"); }
            targetModel.x += x;
            targetModel.y += y;
            await dbContext.SaveChangesAsync();
            missionService.CreateMissionByTargetAsync(targetModel);
            missionService.IfMissionIsRrelevantAsync();
            return targetModel;

        }
    }
}
