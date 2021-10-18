namespace Cinema.DB.Entities
{
    public class HallServiceEntity
    {
        public int HallId { get; set; }
        public HallEntity Hall { get; set; }
        public int ServiceId { get; set; }
        public ServiceEntity Service { get; set; }
        public decimal Price { get; set; }
    }
}