using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("cinemas/{cinemaId:int}/halls")]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpPost]
        public async Task<IActionResult> AddHall(int cinemaId, [FromBody] HallDto hallDto)
        {
            return Ok(await _hallService.AddHallAsync(cinemaId, hallDto));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHall(int id)
        {
            var hall = await _hallService.GetHallByIdAsync(id);

            if (hall == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such hall doesn't exist"
                    }
                );
            }

            return Ok(hall);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _hallService.GetHallByIdAsync(id);

            if (hall == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such hall doesn't exist"
                    }
                );
            }

            await _hallService.DeleteHallAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHall(int id, [FromBody] HallDto hallDto)
        {
            var hall = await _hallService.GetHallByIdAsync(id);

            if (hall == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such hall doesn't exist"
                    }
                );
            }

            await _hallService.UpdateHallAsync(id, hallDto);
            return Ok();
        }
    }
}