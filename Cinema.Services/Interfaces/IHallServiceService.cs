using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IHallServiceService
    {
        Task<IEnumerable<HallServiceDto>> GetServicesOfHallAsync(int hallId);
        Task AddServiceToHallAsync(int hallId, int serviceId, decimal price);
        Task DeleteServiceFromHallAsync(int hallId, int serviceId);
    }
}