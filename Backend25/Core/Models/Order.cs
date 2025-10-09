using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Core.Common;

namespace Core.Models
{
    public class Order : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; } = null!;
        public virtual User User { get; set; } = null!; 
        public virtual ICollection<DishInCart> DishInCarts { get; set; } = new List<DishInCart>();

    }
}
