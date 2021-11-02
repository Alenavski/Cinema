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

        public async Task AddTicketAsync(UserDto userDto, TicketDto ticketDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userDto.Id);
            var showtime = await _context.Showtimes
                .SingleOrDefaultAsync(sh => sh.Id == ticketDto.Showtime.Id);
            var ticket = new TicketEntity()
            {
                Showtime = showtime,
                DateOfBooking = ticketDto.DateOfBooking,
                User = user
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            ticketDto.Id = ticket.Id;

            await AddAdditionsForTicketAsync(ticketDto);
            await AddSeatsForTicketAsync(ticketDto);
        }

        private async Task AddAdditionsForTicketAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            var showtimeAdditions = await _context.ShowtimesAdditions
                .Where(sha => sha.ShowtimeId == ticket.Showtime.Id)
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

        private async Task AddSeatsForTicketAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            var seats = await _context.Seats
                .Where(s => s.Hall.Id == ticket.Showtime.Hall.Id)
                .ToListAsync();

            foreach (var ticketSeat in ticketDto.TicketSeats)
            {
                var seat = seats.SingleOrDefault(s => s.Id == ticketSeat.Seat.Id);
                if (seat != null)
                {
                    await _context.TicketsSeats.AddAsync(
                        new TicketSeatEntity
                        {
                            IsOrdered = true,
                            SeatId = seat.Id,
                            TicketId = ticketDto.Id
                        }
                    );
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}