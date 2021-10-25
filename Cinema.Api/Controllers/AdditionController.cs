using System.Threading.Tasks;
using Cinema.Services.Constants;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("additions")]
    public class AdditionController : ControllerBase
    {
        private readonly IAdditionService _additionService;

        public AdditionController(IAdditionService additionService)
        {
            _additionService = additionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditions()
        {
            return Ok(await _additionService.GetAdditionsAsync());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAddition(int id)
        {
            var addition = await _additionService.GetAdditionByIdAsync(id);

            if (addition == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such addition doesn't exist"
                    }
                );
            }

            await _additionService.DeleteAdditionAsync(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAddition(AdditionDto additionDto)
        {
            return Ok(await _additionService.AddAdditionAsync(additionDto));
        }
    }
}