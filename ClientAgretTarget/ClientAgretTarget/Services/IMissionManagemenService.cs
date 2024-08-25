using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface IMissionManagemenService
    {
        Task<List<MissionManagementVM>> GetAll();
    }
}
