namespace Cinema.Services.Dtos
{
    public class HallServiceDto
    {
        public decimal Price { get; set; }

        public HallServiceDto(decimal price)
        {
            Price = price;
        }
    }
}