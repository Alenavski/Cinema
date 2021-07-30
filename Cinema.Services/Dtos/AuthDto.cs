using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class AuthDto
    {
        [EmailAddress]
        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}