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
    public class HallService : IHallService
    {
        private readonly ApplicationContext _context;

        public HallService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HallDto> GetHallByIdAsync(int id)
        {
            return await _context.Halls
                .Include(h => h.Seats)
                .Where(h => h.Id == id)
                .ProjectToType<HallDto>()
                .SingleOrDefaultAsync();
        }

        public async Task DeleteHallAsync(int id)
        {
            var hall = await _context.Halls.FirstOrDefaultAsync(h => h.Id == id);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHallAsync(int id, HallDto hallDto)
        {
            var hall = await _context.Halls
                .Include(h => h.Seats)
                .ThenInclude(s => s.SeatType)
                .FirstOrDefaultAsync(h => h.Id == id);

            hall.Name = hallDto.Name;

            var seatTypes = await _context.SeatTypes.ToListAsync();
            foreach (var seat in hallDto.Seats)
            {
                if (seat.Id == 0)
                {
                    var seatType = seatTypes.SingleOrDefault(st => st.Id == seat.SeatType.Id);
                    await _context.Seats.AddAsync(
                        new SeatEntity
                        {
                            Hall = hall,
                            Index = seat.Index,
                            Id = seat.Id,
                            Place = seat.Place,
                            Row = seat.Row,
                            SeatType = seatType
                        }
                    );
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddHallAsync(int cinemaId, HallDto hallDto)
        {
            var cinema = await _context.Cinemas
                .Include(c => c.Halls)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            var hall = hallDto.Adapt<HallEntity>();

            hall.Seats = null;
            cinema.Halls.Add(hall);

            var seatTypes = await _context.SeatTypes.ToListAsync();
            foreach (var seat in hallDto.Seats)
            {
                if (seat.Id == 0)
                {
                    var seatType = seatTypes.SingleOrDefault(st => st.Id == seat.SeatType.Id);
                    await _context.Seats.AddAsync(
                        new SeatEntity
                        {
                            Hall = hall,
                            Index = seat.Index,
                            Id = seat.Id,
                            Place = seat.Place,
                            Row = seat.Row,
                            SeatType = seatType
                        }
                    );
                }
            }

            await _context.SaveChangesAsync();
            return hall.Id;
        }
    }
}