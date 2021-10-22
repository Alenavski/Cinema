using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class TicketPriceDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        public SeatTypeDto SeatType { get; set; }
        
        public TicketPriceDto(decimal price, SeatTypeDto seatType)
        {
            Price = price;
            SeatType = seatType;
        }
    }
}