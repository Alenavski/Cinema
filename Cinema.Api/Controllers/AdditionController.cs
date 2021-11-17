using System.Threading.Tasks;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAddition(AdditionDto additionDto)
        {
            return Ok(await _additionService.AddAdditionAsync(additionDto));
        }
    }
}