using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class HallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CinemaEntity Cinema { get; set; }
        public ICollection<HallAdditionEntity> Additions { get; set; }
        public ICollection<SeatEntity> Seats { get; set; }
        public ICollection<ShowtimeEntity> Showtimes { get; set; }
    }
}