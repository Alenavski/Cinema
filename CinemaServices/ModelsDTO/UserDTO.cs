using CinemaDB.Entities;

namespace CinemaServices.ModelsDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public UserDTO()
        {
            
        }

        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}