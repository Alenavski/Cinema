using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, 53280)]
        public int MinutesLength { get; set; }

        public byte[] Poster { get; set; }
        
        public ICollection<ShowtimeDto> Showtimes { get; set; }

        public MovieDto(
            int id,
            string title,
            string description,
            DateTime startDate,
            DateTime endDate,
            int minutesLength,
            byte[] poster,
            ICollection<ShowtimeDto> showtimes
        )
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            MinutesLength = minutesLength;
            Poster = poster;
            Showtimes = showtimes;
        }
    }
}