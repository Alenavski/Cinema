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
    public class AdditionService : IAdditionService
    {
        private readonly ApplicationContext _context;

        public AdditionService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdditionDto>> GetAdditionsAsync()
        {
            return await _context.Additions
                .ProjectToType<AdditionDto>()
                .ToListAsync();
        }

        public async Task<AdditionDto> GetAdditionByIdAsync(int id)
        {
            return await _context.Additions
                .Where(s => s.Id == id)
                .ProjectToType<AdditionDto>()
                .SingleOrDefaultAsync();
        }

        public async Task<int> AddAdditionAsync(AdditionDto additionDto)
        {
            var service = additionDto.Adapt<AdditionEntity>();
            await _context.Additions.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }
    }
}