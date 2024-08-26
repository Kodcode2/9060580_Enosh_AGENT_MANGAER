using ClientAgretTarget.ViewModel;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class MissionService(IHttpClientFactory clientFactory) : IMissionService
    {
        private readonly string _baseUrl = "https://localhost:7299";

        public async Task<MissionVM> Details(int id)
        {
            List<MissionVM> Missions = await GetAll();
            var Mission = Missions.Find(x => x.Id == id);
            return Mission;

        }

        public async Task<List<MissionVM>> GetAll()
        {

            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/Missions");
            var respounce = await httpClient.SendAsync(request);
            if (!respounce.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var content = await respounce.Content.ReadAsStringAsync();
            List<MissionVM>? Missions = JsonSerializer.Deserialize<List<MissionVM>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Missions;

        }
        public async Task RunAMission(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/Missions/{id}");

            var result = await httpClient.SendAsync(request);
        }
    }
}
