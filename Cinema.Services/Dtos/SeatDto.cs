using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class SeatDto
    {
        public long Id { get; set; }
        public byte Index { get; set; }
        public byte Row { get; set; }
        public byte Place { get; set; }

        [Required]
        public SeatTypeDto SeatType { get; set; }

        public SeatDto(long id, byte index, byte row, byte place, SeatTypeDto seatType)
        {
            Id = id;
            Index = index;
            Row = row;
            Place = place;
            SeatType = seatType;
        }
    }
}