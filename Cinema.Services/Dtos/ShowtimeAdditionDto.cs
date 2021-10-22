namespace Cinema.Services.Dtos
{
    public class ShowtimeAdditionDto
    {
        public HallDto Hall { get; set; }
        public AdditionDto Addition { get; set; }
        
        public ShowtimeAdditionDto(HallDto hall, AdditionDto addition)
        {
            Hall = hall;
            Addition = addition;
        }
    }
}