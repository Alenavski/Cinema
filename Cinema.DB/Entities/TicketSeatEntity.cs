namespace Cinema.DB.Entities
{
    public class TicketSeatEntity
    {
        public long TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public long SeatId { get; set; }
        public SeatEntity Seat { get; set; }
        public bool IsOrdered { get; set; }
    }
}