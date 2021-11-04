using System;
using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class ShowtimeDateEntity
    {
        public long Id { get; set; }
        public long ShowtimeId { get; set; }
        public ShowtimeEntity Showtime { get; set; }
        public DateTime Date { get; set; }

        public ICollection<TicketEntity> Tickets { get; set; }
    }
}