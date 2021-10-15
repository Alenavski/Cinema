using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetServicesAsync();
        Task<ServiceDto> GetServiceAsync(int id);
        Task<IEnumerable<ServiceDto>> GetServicesOfHallAsync(int hallId);
        Task<int> AddServiceAsync(ServiceDto serviceDto);
        Task DeleteServiceAsync(int id);
        Task AddServiceToHall(int hallId, int serviceId);
    }
}