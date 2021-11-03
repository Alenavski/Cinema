using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("seats")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSeats([FromBody] SeatDto[] seatDtos)
        {
            await _seatService.DeleteSeatsAsync(seatDtos);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSeats([FromBody] SeatDto[] seatDtos)
        {
            await _seatService.UpdateSeatsAsync(seatDtos);
            return Ok();
        }

        [HttpGet("showtime/{showtimeId:long}")]
        public async Task<IActionResult> GetBlockedSeatsOfShowtime(long showtimeId)
        {
            return Ok(await _seatService.GetBlockedSeatOfShowtimeAsync(showtimeId));
        }
    }
}