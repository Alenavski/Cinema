using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("movies/{movieId:int}/showtimes")]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShowtime(int movieId, ShowtimeDto showtimeDto)
        {
            if (await _showtimeService.CanAddShowtime(movieId, showtimeDto))
            {
                return Ok(await _showtimeService.AddShowtimeAsync(movieId, showtimeDto));
            }
            else
            {
                return BadRequest(
                    new
                    {
                        message = "Can't add showtime at time in this hall"
                    }
                );
            }
        }

        [HttpDelete("{showtimeId:long}")]
        public async Task<IActionResult> DeleteShowtime(long showtimeId)
        {
            var showtime = await _showtimeService.GetShowtimeAsync(showtimeId);

            if (showtime == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such showtime doesn't exist"
                    }
                );
            }

            await _showtimeService.DeleteShowtime(showtimeId);
            return Ok();
        }
    }
}