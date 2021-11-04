using System.Linq;
using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _context;

        public TicketService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<long> AddTicketAsync(int userId, TicketDto ticketDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);
            var showtime = await _context.Showtimes
                .SingleOrDefaultAsync(sh => sh.Id == ticketDto.Showtime.Id);
            var ticket = new TicketEntity
            {
                Id = 0,
                Showtime = showtime,
                DateOfBooking = ticketDto.DateOfBooking,
                DateOfShowtime = ticketDto.DateOfShowtime,
                User = user
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket.Id;
        }

        public async Task UpdateDateOfBookingAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            ticket.DateOfBooking = ticketDto.DateOfBooking;
            await _context.SaveChangesAsync();
        }

        public async Task AddAdditionsForTicketAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            var showtimeAdditions = await _context.ShowtimesAdditions
                .Where(sha => sha.ShowtimeId == ticketDto.Showtime.Id)
                .ToListAsync();

            foreach (var additionDto in ticketDto.Additions)
            {
                var showtimeAddition = showtimeAdditions
                    .SingleOrDefault(sha => sha.AdditionId == additionDto.Id);
                if (showtimeAddition != null)
                {
                    await _context.TicketsAdditions.AddAsync(
                        new TicketAdditionEntity
                        {
                            AdditionId = additionDto.Id,
                            TicketId = ticket.Id
                        }
                    );
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddSeatForTicketAsync(long ticketId, long seatId, bool isOrdered)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Showtime)
                .SingleOrDefaultAsync(t => t.Id == ticketId);
            var seat = await _context.Seats
                .SingleOrDefaultAsync(s => s.Id == seatId);
            var ticketSeat = await _context.TicketsSeats
                .SingleOrDefaultAsync(ts => ts.SeatId == seatId && ts.TicketId == ticketId);
            if (ticketSeat == null)
            {
                await _context.TicketsSeats.AddAsync(
                    new TicketSeatEntity
                    {
                        IsOrdered = isOrdered,
                        Ticket = ticket,
                        Seat = seat
                    }
                );
            }
            else
            {
                ticketSeat.IsOrdered = true;
            }

            var showtime = await _context.Showtimes
                .SingleOrDefaultAsync(s => s.Id == ticket.Showtime.Id);
            showtime.NumberOfFreeSeats -= 1;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeatTicketAsync(long seatId, long ticketId)
        {
            var ticketSeat = await _context.TicketsSeats
                .Include(ts => ts.Ticket)
                .ThenInclude(t => t.Showtime)
                .SingleOrDefaultAsync(ts => ts.SeatId == seatId && ts.TicketId == ticketId);
            if (ticketSeat is { IsOrdered: false })
            {
                var showtime = await _context.Showtimes
                    .SingleOrDefaultAsync(sh => sh.Id == ticketSeat.Ticket.Showtime.Id);
                showtime.NumberOfFreeSeats += 1;
                _context.Remove(ticketSeat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmptyTicketAsync(long ticketId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.TicketsSeats)
                .SingleOrDefaultAsync(t => t.Id == ticketId);
            if (ticket != null && ticket.TicketsSeats.Count == 0)
            {
                _context.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}