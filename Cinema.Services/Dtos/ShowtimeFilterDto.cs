using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class ShowtimeFilterDto
    {
        [MaxLength(50)]
        public string CinemaName { get; set; }

        [MaxLength(150)]
        public string MovieTitle { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [Required]
        public int NumberOfFreeSeats { get; set; }
    }
}