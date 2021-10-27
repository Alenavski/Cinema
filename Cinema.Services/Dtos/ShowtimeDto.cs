using System;
using System.Collections.Generic;
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
        
        public ICollection<AdditionDto> Additions { get; set; }
        
        public ICollection<TicketPriceDto> Prices { get; set; }

        public ShowtimeDto(
            long id, 
            TimeSpan time, 
            short numberOfFreeSeats, 
            HallDto hall,
            ICollection<TicketPriceDto> prices, 
            ICollection<AdditionDto> additions
        )
        {
            Id = id;
            Time = time;
            NumberOfFreeSeats = numberOfFreeSeats;
            Hall = hall;
            Prices = prices;
            Additions = additions;
        }
    }
}