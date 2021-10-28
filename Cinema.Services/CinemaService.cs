using System;
using System.Collections.Generic;
using System.IO;
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
    public class CinemaService : ICinemaService
    {
        private ApplicationContext _context;

        public CinemaService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> AddCinemaAsync(CinemaDto cinemaDto)
        {
            var cinemaEntity = cinemaDto.Adapt<CinemaEntity>();
            await _context.Cinemas.AddAsync(cinemaEntity);
            await _context.SaveChangesAsync();
            return cinemaEntity.Id;
        }

        public async Task<CinemaDto> GetCinemaByIdAsync(int id)
        {
            return await _context.Cinemas
                .Include(c => c.Halls)
                .ThenInclude(h => h.Seats)
                .Where(c => c.Id == id)
                .ProjectToType<CinemaDto>()
                .SingleOrDefaultAsync();
        }

        public async Task UpdateCinemaAsync(int id, CinemaDto cinemaDto)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);

            cinema.Name = cinemaDto.Name;
            cinema.City = cinemaDto.City;
            cinema.Address = cinemaDto.Address;
            cinema.Image = cinemaDto.Image ?? cinema.Image;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync(string term)
        {
            return term != null
                ? await _context.Cinemas
                    .Where(c => c.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                    .ProjectToType<CinemaDto>()
                    .ToListAsync()
                : await _context.Cinemas
                    .Include(c => c.Halls)
                    .ProjectToType<CinemaDto>()
                    .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCitiesByTermAsync(string term)
        {
            return await _context.Cinemas
                .Where(c => c.City.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.City)
                .Distinct()
                .ToListAsync();
        }

        public async Task DeleteCinemaAsync(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
        }
    }
}