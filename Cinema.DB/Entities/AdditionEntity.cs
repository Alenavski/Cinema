using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class AdditionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HallAdditionEntity> Halls { get; set; }
        public ICollection<ShowtimeAdditionEntity> Showtimes { get; set; }
    }
}