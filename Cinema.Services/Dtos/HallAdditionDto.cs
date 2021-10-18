using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Services.Dtos
{
    public class HallAdditionDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public HallAdditionDto(decimal price)
        {
            Price = price;
        }
    }
}