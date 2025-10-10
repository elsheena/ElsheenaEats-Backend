using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class CreateRatingDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int RatingValue { get; set; }

        [StringLength(1000, ErrorMessage = "Review text cannot exceed 1000 characters")]
        public string? Text { get; set; }
    }
}