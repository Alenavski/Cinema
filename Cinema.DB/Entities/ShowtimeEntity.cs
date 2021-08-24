using System;

namespace Cinema.DB.Entities
{
    public class ShowtimeEntity
    {
        public long Id { get; set; }
        public TimeSpan Time { get; set; }
        public short NumberOfFreeSeats { get; set; }
        public MovieEntity Movie { get; set; }
        public HallEntity Hall { get; set; }
    }
}