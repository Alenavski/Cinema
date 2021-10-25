namespace Cinema.Services.Dtos
{
    public class ShowtimeAdditionDto
    {
        public AdditionDto Addition { get; set; }
        
        public ShowtimeAdditionDto(AdditionDto addition)
        {
            Addition = addition;
        }
    }
}