using Core.Models;

namespace BusinessLogicLayer.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; } = null!;
        public List<DishInCartDto> Dishes { get; set; } = new List<DishInCartDto>();
    }
}