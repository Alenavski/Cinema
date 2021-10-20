using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IShowtimeService
    {
        Task<long> AddShowtimeAsync(int movieId, ShowtimeDto showtimeDto);
        Task DeleteShowtime(long id);
        Task<ShowtimeDto> GetShowtimeAsync(long id);
    }
}