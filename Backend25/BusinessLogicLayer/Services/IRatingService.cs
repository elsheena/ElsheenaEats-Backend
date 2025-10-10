using BusinessLogicLayer.DTOs;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface IRatingService
    {
        Task<RatingDto> CreateRatingAsync(Guid userId, Guid dishId, CreateRatingDto createRatingDto);
        Task<List<RatingDto>> GetDishRatingsAsync(Guid dishId);
        Task<bool> CanUserRateDishAsync(Guid userId, Guid dishId);
        Task UpdateDishAverageRatingAsync(Guid dishId);
    }

    public class RatingService : IRatingService
    {
        private readonly ElsheenaDbContext _context;

        public RatingService(ElsheenaDbContext context)
        {
            _context = context;
        }

        public async Task<RatingDto> CreateRatingAsync(Guid userId, Guid dishId, CreateRatingDto createRatingDto)
        {
            // Validate that the dish exists
            var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dishId && d.DeleteDate == null);
            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} not found");
            }

            // Validate that user has ordered this dish before
            if (!await CanUserRateDishAsync(userId, dishId))
            {
                throw new UnauthorizedAccessException("You can only rate dishes you have previously ordered");
            }

            // Check if user has already rated this dish
            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == userId && r.DishId == dishId && r.DeleteDate == null);

            if (existingRating != null)
            {
                // Update existing rating
                existingRating.Value = createRatingDto.RatingValue;
                existingRating.Text = createRatingDto.Text;
                existingRating.ModifyDateTime = DateTime.UtcNow;
            }
            else
            {
                // Create new rating
                existingRating = new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    DishId = dishId,
                    Value = createRatingDto.RatingValue,
                    Text = createRatingDto.Text,
                    CreateDateTime = DateTime.UtcNow,
                    ModifyDateTime = DateTime.UtcNow
                };
                _context.Ratings.Add(existingRating);
            }

            await _context.SaveChangesAsync();

            // Update dish average rating
            await UpdateDishAverageRatingAsync(dishId);

            // Get user info for response
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return new RatingDto
            {
                Id = existingRating.Id,
                RatingValue = existingRating.Value,
                Text = existingRating.Text,
                CreatedAt = existingRating.CreateDateTime,
                UserId = userId,
                UserFullName = user?.FullName ?? "Unknown User"
            };
        }

        public async Task<List<RatingDto>> GetDishRatingsAsync(Guid dishId)
        {
            var ratings = await _context.Ratings
                .Include(r => r.User)
                .Where(r => r.DishId == dishId && r.DeleteDate == null)
                .OrderByDescending(r => r.CreateDateTime)
                .Select(r => new RatingDto
                {
                    Id = r.Id,
                    RatingValue = r.Value,
                    Text = r.Text,
                    CreatedAt = r.CreateDateTime,
                    UserId = r.UserId,
                    UserFullName = r.User.FullName
                })
                .ToListAsync();

            return ratings;
        }

        public async Task<bool> CanUserRateDishAsync(Guid userId, Guid dishId)
        {
            // Check if user has ordered this dish in any of their completed orders
            var hasOrderedDish = await _context.DishesInCarts
                .Include(d => d.Order)
                .AnyAsync(d => d.UserId == userId 
                              && d.DishId == dishId 
                              && d.OrderId != null 
                              && d.Order != null 
                              && (d.Order.Status == Status.Delivered || d.Order.Status == Status.Packaging || d.Order.Status == Status.Delivery));

            return hasOrderedDish;
        }

        public async Task UpdateDishAverageRatingAsync(Guid dishId)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
            if (dish == null) return;

            var ratings = await _context.Ratings
                .Where(r => r.DishId == dishId && r.DeleteDate == null)
                .Select(r => r.Value)
                .ToListAsync();

            if (ratings.Any())
            {
                dish.AverageRating = (float)ratings.Average();
            }
            else
            {
                dish.AverageRating = 0;
            }

            dish.ModifyDateTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}