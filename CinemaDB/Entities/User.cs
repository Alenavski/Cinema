using System.ComponentModel.DataAnnotations;

namespace CinemaDB.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(16)]
        public string Salt { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Role { get; set; }
    }
}