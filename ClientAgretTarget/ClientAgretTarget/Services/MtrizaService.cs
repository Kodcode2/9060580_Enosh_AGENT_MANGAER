using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public class MtrizaService(IHttpClientFactory clientFactory,IAgentService agentService,ITargetService targetService) : IMtrizaService
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<(List<AgentVM>,List<TargetVM>)> GetAllAgentAndTarget()
        {
            var agent = await agentService.GetAll();
            var targets = await targetService.GetAll();
            return (agent, targets);
        }

    }
}
