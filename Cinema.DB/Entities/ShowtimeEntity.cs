using System;
using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class ShowtimeEntity
    {
        public long Id { get; set; }
        public TimeSpan Time { get; set; }
        public short NumberOfFreeSeats { get; set; }
        public int MovieId { get; set; }
        public MovieEntity Movie { get; set; }
        public HallEntity Hall { get; set; }
        public ICollection<TicketPriceEntity> Prices { get; set; }
        public ICollection<ShowtimeAdditionEntity> Additions { get; set; }
        public ICollection<TicketEntity> Tickets { get; set; }
    }
}