namespace Cinema.Services.Dtos
{
    public class SeatTypeDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public SeatTypeDto(byte id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}