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
            return Ok(await _cinemaService.AddCinema(cinemaDto));
        }

        [HttpGet]
        public IActionResult GetCinemas()
        {
            return Ok(_cinemaService.GetCinemas());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCinemaById(int id)
        {
            var cinema = await _cinemaService.GetCinemaById(id);

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
            var cinema = await _cinemaService.GetCinemaById(id);

            if (cinema == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such cinema doesn't exist"
                    }
                );
            }

            return Ok(await _cinemaService.UpdateCinema(id, cinemaDto));
        }
    }
}