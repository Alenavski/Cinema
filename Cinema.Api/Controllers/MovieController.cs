using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetMovies([FromQuery] ShowtimeFilterDto showtimeFilterDto)
        {
            var movies = _movieService.GetMoviesByFilter(showtimeFilterDto);
            return Ok(movies);
        }
    }
}