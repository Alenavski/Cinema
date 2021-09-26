using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class HallService : IHallService
    {
        private ApplicationContext _context;

        public HallService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HallDto> GetHallById(int id)
        {
            var hall = await _context.Halls.Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == id);
            return hall?.Adapt<HallDto>();
        }

        public async Task DeleteHall(int id)
        {
            var hall = await _context.Halls.FirstOrDefaultAsync(h => h.Id == id);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHall(int id, HallDto hallDto)
        {
            var hall = await _context.Halls.Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == id);

            hall.Name = hallDto.Name;
            hall.Seats = hallDto.Seats.Adapt<SeatEntity[]>();
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddHall(int cinemaId, HallDto hallDto)
        {
            var cinema = await _context.Cinemas.Include(c => c.Halls)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            var hall = hallDto.Adapt<HallEntity>();
            cinema.Halls.Add(hall);
            await _context.SaveChangesAsync();
            return hall.Id;
        }
    }
}