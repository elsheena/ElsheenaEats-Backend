using BusinessLogicLayer.DTOs;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface ICartService
    {
        Task<List<DishInCartDto>> GetCartAsync(Guid userId);
        Task<DishInCartDto> AddToCartAsync(Guid userId, AddToCartDto addToCartDto);
        Task<DishInCartDto> UpdateCartItemAsync(Guid userId, Guid dishId, UpdateCartItemDto updateCartDto);
        Task<bool> RemoveFromCartAsync(Guid userId, Guid dishId);
        Task<bool> ClearCartAsync(Guid userId);
        Task<double> GetCartTotalAsync(Guid userId);
    }

    public class CartService : ICartService
    {
        private readonly ElsheenaDbContext _context;

        public CartService(ElsheenaDbContext context)
        {
            _context = context;
        }

        public async Task<List<DishInCartDto>> GetCartAsync(Guid userId)
        {
            var cartItems = await _context.DishesInCarts
                .Include(d => d.Dish)
                .Where(d => d.UserId == userId && d.OrderId == null) // Only items not in an order yet
                .Select(d => new DishInCartDto
                {
                    Id = d.Id,
                    DishId = d.DishId,
                    DishName = d.Dish.Name,
                    DishPrice = d.Dish.Price,
                    DishImage = d.Dish.Image,
                    Amount = d.Amount,
                    TotalPrice = d.Amount * d.Dish.Price
                })
                .ToListAsync();

            return cartItems;
        }

        public async Task<DishInCartDto> AddToCartAsync(Guid userId, AddToCartDto addToCartDto)
        {
            // Check if dish exists
            var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == addToCartDto.DishId && d.DeleteDate == null);
            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {addToCartDto.DishId} not found");
            }

            // Check if item already exists in cart
            var existingCartItem = await _context.DishesInCarts
                .FirstOrDefaultAsync(d => d.UserId == userId && d.DishId == addToCartDto.DishId && d.OrderId == null);

            if (existingCartItem != null)
            {
                // Update existing item
                existingCartItem.Amount += addToCartDto.Amount;
                existingCartItem.TotalPrice = existingCartItem.Amount * dish.Price;
            }
            else
            {
                // Create new cart item
                existingCartItem = new DishInCart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    DishId = addToCartDto.DishId,
                    Amount = addToCartDto.Amount,
                    TotalPrice = addToCartDto.Amount * dish.Price
                };
                _context.DishesInCarts.Add(existingCartItem);
            }

            await _context.SaveChangesAsync();

            return new DishInCartDto
            {
                Id = existingCartItem.Id,
                DishId = dish.Id,
                DishName = dish.Name,
                DishPrice = dish.Price,
                DishImage = dish.Image,
                Amount = existingCartItem.Amount,
                TotalPrice = existingCartItem.TotalPrice
            };
        }

        public async Task<DishInCartDto> UpdateCartItemAsync(Guid userId, Guid dishId, UpdateCartItemDto updateCartDto)
        {
            var cartItem = await _context.DishesInCarts
                .Include(d => d.Dish)
                .FirstOrDefaultAsync(d => d.UserId == userId && d.DishId == dishId && d.OrderId == null);

            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            cartItem.Amount = updateCartDto.Amount;
            cartItem.TotalPrice = cartItem.Amount * cartItem.Dish.Price;

            await _context.SaveChangesAsync();

            return new DishInCartDto
            {
                Id = cartItem.Id,
                DishId = cartItem.Dish.Id,
                DishName = cartItem.Dish.Name,
                DishPrice = cartItem.Dish.Price,
                DishImage = cartItem.Dish.Image,
                Amount = cartItem.Amount,
                TotalPrice = cartItem.TotalPrice
            };
        }

        public async Task<bool> RemoveFromCartAsync(Guid userId, Guid dishId)
        {
            var cartItem = await _context.DishesInCarts
                .FirstOrDefaultAsync(d => d.UserId == userId && d.DishId == dishId && d.OrderId == null);

            if (cartItem == null)
            {
                return false;
            }

            _context.DishesInCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ClearCartAsync(Guid userId)
        {
            var cartItems = await _context.DishesInCarts
                .Where(d => d.UserId == userId && d.OrderId == null)
                .ToListAsync();

            if (cartItems.Any())
            {
                _context.DishesInCarts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<double> GetCartTotalAsync(Guid userId)
        {
            return await _context.DishesInCarts
                .Where(d => d.UserId == userId && d.OrderId == null)
                .SumAsync(d => d.TotalPrice);
        }
    }
}