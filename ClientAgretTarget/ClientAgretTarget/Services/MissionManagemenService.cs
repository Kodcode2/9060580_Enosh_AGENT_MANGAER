using ClientAgretTarget.Models;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace ClientAgretTarget.Services
{
    public class MissionManagemenService(IHttpClientFactory clientFactory,IAgentService agentService,ITargetService targetService,IMissionService missionService) : IMissionManagemenService
    {

        private readonly string _baseUrl = "https://localhost:7299";

        public async Task<MissionManagementVM> Details(int id)
        {
            var Missions = await GetAll();
            var mission = Missions.Find(x => x.ID == id);
            return mission;

        }

        public async Task<List<MissionManagementVM>> GetAll()
        {
            var httpClient = clientFactory.CreateClient();
            var requestMissions = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/Missions/getAll");
            var respounceMissions = await httpClient.SendAsync(requestMissions);
            if (!respounceMissions.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var contentMissions = await respounceMissions.Content.ReadAsStringAsync();
            List<MissionManagementVM>? Missions = JsonSerializer.Deserialize<List<MissionManagementVM>>(contentMissions, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Missions;

        }
        // חישוב של הפרטים 
        public async Task<GeneralVM> GetGeneral()
        {
            var agents = await agentService.GetAll();
            var targets = await targetService.GetAll();
            var missions = await missionService.GetAll();
            
            var agentsNotActive = agents.Where(x => x.StatusAgent == StatusAgent.IsNnotActive).ToList();
            // רשימה של סוכנים שכשירים לפעולה
            List<Object> a  = [];
            foreach (var agent in agentsNotActive) 
            {
                var b = missions.Where(x => x.StatusMission == StatusMission.offer).Where(x => x.AgentID == agent.Id);
                a.Add(b);
            }
            
             GeneralVM general = new() 
             { 
             SumAgents= agents.Count,
             SumTargets= targets.Count,
             SumMissions = missions.Count,
             AttitudeOfAgentsToGoals = agents.Count / targets.Count,
             AgentRatioToGoalsThatAllowedStaff =  (double)(a.Count / missions.Count),
             };
            return general;
        }

        
    }
}

