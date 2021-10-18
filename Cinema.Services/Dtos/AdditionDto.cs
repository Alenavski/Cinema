using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class AdditionDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public AdditionDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}