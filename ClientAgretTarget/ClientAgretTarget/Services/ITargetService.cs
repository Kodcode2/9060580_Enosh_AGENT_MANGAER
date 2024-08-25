using ClientAgretTarget.Models;

namespace ClientAgretTarget.Services
{
    public interface ITargetService
    {
        Task<List<TargetModel>> GetAllTargets();
    }
}
