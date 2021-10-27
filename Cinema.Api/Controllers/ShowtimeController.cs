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

        [HttpGet("cinemas")]
        public async Task<IActionResult> GetCinemasByMovieId(int movieId)
        {
            return Ok(await _showtimeService.GetCinemasByMovieIdAsync(movieId));
        }

        [HttpPost]
        public async Task<IActionResult> AddShowtime(int movieId, ShowtimeDto showtimeDto)
        {
            var canAddShowtime = await _showtimeService.CanAddShowtimeAsync(movieId, showtimeDto);

            if (!canAddShowtime)
            {
                return BadRequest(
                    new
                    {
                        message = "Can't add showtime at time in this hall"
                    }
                );
            }

            await _showtimeService.AddShowtimeAsync(movieId, showtimeDto);
            return Ok();
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