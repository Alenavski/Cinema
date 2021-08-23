using System;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("showtime")]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpGet("filter")]
        public IActionResult GetShowtimes([FromQuery] ShowtimeFilterDto showtimeFilterDto)
        {
            return Ok(new
            {
                showtimes = _showtimeService.GetShowtimesByFilter(showtimeFilterDto)
            });
        }

        [HttpGet("date")]
        public IActionResult GetDate()
        {
            return Ok(new
            {
                date = DateTime.Now
            });
        }
    }
}