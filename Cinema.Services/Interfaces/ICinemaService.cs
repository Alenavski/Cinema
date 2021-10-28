using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<int> AddCinemaAsync(CinemaDto cinemaDto);
        Task<CinemaDto> GetCinemaByIdAsync(int id);
        Task UpdateCinemaAsync(int id, CinemaDto cinemaDto);
        Task<IEnumerable<CinemaDto>> GetCinemasAsync(string term);
        Task DeleteCinemaAsync(int id);
        Task<IEnumerable<string>> GetCitiesByTermAsync(string term);
    }
}