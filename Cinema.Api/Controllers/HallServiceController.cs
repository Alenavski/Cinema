using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("halls/{hallId:int}/services")]
    public class HallServiceController : ControllerBase
    {
        private readonly IHallServiceService _hallServiceService;
        private readonly IHallService _hallService;
        private readonly IServiceService _serviceService;

        public HallServiceController(IHallServiceService hallServiceService, IServiceService serviceService, IHallService hallService)
        {
            _hallServiceService = hallServiceService;
            _serviceService = serviceService;
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServicesOfHall(int hallId)
        {
            return Ok(await _hallServiceService.GetServicesOfHallAsync(hallId));
        }

        [HttpPost("{serviceId:int}")]
        public async Task<IActionResult> AddServiceToHall(int hallId, int serviceId, [FromBody] HallServiceDto hallServiceDto)
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

            var service = await _serviceService.GetServiceByIdAsync(serviceId);
            if (service == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such service doesn't exist"
                    }
                );
            }

            await _hallServiceService.AddServiceToHallAsync(hallId, serviceId, hallServiceDto.Price);
            return Ok();
        }

        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> DeleteServiceFromHall(int serviceId, int hallId)
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

            var service = await _serviceService.GetServiceByIdAsync(serviceId);
            if (service == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such service doesn't exist"
                    }
                );
            }

            await _hallServiceService.DeleteServiceFromHallAsync(hallId, serviceId);
            return Ok();
        }
    }
}