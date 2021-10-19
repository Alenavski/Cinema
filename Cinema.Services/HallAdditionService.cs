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
    public class HallAdditionService : IHallAdditionService
    {
        private readonly ApplicationContext _context;

        public HallAdditionService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HallAdditionDto>> GetHallAdditionsAsync(int hallId)
        {
            return await _context.HallsAdditions
                .Include(ha => ha.Hall)
                .Include(ha => ha.Addition)
                .Where(ha => ha.Hall.Id == hallId)
                .ProjectToType<HallAdditionDto>()
                .ToListAsync();
        }

        public async Task AddAdditionToHallAsync(int hallId, int additionId, decimal price)
        {
            var service = await _context.Additions.SingleOrDefaultAsync(s => s.Id == additionId);
            var hall = await _context.Halls
                .Include(h => h.Additions)
                .SingleOrDefaultAsync(h => h.Id == hallId);

            var hallService = new HallAdditionEntity
            {
                Hall = hall,
                Addition = service,
                Price = price
            };
            _context.HallsAdditions.Add(hallService);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdditionFromHallAsync(int hallId, int additionId)
        {
            var serviceHall = await _context.HallsAdditions
                .Include(hs => hs.Hall)
                .Include(hs => hs.Addition)
                .SingleOrDefaultAsync(hs => hs.Hall.Id == hallId && hs.Addition.Id == additionId);
            _context.HallsAdditions.Remove(serviceHall);
            await _context.SaveChangesAsync();
        }
    }
}