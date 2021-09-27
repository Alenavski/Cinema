using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
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
        public async Task<IActionResult> AddCinema([FromBody] CinemaDto cinemaDto)
        {
            return Ok(await _cinemaService.AddCinemaAsync(cinemaDto));
        }

        [HttpGet]
        public IActionResult GetCinemas()
        {
            return Ok(_cinemaService.GetCinemasAsync());
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