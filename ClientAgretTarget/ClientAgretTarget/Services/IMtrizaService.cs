using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface IMtrizaService
    {
        Task<(List<AgentVM>, List<TargetVM>)> GetAllAgentAndTarget();
    }
}
