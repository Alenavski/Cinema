using System;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("showtimes")]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpGet]
        public IActionResult GetShowtimes([FromQuery] ShowtimeFilterDto showtimeFilterDto)
        {
            var movies = _showtimeService.GetMoviesByFilter(showtimeFilterDto);
            return Ok(movies);
        }
    }
}