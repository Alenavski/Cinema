﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class HallDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<HallServiceDto> Services { get; set; }

        public ICollection<SeatDto> Seats { get; set; }

        public HallDto(int id, string name, ICollection<HallServiceDto> services, ICollection<SeatDto> seats)
        {
            Id = id;
            Name = name;
            Services = services;
            Seats = seats;
        }
    }
}