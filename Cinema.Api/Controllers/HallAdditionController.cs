using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("halls/{hallId:int}/additions")]
    public class HallAdditionController : ControllerBase
    {
        private readonly IHallAdditionService _hallAdditionService;
        private readonly IHallService _hallService;
        private readonly IAdditionService _additionService;

        public HallAdditionController(IHallAdditionService hallAdditionService, IAdditionService additionService, IHallService hallService)
        {
            _hallAdditionService = hallAdditionService;
            _additionService = additionService;
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHallAdditions(int hallId)
        {
            return Ok(await _hallAdditionService.GetHallAdditionsAsync(hallId));
        }

        [HttpPost("{additionId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdditionToHall(int hallId, int additionId, [FromBody] HallAdditionDto hallAdditionDto)
        {
            var hall = await _hallService.GetHallByIdAsync(hallId);
            if (hall == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such hall doesn't exist"
                    }
                );
            }

            var addition = await _additionService.GetAdditionByIdAsync(additionId);
            if (addition == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such addition doesn't exist"
                    }
                );
            }

            await _hallAdditionService.AddAdditionToHallAsync(hallId, additionId, hallAdditionDto.Price);
            return Ok();
        }

        [HttpDelete("{additionId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdditionFromHall(int additionId, int hallId)
        {
            var hall = await _hallService.GetHallByIdAsync(hallId);
            if (hall == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such hall doesn't exist"
                    }
                );
            }

            var addition = await _additionService.GetAdditionByIdAsync(additionId);
            if (addition == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such addition doesn't exist"
                    }
                );
            }

            await _hallAdditionService.DeleteAdditionFromHallAsync(hallId, additionId);
            return Ok();
        }
    }
}