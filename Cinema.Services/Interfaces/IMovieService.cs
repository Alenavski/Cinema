using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<MovieDto> GetMoviesByFilter(ShowtimeFilterDto filter);
        Task<int> AddMovieAsync(MovieDto movie);
        Task<MovieDto> GetMovieByIdAsync(int id);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieAsync(MovieDto movieDto);
        Task<IEnumerable<MovieDto>> GetMoviesByTermAsync(string term);
    }
}