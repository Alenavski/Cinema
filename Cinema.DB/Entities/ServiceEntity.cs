using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HallServiceEntity> Halls { get; set; }
    }
}