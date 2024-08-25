using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface IMissionService
    {
        void CreateMissionByAgentAsync(AgentModel agentModel);
        void CreateMissionByTargetAsync(TargetModel targetModel);
        void IfMissionIsRrelevantAsync();
        Task<MissionModel> assignToAMissionAsync(int id);
        Task AgentsPursuitAsync();
        void ChangeAgentPosition(AgentModel agent, TargetModel target);


    }
}
