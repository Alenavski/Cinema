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

        public async Task<IEnumerable<SeatDto>> GetBlockedSeatOfShowtimeAsync(long showtimeId, DateTime dateOfShowtime)
        {
            var seats = await _context.Tickets
                .Where(ticket => ticket.ShowtimeDate.ShowtimeId == showtimeId && ticket.ShowtimeDate.Date == dateOfShowtime)
                .SelectMany(ticket => ticket.TicketsSeats)
                .Where(ts => ts.IsOrdered || (DateTime.Now - ts.BlockingTime).TotalMinutes < 15)
                .Select(ts => ts.Seat)
                .ProjectToType<SeatDto>()
                .ToListAsync();
            return seats;
        }
    }
}