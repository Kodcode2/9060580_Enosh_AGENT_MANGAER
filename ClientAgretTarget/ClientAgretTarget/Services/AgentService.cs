using ClientAgretTarget.ViewModel;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class AgentService(IHttpClientFactory clientFactory) : IAgentService
    {
        private readonly string _baseUrl = "https://localhost:7299";

        public async Task<AgentVM> Details(int id)
        {
            List<AgentVM> agents = await GetAll();
            var agent = agents.Find(x => x.Id == id);
            return agent;

        }

        public async Task<List<AgentVM>> GetAll()
        {

            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/agents");
            var respounce = await httpClient.SendAsync(request);
            if (!respounce.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var content = await respounce.Content.ReadAsStringAsync();
            List<AgentVM>? agents = JsonSerializer.Deserialize<List<AgentVM>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return agents;

        }
    }
}
