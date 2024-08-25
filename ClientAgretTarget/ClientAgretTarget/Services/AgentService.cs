using ClientAgretTarget.Models;
using ClientAgretTarget.ViewModel;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class AgentService(IHttpClientFactory clientFactory) : IAgentService
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<List<AgentModel>> GetAllAgents()
        {
            var httpClient = clientFactory.CreateClient();
            var requestAgents = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/agents");
            var respounceAgents = await httpClient.SendAsync(requestAgents);
            if (!respounceAgents.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var contentAgents = await respounceAgents.Content.ReadAsStringAsync();
            List<AgentModel>? agents = JsonSerializer.Deserialize<List<AgentModel>>(contentAgents, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return agents;

        }
    }
}
