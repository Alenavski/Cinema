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
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _context;

        public TicketService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketDto>> GetTickets(int userId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.IsOrdered)
                .Include(t => t.User)
                .Include(t => t.ShowtimeDate)
                .ThenInclude(shDate => shDate.Showtime)
                .ThenInclude(sh => sh.Prices)
                .Include(t => t.TicketsAdditions)
                .ThenInclude(ta => ta.Addition)
                .Include(t => t.TicketsSeats)
                .ThenInclude(ts => ts.Seat)
                .ThenInclude(seat => seat.SeatType)
                .Where(t => t.User.Id == userId)
                .ProjectToType<TicketDto>()
                .ToListAsync();

            return tickets;
        }

        public async Task<IEnumerable<TicketMovieDto>> GetTicketMovies(int userId)
        {
            var ticketMovies = await _context.Tickets
                .Where(t => t.IsOrdered)
                .Include(t => t.User)
                .Include(t => t.ShowtimeDate)
                .ThenInclude(sd => sd.Showtime)
                .ThenInclude(s => s.Movie)
                .Where(t => t.User.Id == userId)
                .Select(t => new
                {
                    Movie = t.ShowtimeDate.Showtime.Movie,
                    Ticket = t
                })
                .ToListAsync();

            return ticketMovies.Adapt<TicketMovieDto[]>();
        }

        public async Task<long> AddTicketAsync(int userId, TicketDto ticketDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            var showtimeDate = await _context.ShowtimesDates
                .SingleOrDefaultAsync(sh => sh.ShowtimeId == ticketDto.Showtime.Id && sh.Date == ticketDto.DateOfShowtime);

            var ticket = new TicketEntity
            {
                Id = 0,
                ShowtimeDate = showtimeDate,
                DateOfBooking = ticketDto.DateOfBooking,
                User = user,
                IsOrdered = false
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket.Id;
        }

        public async Task ApplyTicketAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            ticket.DateOfBooking = ticketDto.DateOfBooking;
            ticket.IsOrdered = true;
            await _context.SaveChangesAsync();
        }

        public async Task AddAdditionsForTicketAsync(TicketDto ticketDto)
        {
            var ticket = await _context.Tickets
                .SingleOrDefaultAsync(t => t.Id == ticketDto.Id);
            var showtimeAdditions = await _context.ShowtimesAdditions
                .Where(sha => sha.ShowtimeId == ticketDto.Showtime.Id)
                .ToListAsync();

            foreach (var additionDto in ticketDto.TicketsAdditions)
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
                        Seat = seat,
                        BlockingTime = DateTime.Now
                    }
                );
            }
            else
            {
                ticketSeat.IsOrdered = true;
            }


            await _context.SaveChangesAsync();
        }


        public async Task DeleteSeatTicketAsync(long seatId, long ticketId)
        {
            var ticketSeat = await _context.TicketsSeats
                .Include(ts => ts.Ticket)
                .SingleOrDefaultAsync(ts => ts.SeatId == seatId && ts.TicketId == ticketId);

            if (ticketSeat is { IsOrdered: false })
            {
                _context.Remove(ticketSeat);
                await _context.SaveChangesAsync();
            }
        }
    }
}