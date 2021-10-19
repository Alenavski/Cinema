using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IHallAdditionService
    {
        Task<IEnumerable<HallAdditionDto>> GetHallAdditionsAsync(int hallId);
        Task AddAdditionToHallAsync(int hallId, int additionId, decimal price);
        Task DeleteAdditionFromHallAsync(int hallId, int additionId);
    }
}