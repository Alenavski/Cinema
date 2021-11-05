using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ITicketService
    {
        Task<long> AddTicketAsync(int userId, TicketDto ticketDto);
        Task AddSeatForTicketAsync(long ticketId, long seatId, bool isOrdered);
        Task AddAdditionsForTicketAsync(TicketDto ticketDto);
        Task UpdateDateOfBookingAsync(TicketDto ticketDto);
        Task DeleteSeatTicketAsync(long seatId, long ticketId);
        Task<IEnumerable<TicketDto>> GetTickets(int userId);
        Task<IEnumerable<TicketMovieDto>> GetTicketMovies(int userId);
    }
}