using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("cinemas")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCinema([FromBody] CinemaDto cinemaDto)
        {
            return Ok(await _cinemaService.AddCinemaAsync(cinemaDto));
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCitiesByTerm([FromQuery] string term)
        {
            return Ok(await _cinemaService.GetCitiesByTermAsync(term));
        }

        [HttpGet]
        public async Task<IActionResult> GetCinemas([FromQuery] string term)
        {
            return Ok(await _cinemaService.GetCinemasAsync(term));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCinemaById(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);

            if (cinema == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such cinema doesn't exist"
                    }
                );
            }

            return Ok(cinema);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCinema(int id, [FromBody] CinemaDto cinemaDto)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);

            if (cinema == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such cinema doesn't exist"
                    }
                );
            }

            await _cinemaService.UpdateCinemaAsync(id, cinemaDto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);

            if (cinema == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such cinema doesn't exist"
                    }
                );
            }

            await _cinemaService.DeleteCinemaAsync(id);
            return Ok();
        }
    }
}