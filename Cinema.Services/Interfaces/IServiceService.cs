using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetServicesAsync();
        Task<ServiceDto> GetServiceByIdAsync(int id);
        Task<int> AddServiceAsync(ServiceDto serviceDto);
        Task DeleteServiceAsync(int id);
    }
}