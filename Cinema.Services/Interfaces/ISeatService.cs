using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ISeatService
    {
        Task DeleteSeatsAsync(IEnumerable<SeatDto> seatDtos);
        Task UpdateSeatsAsync(IEnumerable<SeatDto> seatDtos);
        Task<IEnumerable<SeatDto>> GetBlockedSeatOfShowtimeAsync(long showtimeId);
    }
}