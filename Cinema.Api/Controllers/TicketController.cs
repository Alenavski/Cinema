using System.Security.Claims;
using System.Threading.Tasks;
using Cinema.Api.Tools.Interfaces;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IBackgroundTaskService _backgroundTaskService;

        public TicketController(ITicketService ticketService, IBackgroundTaskService backgroundTaskService)
        {
            _ticketService = ticketService;
            _backgroundTaskService = backgroundTaskService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddTicket([FromBody] TicketDto ticketDto)
        {
            var userClaim = User.FindFirst(ClaimTypes.Sid);
            if (userClaim == null)
            {
                return Unauthorized();
            }

            var ticketId = await _ticketService.AddTicketAsync(int.Parse(userClaim.Value), ticketDto);
            _backgroundTaskService.DeleteEmptyTicketWithDelay(ticketId, 30);

            return Ok(ticketId);
        }

        [HttpPost("{ticketId:long}/seats/{seatId:long}")] 
        [Authorize(Roles = "User")]
        public async Task<IActionResult> BlockSeat(long seatId, long ticketId)
        {
            await _ticketService.AddSeatForTicketAsync(ticketId, seatId, false);

            _backgroundTaskService.UnblockSeatWithDelay(seatId, ticketId, 15);

            return Ok();
        }

        [HttpDelete("{ticketId:long}/seats/{seatId:long}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnblockSeat(long seatId, long ticketId)
        {
            await _ticketService.DeleteSeatTicketAsync(seatId, ticketId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ApplyTicket([FromBody] TicketDto ticketDto)
        {
            await _ticketService.UpdateDateOfBookingAsync(ticketDto);

            if (ticketDto.TicketSeats == null)
            {
                return BadRequest(
                    new
                    {
                        message = "Please, choose seat for ticket"
                    }
                );
            }
            foreach (var ticketSeatDto in ticketDto.TicketSeats)
            {
                await _ticketService.AddSeatForTicketAsync(ticketDto.Id, ticketSeatDto.Seat.Id, true);
            }

            if (ticketDto.Additions != null)
            {
                await _ticketService.AddAdditionsForTicketAsync(ticketDto);
            }
            return Ok();
        }
    }
}