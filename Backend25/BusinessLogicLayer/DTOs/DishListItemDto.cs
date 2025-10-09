using Core.Models;

namespace BusinessLogicLayer.DTOs
{
    public class DishListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Image { get; set; } = null!;
        public bool IsVegetarian { get; set; }
        public float AverageRating { get; set; }
        public Category Category { get; set; }
    }
}