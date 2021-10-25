namespace Cinema.DB.Entities
{
    public class ShowtimeAdditionEntity
    {
        public long ShowtimeId { get; set; }
        public ShowtimeEntity Showtime { get; set; }
        public int AdditionId { get; set; }

        public AdditionEntity Addition { get; set; }
    }
}