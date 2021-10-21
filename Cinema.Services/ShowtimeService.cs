using System;
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
    public class ShowtimeService : IShowtimeService
    {
        private readonly ApplicationContext _context;

        public ShowtimeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CanAddShowtime(int movieId, ShowtimeDto showtimeDto)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieId);
            var showtimes = await _context.Showtimes
                .Include(sh => sh.Hall)
                .Where(sh => sh.Hall.Id == showtimeDto.Hall.Id)
                .ToListAsync();

            foreach (var showtime in showtimes)
            {
                if (Math.Abs(showtime.Time.TotalMinutes - showtimeDto.Time.TotalMinutes) <= movie.MinutesLength)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<long> AddShowtimeAsync(int movieId, ShowtimeDto showtimeDto)
        {
            var hall = await _context.Halls
                .Include(h => h.Seats)
                .SingleOrDefaultAsync(h => h.Id == showtimeDto.Hall.Id);
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieId);

            var showtime = new ShowtimeEntity
            {
                Hall = hall,
                Id = 0,
                Movie = movie,
                NumberOfFreeSeats = (short)hall.Seats.Count,
                Time = showtimeDto.Time
            };
            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();
            return showtime.Id;
        }

        public async Task DeleteShowtime(long id)
        {
            var showtime = await _context.Showtimes.SingleOrDefaultAsync(sh => sh.Id == id);
            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();
        }

        public async Task<ShowtimeDto> GetShowtimeAsync(long id)
        {
            return await _context.Showtimes
                .Where(sh => sh.Id == id)
                .ProjectToType<ShowtimeDto>()
                .SingleOrDefaultAsync();
        }
    }
}