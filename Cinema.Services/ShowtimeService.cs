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
    public class ShowtimeService : IShowtimeService
    {
        private readonly ApplicationContext _context;

        public ShowtimeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CinemaDto>> GetCinemasByMovieIdAsync(int movieId)
        {
            return await _context.Cinemas
                .Include(c => c.Halls.Where(h => h.Showtimes.Any(sh => sh.MovieId == movieId)))
                .ThenInclude(h => h.Showtimes.Where(sh => sh.MovieId == movieId))
                .Where(c => c.Halls.Any(h => h.Showtimes.Any(sh => sh.MovieId == movieId)))
                .ProjectToType<CinemaDto>()
                .ToListAsync();
        }

        public async Task<bool> CanAddShowtimeAsync(int movieId, ShowtimeDto showtimeDto)
        {
            var movieShowtime = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieId);
            var movies = await _context.Movies.ToListAsync();
            foreach (var movie in movies.Where(movie => DateTime.Compare(movie.EndDate, DateTime.Now) >= 0))
            {
                var showtimes = await _context.Showtimes
                    .Include(sh => sh.Hall)
                    .Include(sh => sh.Movie)
                    .Where(sh => sh.Movie.Id == movie.Id)
                    .ToListAsync();
                
                foreach (var showtime in showtimes.Where(showtime => showtime.Hall.Id == showtimeDto.Hall.Id))
                {
                    if (showtime.Time.TotalMinutes - showtimeDto.Time.TotalMinutes > 0)
                    {
                        if (showtime.Time.TotalMinutes - showtimeDto.Time.TotalMinutes < movieShowtime.MinutesLength)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (showtimeDto.Time.TotalMinutes - showtime.Time.TotalMinutes < movie.MinutesLength)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task AddShowtimeAsync(int movieId, ShowtimeDto showtimeDto)
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
                Time = showtimeDto.Time
            };
            await _context.Showtimes.AddAsync(showtime);
            await _context.SaveChangesAsync();

            showtimeDto.Id = showtime.Id;

            for (int i = 0; i < (movie.EndDate - movie.StartDate).Days; i++)
            {
                var showtimeDate = new ShowtimeDateEntity()
                {
                    ShowtimeId = showtime.Id,
                    Date = movie.StartDate + new TimeSpan(1,0,0,0)
                };

                await _context.ShowtimesDates.AddAsync(showtimeDate);
                await _context.SaveChangesAsync();
            }

            await AddPricesForSeatTypesAsync(showtimeDto);
            await AddAdditionsForShowtimeAsync(showtimeDto);
        }

        private async Task AddAdditionsForShowtimeAsync(ShowtimeDto showtimeDto)
        {
            var showtime = await _context.Showtimes.SingleOrDefaultAsync(sh => sh.Id == showtimeDto.Id);
            var hallAdditions = await _context.HallsAdditions
                .Where(ha => ha.HallId == showtime.Hall.Id)
                .ToListAsync();
            foreach (var additionDto in showtimeDto.Additions)
            {
                var hallAddition = hallAdditions
                    .SingleOrDefault(ha => ha.AdditionId == additionDto.Id);
                if (hallAddition != null)
                {
                    await _context.ShowtimesAdditions.AddAsync(
                        new ShowtimeAdditionEntity
                        {
                            AdditionId = additionDto.Id,
                            ShowtimeId = showtime.Id
                        }
                    );
                }
            }

            await _context.SaveChangesAsync();
        }
        
        private async Task AddPricesForSeatTypesAsync(ShowtimeDto showtimeDto)
        {
            var showtime = await _context.Showtimes.SingleOrDefaultAsync(sh => sh.Id == showtimeDto.Id);
            var seatTypes = await _context.SeatTypes.ToListAsync();
            foreach (var price in showtimeDto.Prices)
            {
                var seatType = seatTypes.SingleOrDefault(st => st.Id == price.SeatType.Id);
                if (seatType != null)
                {
                    await _context.TicketsPrices.AddAsync(
                        new TicketPriceEntity
                        {
                            Price = price.Price,
                            SeatType = seatType,
                            Showtime = showtime
                        }
                    );
                }
            }
            
            await _context.SaveChangesAsync();
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