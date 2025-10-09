using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Core.Common;

namespace Core.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 0,
    }
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime RefreshExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public virtual ICollection<DishInCart> DishBaskets { get; set; } = new List<DishInCart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    }
}
