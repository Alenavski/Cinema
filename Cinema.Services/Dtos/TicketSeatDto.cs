namespace Cinema.Services.Dtos
{
    public class TicketSeatDto
    {
        public SeatDto Seat { get; set; }
        public bool IsOrdered { get; set; }

        public TicketSeatDto(SeatDto seat, bool isOrdered)
        {
            Seat = seat;
            IsOrdered = isOrdered;
        }
    }
}