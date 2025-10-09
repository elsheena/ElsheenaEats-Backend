using Core.Models;

namespace BusinessLogicLayer.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}