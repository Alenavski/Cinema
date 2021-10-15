using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class HallDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ServiceDto> Services { get; set; }

        public ICollection<SeatDto> Seats { get; set; }

        public HallDto(int id, string name, ICollection<ServiceDto> services, ICollection<SeatDto> seats)
        {
            Id = id;
            Name = name;
            Services = services;
            Seats = seats;
        }
    }
}