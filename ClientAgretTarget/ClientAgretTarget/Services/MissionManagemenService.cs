using ClientAgretTarget.Models;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static ClientAgretTarget.Utels.Calculations;

namespace ClientAgretTarget.Services
{
    public class MissionManagemenService(IHttpClientFactory clientFactory) : IMissionManagemenService
    {

        private readonly string _baseUrl = "https://localhost:7299";
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
    }
}

