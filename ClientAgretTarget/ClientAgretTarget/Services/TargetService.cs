using ClientAgretTarget.Models;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class TargetService(IHttpClientFactory clientFactory) : ITargetService
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<List<TargetModel>> GetAllTargets()
        {
            var httpClient = clientFactory.CreateClient();
            var requestTarget = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/targets");
            var respounceTarget = await httpClient.SendAsync(requestTarget);
            if (!respounceTarget.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var contentTarget = await respounceTarget.Content.ReadAsStringAsync();
            List<TargetModel>? Targets = JsonSerializer.Deserialize<List<TargetModel>>(contentTarget, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Targets;

        }
    }
}
