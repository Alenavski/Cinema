using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HallEntity> Halls { get; set; }
    }
}