using ClientAgretTarget.Models;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class MissionService(IHttpClientFactory clientFactory) : IMissionService
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<List<MissionModel>> GetAllMissions()
        {
            var httpClient = clientFactory.CreateClient();
            var requestMissions = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/Missions");
            var respounceMissions = await httpClient.SendAsync(requestMissions);
            if (!respounceMissions.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var contentMissions = await respounceMissions.Content.ReadAsStringAsync();
            List<MissionModel>? Missions = JsonSerializer.Deserialize<List<MissionModel>>(contentMissions, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Missions;

        }
    }
}
