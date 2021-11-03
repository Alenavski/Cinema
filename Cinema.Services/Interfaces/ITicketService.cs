using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ITicketService
    {
        Task<long> AddTicketAsync(int userId, TicketDto ticketDto);
        Task AddSeatForTicketAsync(long ticketId, long seatId, bool isOrdered);
        Task AddAdditionsForTicketAsync(TicketDto ticketDto);
        Task DeleteSeatTicketAsync(long seatId, long ticketId);
        Task DeleteEmptyTicketAsync(long ticketId);
        Task UpdateDateOfBookingAsync(TicketDto ticketDto);
    }
}