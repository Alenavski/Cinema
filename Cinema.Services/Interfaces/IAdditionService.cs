using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IAdditionService
    {
        Task<IEnumerable<AdditionDto>> GetAdditionsAsync();
        Task<AdditionDto> GetAdditionByIdAsync(int id);
        Task<int> AddAdditionAsync(AdditionDto additionDto);
    }
}