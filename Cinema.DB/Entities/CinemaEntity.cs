using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class CinemaEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public ICollection<HallEntity> Halls { get; set; }
    }
}