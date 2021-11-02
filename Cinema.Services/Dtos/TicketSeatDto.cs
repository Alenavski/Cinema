namespace Cinema.Services.Dtos
{
    public class TicketSeatDto
    {
        public TicketDto Ticket { get; set; }
        public SeatDto Seat { get; set; }
        public bool IsOrdered { get; set; }

        public TicketSeatDto(TicketDto ticket, SeatDto seat, bool isOrdered)
        {
            Ticket = ticket;
            Seat = seat;
            IsOrdered = isOrdered;
        }
    }
}