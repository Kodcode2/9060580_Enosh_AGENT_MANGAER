﻿using AgentsRest.Date;
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
        // הפעל משימה
        public async Task<MissionModel> assignToAMissionAsync(int id)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            MissionModel? missionModel = dbContext.Missions.Find(id);
            if (missionModel == null) { throw new Exception($"not find Mission by id {id}"); }
            missionModel.StatusMission = StatusMission.assignToAMission;
            await dbContext.SaveChangesAsync();
            return missionModel;
        }

        // בדיקה האם יש אופיצה למשימה כשסוכן זז
        public async void CreateMissionByAgentAsync(AgentModel agentModel)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            // יצירת רשימה של מטרות חייים
            var targetlive = dbContext.Targets.Where(x => x.StatusTarget == StatusTarget.Live).ToList();


            foreach (var target in targetlive)
            {
                int xA = agentModel.x;
                int xT = target.x;
                int yA = agentModel.y;
                int yT = target.y;

                // שולח לבדיקה מה המרחק בין המטרה לסוכן
                var distance = DistanceCalculation(xA, yA, xT, yT);

                // מביא את כל המשימות ש המטרה נמצאת בהם
                var targetInMission = dbContext.Missions.Where(x => x.TargetId == target.Id).ToList();

                // ואז מסנן ומביא רשימה רק עם המשימות הפעילות 
                targetInMission = targetInMission.Where(x => x.StatusMission == StatusMission.assignToAMission).ToList();

                // פה אני בודק האם המטרה נמצאת ברדיוס שאפשר להציע משימה לסוכן ו שהרשימה רייקה שזה אומר שאין משימות פעילות על המטרה שלי
                if (distance <= 200 && targetInMission.IsNullOrEmpty())
                {
                    MissionModel newModel = new()
                    {
                        AgentID = agentModel.Id,
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
        public async void CreateMissionByTargetAsync(TargetModel targetModel)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();
            // יצירת רשימה של סוכנים רדומים
            var agentsNotActive = dbContext.Agents.Where(x => x.StatusAgent == StatusAgent.IsNnotActive).ToList();


            foreach (var agents in agentsNotActive)
            {
                int xT = targetModel.x;
                int xA = agents.x;
                int yT = targetModel.y;
                int yA = agents.y;

                // שולח לבדיקה מה המרחק בין המטרה לסוכן
                var distance = DistanceCalculation(xA, yA, xT, yT);

                // מביא את כל המשימות ש הסוכן נמצאת בהם
                var agentsInMission = dbContext.Missions.Where(x => x.AgentID == agents.Id).ToList();

                // ואז מסנן ומביא רשימה רק עם המשימות הפעילות 
                agentsInMission = agentsInMission.Where(x => x.StatusMission == StatusMission.assignToAMission).ToList();

                // פה אני בודק האם הסוכן נמצאת ברדיוס שאפשר להציע משימה לסוכן ו שהרשימה רייקה שזה אומר שאין משימות פעילות של הסוכן שלי
                if (distance <= 200 && agentsInMission.IsNullOrEmpty())
                {
                    MissionModel newModel = new()
                    {
                        AgentID = agents.Id,
                        TargetId = targetModel.Id,
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


    }
}
