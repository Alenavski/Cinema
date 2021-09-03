using System.Collections.Generic;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IMovieService
    {
        public IEnumerable<MovieDto> GetMoviesByFilter(ShowtimeFilterDto filter);
    }
}