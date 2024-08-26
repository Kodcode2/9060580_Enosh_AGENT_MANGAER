using ClientAgretTarget.ViewModel;

namespace ClientAgretTarget.Services
{
    public interface ITargetService
    {
        Task<List<TargetVM>> GetAll();
        Task<TargetVM> Details(int id);
    }
}
