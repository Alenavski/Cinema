using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface ISeatService
    {
        public Task DeleteSeatsAsync(IEnumerable<SeatDto> seatDtos);
        public Task UpdateSeatsAsync(IEnumerable<SeatDto> seatDtos);
    }
}