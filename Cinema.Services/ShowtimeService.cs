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

        public IEnumerable<CinemaDto> GetCinemasByMovieId(int movieId)
        {
            var groupCinemasShowtimes = _context.Showtimes
                .Include(sh => sh.Movie)
                .Include(sh => sh.Hall)
                .ThenInclude(h => h.Cinema)
                .Where(sh => sh.Movie.Id == movieId)
                .AsEnumerable()
                .GroupBy(sh => sh.Hall.Cinema);

            var cinemasList = new List<CinemaDto>();
            foreach (var cinemaShowtimes in groupCinemasShowtimes)
            {
                var cinemaDto = cinemaShowtimes.Key.Adapt<CinemaDto>();

                var halls = cinemaDto.Halls.GroupJoin(
                    cinemaShowtimes,
                    hall => hall.Id,
                    showtime => showtime.Hall.Id,
                    (hall, showtime) => hall
                ).ToList();

                cinemaDto.Halls = halls;
                cinemasList.Add(cinemaDto);
            }
            
            return cinemasList;
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
                NumberOfFreeSeats = (short)hall.Seats.Count,
                Time = showtimeDto.Time
            };
            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();
            showtimeDto.Id = showtime.Id;

            await AddPricesForSeatTypesAsync(showtimeDto);
            await AddAdditionsForShowtimeAsync(showtimeDto);
        }

        private async Task AddAdditionsForShowtimeAsync(ShowtimeDto showtimeDto)
        {
            var showtime = await _context.Showtimes.SingleOrDefaultAsync(sh => sh.Id == showtimeDto.Id);
            /*var additions = _context.Additions
                .Include(a => a.Halls)
                .ThenInclude(ah => ah.Hall);*/
            var hallAdditions = await _context.HallsAdditions
                //.Include(ha => ha.Hall)
                //.Include(ha => ha.Addition)
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