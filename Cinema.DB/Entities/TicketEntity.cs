using System;
using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class TicketEntity
    {
        public long Id { get; set; }
        public bool IsOrdered { get; set; }
        public DateTime DateOfBooking { get; set; }
        public ShowtimeDateEntity ShowtimeDate { get; set; }
        public UserEntity User { get; set; }
        public ICollection<TicketAdditionEntity> TicketsAdditions { get; set; }
        public ICollection<TicketSeatEntity> TicketsSeats { get; set; }
    }
}