using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class ShowtimeDto
    {
        public long Id { get; set; }

        [Required]
        public DateTime Time { get; set; }
        public short NumberOfFreeSeats { get; set; }

        [Required]
        public MovieDto Movie { get; set; }

        [Required]
        public HallDto Hall { get; set; }

        public ShowtimeDto(long id, DateTime time, short numberOfFreeSeats, MovieDto movie, HallDto hall)
        {
            Id = id;
            Time = time;
            NumberOfFreeSeats = numberOfFreeSeats;
            Movie = movie;
            Hall = hall;
        }
    }
}