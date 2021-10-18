using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class HallServiceService : IHallServiceService
    {
        private readonly ApplicationContext _context;

        public HallServiceService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HallServiceDto>> GetServicesOfHallAsync(int hallId)
        {
            var hall = await _context.Halls
                .Include(h => h.Services)
                .ProjectToType<HallDto>()
                .SingleOrDefaultAsync(h => h.Id == hallId);
            return hall.Services;
        }

        public async Task AddServiceToHallAsync(int hallId, int serviceId, decimal price)
        {
            var service = await _context.Services.SingleOrDefaultAsync(s => s.Id == serviceId);
            var hall = await _context.Halls
                .Include(h => h.Services)
                .SingleOrDefaultAsync(h => h.Id == hallId);

            var hallService = new HallServiceEntity
            {
                Hall = hall,
                Service = service,
                Price = price
            };
            _context.HallsServices.Add(hallService);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceFromHallAsync(int hallId, int serviceId)
        {
            var serviceHall = await _context.HallsServices
                .Include(hs => hs.Hall)
                .Include(hs => hs.Service)
                .SingleOrDefaultAsync(hs => hs.Hall.Id == hallId && hs.Service.Id == serviceId);
            _context.HallsServices.Remove(serviceHall);
            await _context.SaveChangesAsync();
        }
    }
}