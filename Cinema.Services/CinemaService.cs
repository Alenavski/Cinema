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
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
            return cinema?.Adapt<CinemaDto>();
        }

        public async Task<int> UpdateCinema(int id, CinemaDto cinemaDto)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
            if (cinema == null)
            {
                return -1;
            }

            cinema.Name = cinemaDto.Name;
            cinema.City = cinemaDto.City;
            cinema.Address = cinemaDto.Address;
            cinema.Image = cinemaDto.Image ?? cinema.Image;
            await _context.SaveChangesAsync();

            return cinema.Id;
        }

        public IEnumerable<CinemaDto> GetCinemas()
        {
            return _context.Cinemas.Adapt<CinemaDto[]>();
        }
    }
}