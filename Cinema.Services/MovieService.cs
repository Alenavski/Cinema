using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationContext _context;

        public MovieService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddMovieAsync(MovieDto movie)
        {
            var movieEntity = movie.Adapt<MovieEntity>();
            await _context.Movies.AddAsync(movieEntity);
            await _context.SaveChangesAsync();
            return movieEntity.Id;
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Where(m => m.Id == id)
                .ProjectToType<MovieDto>()
                .SingleOrDefaultAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(MovieDto movieDto)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieDto.Id);

            movie.Description = movieDto.Description;
            movie.Poster = movieDto.Poster ?? movie.Poster;
            movie.Title = movieDto.Title;
            movie.EndDate = movieDto.EndDate;
            movie.StartDate = movieDto.StartDate;
            movie.MinutesLength = movieDto.MinutesLength;

            await _context.SaveChangesAsync();
        }

        public IEnumerable<MovieDto> GetMoviesByFilter(ShowtimeFilterDto filter)
        {
            filter.StartTime ??= new TimeSpan(0, 0, 0);
            filter.EndTime ??= new TimeSpan(23, 59, 59);
            var showtimes = _context.Showtimes
                .Include(s =>s.Hall)
                .ThenInclude(h => h.Cinema)
                .Include(s => s.Movie)
                .Where(s => s.Time >= filter.StartTime && s.Time <= filter.EndTime);

            if (filter.City != null)
            {
                showtimes = showtimes
                    .Where(s => s.Hall.Cinema.City == filter.City);
            }

            if (!filter.Date.Equals(DateTime.MinValue))
            {
                showtimes = showtimes
                    .Where(s =>
                        DateTime.Compare(s.Movie.StartDate, filter.Date) <= 0
                        && DateTime.Compare(s.Movie.EndDate, filter.Date) >= 0
                    );
            }

            if (filter.NumberOfFreeSeats != 0)
            {
                showtimes = showtimes
                    .Where(
                        sh =>
                            sh.Dates
                                .Select(date =>
                                    date.Tickets
                                        .Select(ticket => ticket.TicketsSeats)
                                        .Count()
                                    ).Any(count => sh.Hall.Seats.Count - count >= filter.NumberOfFreeSeats)
                            );
            }

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