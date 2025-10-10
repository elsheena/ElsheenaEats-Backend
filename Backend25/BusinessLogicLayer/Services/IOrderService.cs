using BusinessLogicLayer.DTOs;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto createOrderDto);
        Task<List<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> GetOrderDetailsAsync(Guid userId, Guid orderId);
        Task<bool> ConfirmDeliveryAsync(Guid userId, Guid orderId);
        Task<bool> ValidateDeliveryTimeAsync(DateTime deliveryTime);
    }

    public class OrderService : IOrderService
    {
        private readonly ElsheenaDbContext _context;
        private readonly ICartService _cartService;
        private readonly int _minimumDeliveryTimeInMinutes = 30; // Server-defined minimum time

        public OrderService(ElsheenaDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto createOrderDto)
        {
            // Validate delivery time
            if (!await ValidateDeliveryTimeAsync(createOrderDto.DeliveryTime))
            {
                throw new ArgumentException($"Delivery time must be at least {_minimumDeliveryTimeInMinutes} minutes from now");
            }

            // Get cart items
            var cartItems = await _context.DishesInCarts
                .Include(d => d.Dish)
                .Where(d => d.UserId == userId && d.OrderId == null)
                .ToListAsync();

            if (!cartItems.Any())
            {
                throw new InvalidOperationException("Cart is empty");
            }

            // Calculate total price
            var totalPrice = cartItems.Sum(item => item.TotalPrice);

            // Create order
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DeliveryTime = createOrderDto.DeliveryTime,
                Address = createOrderDto.Address,
                Price = totalPrice,
                Status = Status.InCart, // Initially set to InCart, then change to Ordered
                CreateDateTime = DateTime.UtcNow,
                ModifyDateTime = DateTime.UtcNow
            };

            _context.Orders.Add(order);

            // Associate cart items with the order
            foreach (var cartItem in cartItems)
            {
                cartItem.OrderId = order.Id;
            }

            // Change order status to Ordered after cart items are associated
            order.Status = Status.Ordered;

            await _context.SaveChangesAsync();

            return await GetOrderDetailsAsync(userId, order.Id);
        }

        public async Task<List<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId && o.DeleteDate == null)
                .OrderByDescending(o => o.CreateDateTime)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    DeliveryTime = o.DeliveryTime,
                    CreateDateTime = o.CreateDateTime,
                    Price = o.Price,
                    Status = o.Status,
                    Address = o.Address,
                    Dishes = o.DishInCarts.Select(d => new DishInCartDto
                    {
                        Id = d.Id,
                        DishId = d.DishId,
                        DishName = d.Dish.Name,
                        DishPrice = d.Dish.Price,
                        DishImage = d.Dish.Image,
                        Amount = d.Amount,
                        TotalPrice = d.TotalPrice
                    }).ToList()
                })
                .ToListAsync();

            return orders;
        }

        public async Task<OrderDto> GetOrderDetailsAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.DishInCarts)
                .ThenInclude(d => d.Dish)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId && o.DeleteDate == null);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id = {orderId} not found");
            }

            return new OrderDto
            {
                Id = order.Id,
                DeliveryTime = order.DeliveryTime,
                CreateDateTime = order.CreateDateTime,
                Price = order.Price,
                Status = order.Status,
                Address = order.Address,
                Dishes = order.DishInCarts.Select(d => new DishInCartDto
                {
                    Id = d.Id,
                    DishId = d.DishId,
                    DishName = d.Dish.Name,
                    DishPrice = d.Dish.Price,
                    DishImage = d.Dish.Image,
                    Amount = d.Amount,
                    TotalPrice = d.TotalPrice
                }).ToList()
            };
        }

        public async Task<bool> ConfirmDeliveryAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId && o.DeleteDate == null);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id = {orderId} not found");
            }

            // Only "In Process" orders can be confirmed for delivery
            if (order.Status != Status.Delivery)
            {
                throw new InvalidOperationException("Only orders with 'In Process' status can be confirmed for delivery");
            }

            // Update order status to delivered
            order.Status = Status.Delivered;
            order.ModifyDateTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ValidateDeliveryTimeAsync(DateTime deliveryTime)
        {
            var minimumDeliveryTime = DateTime.Now.AddMinutes(_minimumDeliveryTimeInMinutes);
            return deliveryTime >= minimumDeliveryTime;
        }
    }
}