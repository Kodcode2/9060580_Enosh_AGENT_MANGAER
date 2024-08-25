using AgentsRest.Date;
using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Utels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using static AgentsRest.Utels.Calculations;
namespace AgentsRest.Service
{
    public class MissionService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IMissionService
    {
        // מחזיר את כל המשימות
        public async Task<List<MissionModel>> GetAllMissionAsync()
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            return await dbContext.Missions.ToListAsync();
        }

        //מרדף של סוכן אחרי מטרה על ידי איתור סוכנים ומטרות על ידי משימות בפעולה
        public async Task AgentsPursuitAsync()
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();

            var missions = dbContext.Missions.Where(x => x.StatusMission == StatusMission.assignToAMission).ToList();
            foreach (var mission in missions)
            {
                TargetModel? target = await dbContext.Targets.FindAsync(mission.TargetId);
                AgentModel? agent = await dbContext.Agents.FindAsync(mission.AgentID);
                if (target != null && agent != null)
                {
                    agent.x = target.x > agent.x ? agent.x + 1 : agent.x;
                    agent.y = target.y > agent.y ? agent.y + 1 : agent.y;
                    agent.x = target.x < agent.x ? agent.x - 1 : agent.x;
                    agent.y = target.y < agent.y ? agent.y - 1 : agent.y;
                    if (agent.x == target.x && agent.y == target.y)
                    {
                        mission.StatusMission = StatusMission.Ended;
                        target.StatusTarget = StatusTarget.Dead;
                        agent.StatusAgent = StatusAgent.IsNnotActive;
                        mission.TimeRemaining = 0;
                    }
                    var IfDirectionInRange = IsInRange1000(agent.x, agent.y);
                    if (!IfDirectionInRange) { throw new Exception($"Locations out of range of the clipboard"); }
                    mission.TimeRemaining = DistanceCalculation(agent.x, agent.y, target.x, target.y) / 5;
                    await dbContext.SaveChangesAsync();
                }

            }
        }

        // בדיקה האם יש אופיצה למשימה כשסוכן זז
        public async void CreateMissionByAgentAsync(AgentModel agent)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            // יצירת רשימה של מטרות חייים
            var targetlive = dbContext.Targets.Where(x => x.StatusTarget == StatusTarget.Live).ToList();


            foreach (var target in targetlive)
            {
                int xA = agent.x;
                int xT = target.x;
                int yA = agent.y;
                int yT = target.y;

                var ifExistsAMission = dbContext.Missions.Any(x => x.TargetId == target.Id && x.AgentID == agent.Id);
                // שולח לבדיקה מה המרחק בין המטרה לסוכן
                var distance = DistanceCalculation(xA, yA, xT, yT);

                // מביא את כל המשימות ש המטרה נמצאת בהם
                var targetInMission = dbContext.Missions.Where(x => x.TargetId == target.Id).ToList();

                // ואז מסנן ומביא רשימה רק עם המשימות הפעילות 
                targetInMission = targetInMission.Where(x => x.StatusMission == StatusMission.assignToAMission).ToList();

                // פה אני בודק האם המטרה נמצאת ברדיוס שאפשר להציע משימה לסוכן ו שהרשימה רייקה שזה אומר שאין משימות פעילות על המטרה שלי
                if (distance <= 200 && targetInMission.IsNullOrEmpty() && !ifExistsAMission)
                {
                    MissionModel newModel = new()
                    {
                        AgentID = agent.Id,
                        TargetId = target.Id,
                        TimeRemaining = distance / 5,
                        StatusMission = StatusMission.offer,
                    };
                    await dbContext.AddAsync(newModel);
                    await dbContext.SaveChangesAsync();
                }
            }

        }
        // בדיקה באמצעות המטרה שזזה האם יש סוכן ברדיוס 
        public async void CreateMissionByTargetAsync(TargetModel target)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            // יצירת רשימה של סוכנים רדומים
            var agentsNotActive = dbContext.Agents.Where(x => x.StatusAgent == StatusAgent.IsNnotActive).ToList();


            foreach (var agent in agentsNotActive)
            {
                int xT = target.x;
                int xA = agent.x;
                int yT = target.y;
                int yA = agent.y;

                var ifExistsAMission = dbContext.Missions.Any(x => x.TargetId == target.Id && x.AgentID == agent.Id);

                // שולח לבדיקה מה המרחק בין המטרה לסוכן
                var distance = DistanceCalculation(xA, yA, xT, yT);

                // מביא את כל המשימות ש הסוכן נמצאת בהם
                var agentsInMission = dbContext.Missions.Where(x => x.AgentID == agent.Id).ToList();

                // ואז מסנן ומביא רשימה רק עם המשימות הפעילות 
                agentsInMission = agentsInMission.Where(x => x.StatusMission == StatusMission.assignToAMission).ToList();

                // פה אני בודק האם הסוכן נמצאת ברדיוס שאפשר להציע משימה לסוכן ו שהרשימה רייקה שזה אומר שאין משימות פעילות של הסוכן שלי
                if (distance <= 200 && agentsInMission.IsNullOrEmpty() && !ifExistsAMission)
                {
                    MissionModel newModel = new()
                    {
                        AgentID = agent.Id,
                        TargetId = target.Id,
                        TimeRemaining = distance / 5,
                        StatusMission = StatusMission.offer,
                    };
                    await dbContext.AddAsync(newModel);
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        // מחק משימה עם היא לא רלוונטית
        public async void IfMissionIsRrelevantAsync()
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            // מביא את כל ההצעות למשימה
            var MissionOffer = dbContext.Missions.Where(x => x.StatusMission == StatusMission.offer).ToList();

            foreach (var mission in MissionOffer)
            {
                // ובודק האם זה עדין רלוונטי
                TargetModel? target = await dbContext.Targets.FindAsync(mission.TargetId);
                AgentModel? agent = await dbContext.Agents.FindAsync(mission.AgentID);

                var distance = DistanceCalculation(agent.x, agent.y, target.x, target.y);
                if (distance > 200)
                {
                    dbContext.Remove(mission);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        // להפוך משימה למשימה מצוותת
        public async Task<MissionModel> CommandmentToMissionAsync(int id)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            MissionModel? mission = await dbContext.Missions.FindAsync(id);

            AgentModel? agent = await dbContext.Agents.FindAsync(mission.AgentID);
            var agentInMission = dbContext.Missions.Where(x => x.AgentID == agent.Id && x.StatusMission == StatusMission.assignToAMission).ToList();
            if (!agentInMission.IsNullOrEmpty()) { throw new Exception($"The agent on the id {id} is already in action"); }
            var tergetInmission = dbContext.Missions.Where(x => x.TargetId == mission.TargetId && x.StatusMission == StatusMission.assignToAMission).ToList();
            if (!tergetInmission.IsNullOrEmpty()) { throw new Exception($"The target on the id {id} is already in action"); }

            if (agent == null || mission == null) { throw new Exception($"not find by id {id}"); }
            agent.StatusAgent = StatusAgent.IsActive;
            mission.StatusMission = StatusMission.assignToAMission;
            await dbContext.SaveChangesAsync();
            return mission;


        }

        public async Task<List<MissionDto>> GetAllAsync()
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            var a = await dbContext.Missions.Include(x => x.Agent).Include(x => x.Target).ToListAsync();
            List<MissionDto> missions = new List<MissionDto>();
            foreach (MissionModel mission in a)
            {
                missions.Add(new MissionDto()
                {
                    ImageA = mission.Agent.Image,
                    NickName = mission.Agent.NickName,
                    xA = mission.Agent.x,
                    yA = mission.Agent.y,
                    StatusAgent = mission.Agent.StatusAgent,
                    AgentID = mission.AgentID,
                    TargetId = mission.TargetId,
                    TimeRemaining = mission.TimeRemaining,
                    ActualExecutionTime = mission.ActualExecutionTime,
                    StatusMission = mission.StatusMission,
                    ImageT = mission.Target.Image,
                    Name = mission.Target.Name,
                    position = mission.Target.position,
                    xT = mission.Target.x,
                    yT = mission.Target.y,
                    StatusTarget = mission.Target.StatusTarget,
                });
            }
            return missions;
        }
    }
}
