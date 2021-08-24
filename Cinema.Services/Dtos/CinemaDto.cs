using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class CinemaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Address { get; set; }

        public byte[] Image { get; set; }

        public CinemaDto(int id, string name, string city, string address, byte[] image)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            Image = image;
        }
    }
}