using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface IMissionManagemenService
    {
        Task<List<MissionManagementVM>> GetAll();
        Task<MissionManagementVM> Details(int id);
        Task<GeneralVM> GetGeneral();
    }
}
