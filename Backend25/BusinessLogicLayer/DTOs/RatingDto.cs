namespace BusinessLogicLayer.DTOs
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public int RatingValue { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
    }
}