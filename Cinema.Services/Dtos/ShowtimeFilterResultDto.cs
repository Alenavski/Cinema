using System;
using System.Collections.Generic;

namespace Cinema.Services.Dtos
{
    public class ShowtimeFilterResultDto
    {
        public long Id { get; set; }
        public TimeSpan Time { get; set; }
        public short NumberOfFreeSeats { get; set; }
        public HallDto Hall { get; set; }
        public MovieDto Movie { get; set; }
        public ICollection<AdditionDto> Additions { get; set; }
        public ICollection<TicketPriceDto> Prices { get; set; }

        public ShowtimeFilterResultDto(
            long id,
            TimeSpan time,
            short numberOfFreeSeats,
            HallDto hall,
            MovieDto movie,
            ICollection<AdditionDto> additions,
            ICollection<TicketPriceDto> prices
        )
        {
            Id = id;
            Time = time;
            NumberOfFreeSeats = numberOfFreeSeats;
            Hall = hall;
            Movie = movie;
            Additions = additions;
            Prices = prices;
        }
    }
}