namespace Cinema.Services.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ServiceDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}