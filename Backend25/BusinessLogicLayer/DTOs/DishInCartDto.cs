namespace BusinessLogicLayer.DTOs
{
    public class DishInCartDto
    {
        public Guid Id { get; set; }
        public Guid DishId { get; set; }
        public string DishName { get; set; } = null!;
        public double DishPrice { get; set; }
        public string DishImage { get; set; } = null!;
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
    }
}