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

        [Required]
        public CinemaDto Cinema { get; set; }

        public HallDto(int id, string name, CinemaDto cinema)
        {
            Id = id;
            Name = name;
            Cinema = cinema;
        }
    }
}