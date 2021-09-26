using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IHallService
    {
        Task<HallDto> GetHallById(int id);
        Task DeleteHall(int id);
        Task UpdateHall(int id, HallDto hallDto);
        Task<int> AddHall(int cinemaId, HallDto hallDto);
    }
}