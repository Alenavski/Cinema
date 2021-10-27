namespace Cinema.DB.Entities
{
    public class TicketPriceEntity
    {
        public long ShowtimeId { get; set; }
        public ShowtimeEntity Showtime { get; set; }
        public short SeatTypeId { get; set; }
        public SeatTypeEntity SeatType { get; set; }
        public decimal Price { get; set; }
    }
}