using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
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

        public IEnumerable<ShowtimeDto> GetShowtimesByFilter(ShowtimeFilterDto filter)
        {
            filter.EndTime ??= new DateTime(0, 0, 0, 23, 59, 59);
            var showtimes = _context.Showtimes
                .Include(s =>s.Hall)
                .ThenInclude(h => h.Cinema)
                .Where(s => s.Hall.Cinema.City == filter.City)
                .Include(s => s.Movie)
                .Where(s => DateTime.Compare(s.Movie.StartDate, filter.Date) <= 0 && DateTime.Compare(s.Movie.EndDate, filter.Date) >= 0)
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

            var showtimeDtos = new List<ShowtimeDto>();
            foreach (var showtime in showtimes)
            {
                showtimeDtos.Add(
                    new ShowtimeDto(
                        showtime.Id,
                        showtime.Time,
                        showtime.NumberOfFreeSeats,
                        new MovieDto(
                            showtime.Movie.Id,
                            showtime.Movie.Title,
                            showtime.Movie.Description,
                            showtime.Movie.StartDate,
                            showtime.Movie.EndDate,
                            showtime.Movie.Poster
                        ),
                        new HallDto(
                            showtime.Hall.Id,
                            showtime.Hall.Name,
                            new CinemaDto(
                                showtime.Hall.Cinema.Id,
                                showtime.Hall.Cinema.Name,
                                showtime.Hall.Cinema.City,
                                showtime.Hall.Cinema.Address,
                                showtime.Hall.Cinema.Image
                            )
                        )
                    )
                );
            }

            return showtimeDtos;
        }
    }
}