using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface IMissionService
    {
        Task<List<MissionVM>> GetAll();
        Task<MissionVM> Details(int id);
        Task RunAMission(int id);
    }
}
