using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class ShowtimeDto
    {
        public long Id { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public short NumberOfFreeSeats { get; set; }

        [Required]
        public HallDto Hall { get; set; }

        public ShowtimeDto(long id, TimeSpan time, short numberOfFreeSeats, HallDto hall)
        {
            Id = id;
            Time = time;
            NumberOfFreeSeats = numberOfFreeSeats;
            Hall = hall;
        }
    }
}