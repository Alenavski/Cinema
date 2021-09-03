using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<int> AddCinema(CinemaDto cinemaDto);
        Task<CinemaDto> GetCinemaById(int id);
    }
}