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
        
        [HttpPost("add")]
        public async Task<IActionResult> AddCinema([FromBody] CinemaDto cinemaDto)
        {
            return Ok(await _cinemaService.AddCinema(cinemaDto));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCinemaById(int id)
        {
            return Ok(await _cinemaService.GetCinemaById(id));
        }
    }
}