using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface IMissionService
    {
        void CreateMissionByAgentAsync(AgentModel agentModel);
        void CreateMissionByTargetAsync(TargetModel targetModel);
        void IfMissionIsRrelevantAsync();
        
        Task AgentsPursuitAsync();
       
        Task<List<MissionModel>> GetAllMissionAsync();
        Task<MissionModel> CommandmentToMissionAsync(int id);


    }
}
