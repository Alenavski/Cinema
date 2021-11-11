using System;
using System.Collections.Generic;

namespace Cinema.Services.Dtos
{
    public class TicketDto
    {
        public long Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public ShowtimeDto Showtime { get; set; }
        public DateTime DateOfShowtime { get; set; }
        public ICollection<AdditionDto> TicketsAdditions { get; set; }
        public ICollection<TicketSeatDto> TicketsSeats { get; set; }

        public TicketDto()
        {

        }

        public TicketDto(
            long id,
            DateTime dateOfBooking,
            ShowtimeDto showtime,
            ICollection<AdditionDto> ticketsAdditions,
            ICollection<TicketSeatDto> ticketsSeats,
            DateTime dateOfShowtime
        )
        {
            Id = id;
            DateOfBooking = dateOfBooking;
            Showtime = showtime;
            TicketsAdditions = ticketsAdditions;
            TicketsSeats = ticketsSeats;
            DateOfShowtime = dateOfShowtime;
        }
    }
}