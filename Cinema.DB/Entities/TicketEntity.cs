using System;
using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class TicketEntity
    {
        public long Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public ShowtimeEntity Showtime { get; set; }
        public DateTime DateOfShowtime { get; set; }
        public UserEntity User { get; set; }
        public ICollection<TicketAdditionEntity> TicketsAdditions { get; set; }
        public ICollection<TicketSeatEntity> TicketsSeats { get; set; }
    }
}