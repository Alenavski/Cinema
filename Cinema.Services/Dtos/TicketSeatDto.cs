using System;

namespace Cinema.Services.Dtos
{
    public class TicketSeatDto
    {
        public SeatDto Seat { get; set; }
        public bool IsOrdered { get; set; }

        public DateTime BlockingTime { get; set; }

        public TicketSeatDto()
        {

        }

        public TicketSeatDto(SeatDto seat, bool isOrdered)
        {
            Seat = seat;
            IsOrdered = isOrdered;
        }
    }
}