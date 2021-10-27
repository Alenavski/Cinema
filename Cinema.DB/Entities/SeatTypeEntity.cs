using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class SeatTypeEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public ICollection<TicketPriceEntity> Prices { get; set; }
    }
}