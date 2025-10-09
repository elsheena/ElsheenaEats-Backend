using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Models
{
    public class Rating : IBaseEntity
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string? Text { get; set; }
        public Guid DishId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public User User { get; set; } = null!;
        public Dish Dish { get; set; } = null!;
    }
}
