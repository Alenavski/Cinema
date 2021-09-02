using System.Collections.Generic;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IShowtimeService
    {
        public IEnumerable<MovieDto> GetMoviesByFilter(ShowtimeFilterDto filter);
    }
}