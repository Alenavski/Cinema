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
        public string City { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public int NumberOfFreeSeats { get; set; }
    }
}