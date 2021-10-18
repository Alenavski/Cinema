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

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

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
    }
}