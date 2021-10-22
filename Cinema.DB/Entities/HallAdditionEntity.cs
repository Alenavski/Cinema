using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class HallAdditionEntity
    {
        public int HallId { get; set; }
        public HallEntity Hall { get; set; }
        public int AdditionId { get; set; }
        public AdditionEntity Addition { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<ShowtimeAdditionEntity> ShowtimesAdditions { get; set; }
    }
}