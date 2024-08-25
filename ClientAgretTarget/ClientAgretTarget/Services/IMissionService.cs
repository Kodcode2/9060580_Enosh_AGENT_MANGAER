using ClientAgretTarget.Models;

namespace ClientAgretTarget.Services
{
    public interface IMissionService
    {
        Task<List<MissionModel>> GetAllMissions();
    }
}
