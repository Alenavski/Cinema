using System.Threading.Tasks;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class CinemaService : ICinemaService
    {
        private ApplicationContext _context;

        public CinemaService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> AddCinema(CinemaDto cinemaDto)
        {
            var cinemaEntity = cinemaDto.Adapt<CinemaEntity>();
            await _context.Cinemas.AddAsync(cinemaEntity);
            await _context.SaveChangesAsync();
            return cinemaEntity.Id;
        }

        public async Task<CinemaDto> GetCinemaById(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            return cinema.Adapt<CinemaDto>();
        }
    }
}