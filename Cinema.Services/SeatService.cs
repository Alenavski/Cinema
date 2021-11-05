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
    public class SeatService : ISeatService
    {
        private readonly ApplicationContext _context;

        public SeatService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteSeatsAsync(IEnumerable<SeatDto> seatDtos)
        {
            foreach (var seatDto in seatDtos)
            {
                var seat = await _context.Seats
                    .SingleOrDefaultAsync(s => s.Id == seatDto.Id);
                _context.Seats.Remove(seat);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSeatsAsync(IEnumerable<SeatDto> seatDtos)
        {
            foreach (var seatDto in seatDtos)
            {
                var seat = await _context.Seats
                    .Include(s => s.SeatType)
                    .SingleOrDefaultAsync(s => s.Id == seatDto.Id);
                seat.Place = seatDto.Place;
                seat.SeatType = await _context.SeatTypes
                    .SingleOrDefaultAsync(st => st.Id == seatDto.SeatType.Id);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SeatDto>> GetBlockedSeatOfShowtimeAsync(long ticketId)
        {
            var ticket = await _context.Tickets.Include(t => t.ShowtimeDate)
                .SingleOrDefaultAsync(t => t.Id == ticketId);

            var currentTime = DateTime.Now.Subtract(new TimeSpan(0, 15, 0));

            var seats = await _context.Tickets
                .Include(t => t.ShowtimeDate)
                .Include(t => t.TicketsSeats)
                .ThenInclude(ts => ts.Seat)
                .ThenInclude(seat => seat.SeatType)
                .Where(t => t.ShowtimeDate.ShowtimeId == ticket.ShowtimeDate.ShowtimeId
                            && t.ShowtimeDate.Date == ticket.ShowtimeDate.Date)
                .SelectMany(t => t.TicketsSeats)
                .Where(ts => ts.IsOrdered || currentTime < ts.BlockingTime)
                .Select(ts => ts.Seat)
                .ProjectToType<SeatDto>()
                .ToListAsync();
            return seats;
        }
    }
}