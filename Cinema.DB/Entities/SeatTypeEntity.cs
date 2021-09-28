using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class SeatTypeEntity
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<SeatEntity> Seats { get; set; }
    }
}