using ClientAgretTarget.ViewModel;
using System.Text.Json;

namespace ClientAgretTarget.Services
{
    public class TargetService(IHttpClientFactory clientFactory) : ITargetService
    {
        private readonly string _baseUrl = "https://localhost:7299";

        public async Task<TargetVM> Details(int id)
        {
            List<TargetVM> targets = await GetAll();
            var target = targets.Find(x => x.Id == id);
            return target;

        }

        public async Task<List<TargetVM>> GetAll()
        {

            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/targets");
            var respounce = await httpClient.SendAsync(request);
            if (!respounce.IsSuccessStatusCode)
            {
                throw new Exception("uhvuy");
            }
            var content = await respounce.Content.ReadAsStringAsync();
            List<TargetVM>? targets = JsonSerializer.Deserialize<List<TargetVM>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return targets;

        }
    }
}
