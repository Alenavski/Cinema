using System;
using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetMovies([FromQuery] DateTime date)
        {
            return Ok(await _movieService.GetMovies(date));
        }


        [HttpGet("with-showtimes")]
        public async Task<IActionResult> GetMoviesWithShowtimes([FromQuery] ShowtimeFilterDto showtimeFilterDto)
        {
            return Ok(await _movieService.GetMoviesByFilterAsync(showtimeFilterDto));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDto)
        {
            return Ok(await _movieService.AddMovieAsync(movieDto));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such movie doesn't exist"
                    }
                );
            }

            return Ok(movie);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such movie doesn't exist"
                    }
                );
            }

            await _movieService.DeleteMovieAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such movie doesn't exist"
                    }
                );
            }

            await _movieService.UpdateMovieAsync(movieDto);
            return Ok();
        }
    }
}