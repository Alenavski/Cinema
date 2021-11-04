namespace Cinema.DB.Entities
{
    public class SeatEntity
    {
        public long Id { get; set; }
        public byte Index { get; set; }
        public byte Row { get; set; }
        public byte Place { get; set; }
        public HallEntity Hall { get; set; }
        public SeatTypeEntity SeatType { get; set; }
    }
}