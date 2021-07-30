using Cinema.Services.Constants;

namespace Cinema.Services.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}