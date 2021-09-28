using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IHallService
    {
        Task<HallDto> GetHallByIdAsync(int id);
        Task DeleteHallAsync(int id);
        Task UpdateHallAsync(int id, HallDto hallDto);
        Task<int> AddHallAsync(int cinemaId, HallDto hallDto);
    }
}