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
        public ICollection<AdditionDto> Additions { get; set; }
        public ICollection<TicketSeatDto> TicketSeats { get; set; }

        public TicketDto(
            long id,
            DateTime dateOfBooking,
            ShowtimeDto showtime,
            ICollection<AdditionDto> additions,
            ICollection<TicketSeatDto> ticketSeats,
            DateTime dateOfShowtime
        )
        {
            Id = id;
            DateOfBooking = dateOfBooking;
            Showtime = showtime;
            Additions = additions;
            TicketSeats = ticketSeats;
            DateOfShowtime = dateOfShowtime;
        }
    }
}