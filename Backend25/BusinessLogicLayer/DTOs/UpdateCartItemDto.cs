using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class UpdateCartItemDto
    {
        [Required]
        [Range(1, 100, ErrorMessage = "Amount must be between 1 and 100")]
        public int Amount { get; set; }
    }
}