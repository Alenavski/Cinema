using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private ApplicationContext _context;

        public ShowtimeService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<MovieDto> GetMoviesByFilter(ShowtimeFilterDto filter)
        {
            filter.EndTime ??= new TimeSpan(23, 59, 59);
            var showtimes = _context.Showtimes
                .Include(s =>s.Hall)
                .ThenInclude(h => h.Cinema)
                .Include(s => s.Movie)
                .Where(s => s.Hall.Cinema.City == filter.City)
                .Where(s =>
                    DateTime.Compare(s.Movie.StartDate, filter.Date) <= 0
                    && DateTime.Compare(s.Movie.EndDate, filter.Date) >= 0)
                .Where(s => s.Time >= filter.StartTime && s.Time <= filter.EndTime)
                .Where(s => s.NumberOfFreeSeats >= filter.NumberOfFreeSeats);

            if (filter.MovieTitle != null)
            {
                showtimes = showtimes
                    .Where(s => s.Movie.Title == filter.MovieTitle);
            }

            if (filter.CinemaName != null)
            {
                showtimes = showtimes
                    .Where(s => s.Hall.Cinema.Name == filter.CinemaName);
            }

            var movies = showtimes.AsEnumerable().GroupBy(s => s.Movie);
            var movieEntities = new List<MovieEntity>();
            foreach (var movie in movies)
            {
                movie.Key.Showtimes.Clear();
                movie.Key.Showtimes = movie.ToList();
                movieEntities.Add(movie.Key);
            }

            return movieEntities.Adapt<MovieDto[]>();
        }
    }
}