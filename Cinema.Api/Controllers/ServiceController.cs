using System.Threading.Tasks;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            return Ok(await _serviceService.GetServicesAsync());
        }

        [HttpGet("/halls/{hallId:int}")]
        public async Task<IActionResult> GetServicesOfHall(int hallId)
        {
            return Ok(await _serviceService.GetServicesOfHallAsync(hallId));
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceService.GetServiceAsync(id);

            if (service == null)
            {
                return NotFound(
                    new
                    {
                        message = "Such service doesn't exist"
                    }
                );
            }

            await _serviceService.DeleteServiceAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceDto serviceDto)
        {
            return Ok(await _serviceService.AddServiceAsync(serviceDto));
        }

        [HttpPost("/halls/{hallId:int}")]
        public async Task<IActionResult> AddServiceToHall(int hallId, [FromBody] ServiceDto serviceDto)
        {
            await _serviceService.AddServiceToHallAsync(hallId, serviceDto.Id);
            return Ok();
        }

        [HttpDelete("/{serviceId:int}/halls/{hallId:int}")]
        public async Task<IActionResult> DeleteServiceFromHall(int serviceId, int hallId)
        {
            await _serviceService.DeleteServiceFromHallAsync(hallId, serviceId);
            return Ok();
        }
    }
}