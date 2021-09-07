using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<int> AddCinema(CinemaDto cinemaDto);
        Task<CinemaDto> GetCinemaById(int id);
        Task<string> UpdateCinema(int id, CinemaDto cinemaDto);
        IEnumerable<CinemaDto> GetCinemas();
    }
}