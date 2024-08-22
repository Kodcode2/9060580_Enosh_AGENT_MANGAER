using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface IMissionService
    {
        void CreateMissionByAgent(AgentModel agentModel);
        void CreateMissionByTarget(TargetModel targetModel);
    }
}
