using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DishInCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DishId { get; set; }
        public Guid? OrderId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Dish Dish { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
