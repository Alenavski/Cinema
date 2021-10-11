using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("cinemas/{cinemaId:int}/halls/{hallId:int}/seats")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSeats([FromBody] SeatDto[] seatDtos)
        {
            await _seatService.DeleteSeatsAsync(seatDtos);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSeats([FromBody] SeatDto[] seatDtos)
        {
            await _seatService.UpdateSeatsAsync(seatDtos);
            return Ok();
        }
    }
}