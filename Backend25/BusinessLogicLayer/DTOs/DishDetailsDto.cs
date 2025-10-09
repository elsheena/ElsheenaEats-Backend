using Core.Models;

namespace BusinessLogicLayer.DTOs
{
    public class DishDetailsDto : DishListItemDto
    {
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public List<RatingDto> Ratings { get; set; } = new List<RatingDto>();
    }
}