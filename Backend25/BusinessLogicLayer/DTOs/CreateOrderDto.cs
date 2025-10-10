using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string Address { get; set; } = null!;
    }
}