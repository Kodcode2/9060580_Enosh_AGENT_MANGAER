using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Service
{
    public interface ITargetService
    {
        Task<List<TargetModel>> GetAllTargetAsync();
        Task<TargetModel> CreateNewTargetAsync(TargetDto targetDto);
        Task<TargetModel> UpdateLocationTargetAsync(LocationDto locationDto, int id);
        Task<TargetModel> moveTargetAsync(DirectionDto directionDto, int id);
    }
}
