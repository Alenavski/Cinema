namespace Cinema.DB.Entities
{
    public class TicketAdditionEntity
    {
        public long TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public int AdditionId { get; set; }
        public AdditionEntity Addition { get; set; }
        public byte Count { get; set; }
    }
}