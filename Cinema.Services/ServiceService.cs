using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationContext _context;

        public ServiceService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesAsync()
        {
            return await _context.Services
                .ProjectToType<ServiceDto>()
                .ToListAsync();
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            return await _context.Services
                .Where(s => s.Id == id)
                .ProjectToType<ServiceDto>()
                .SingleOrDefaultAsync();
        }

        public async Task<int> AddServiceAsync(ServiceDto serviceDto)
        {
            var service = serviceDto.Adapt<ServiceEntity>();
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(s => s.Id == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}