using BusinessLogicLayer.DTOs;
using Core.Common;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface IDishService
    {
        Task<PagedResult<DishListItemDto>> GetDishListAsync(DishQueryParamsDto query);
        Task<DishDetailsDto> GetDishDetailsAsync(Guid id);
        Task<DishDetailsDto> CreateDishAsync(DishDetailsDto dishDto);
        Task<DishDetailsDto> UpdateDishAsync(Guid id, DishDetailsDto dishDto);
        Task<bool> DeleteDishAsync(Guid id);
    }

    public class DishService : IDishService
    {
        private readonly ElsheenaDbContext _context;

        public DishService(ElsheenaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<DishListItemDto>> GetDishListAsync(DishQueryParamsDto query)
        {
            var dishes = _context.Dishes.Where(x => x.DeleteDate == null).AsQueryable();

            // Apply filters
            if (query.Categories != null && query.Categories.Any())
            {
                dishes = dishes.Where(x => query.Categories.Contains(x.Category));
            }

            if (query.IsVegetarian.HasValue)
            {
                dishes = dishes.Where(x => x.IsVegetarian == query.IsVegetarian.Value);
            }

            if (query.MinPrice.HasValue)
            {
                dishes = dishes.Where(x => x.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                dishes = dishes.Where(x => x.Price <= query.MaxPrice.Value);
            }

            // Apply sorting
            dishes = query.SortBy switch
            {
                SortBy.NameAsc => dishes.OrderBy(x => x.Name),
                SortBy.NameDesc => dishes.OrderByDescending(x => x.Name),
                SortBy.PriceAsc => dishes.OrderBy(x => x.Price),
                SortBy.PriceDesc => dishes.OrderByDescending(x => x.Price),
                SortBy.RatingAsc => dishes.OrderBy(x => x.AverageRating),
                SortBy.RatingDesc => dishes.OrderByDescending(x => x.AverageRating),
                _ => dishes.OrderBy(x => x.Name)
            };

            var totalCount = await dishes.CountAsync();

            var items = await dishes
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new DishListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Image = x.Image,
                    IsVegetarian = x.IsVegetarian,
                    AverageRating = x.AverageRating,
                    Category = x.Category
                }).ToListAsync();

            return new PagedResult<DishListItemDto>
            {
                Items = items,
                PageSize = query.PageSize,
                CurrentPage = query.Page,
                TotalCount = totalCount
            };
        }

        public async Task<DishDetailsDto> GetDishDetailsAsync(Guid id)
        {
            var dish = await _context.Dishes
                .Include(x => x.Rating)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(x => x.Id == id && x.DeleteDate == null);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {id} does not found in database!");
            }

            return new DishDetailsDto()
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Image = dish.Image,
                IsVegetarian = dish.IsVegetarian,
                AverageRating = dish.AverageRating,
                Category = dish.Category,
                CreateDateTime = dish.CreateDateTime,
                ModifyDateTime = dish.ModifyDateTime,
                Ratings = dish.Rating.Select(r => new RatingDto
                {
                    Id = r.Id,
                    RatingValue = r.Value,
                    Text = r.Text,
                    CreatedAt = r.CreateDateTime,
                    UserId = r.UserId,
                    UserFullName = r.User.FullName
                }).ToList()
            };
        }

        public async Task<DishDetailsDto> CreateDishAsync(DishDetailsDto dishDto)
        {
            var dish = new Dish
            {
                Id = Guid.NewGuid(),
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                Image = dishDto.Image,
                IsVegetarian = dishDto.IsVegetarian,
                Category = dishDto.Category,
                CreateDateTime = DateTime.UtcNow,
                ModifyDateTime = DateTime.UtcNow
            };

            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            dishDto.Id = dish.Id;
            dishDto.CreateDateTime = dish.CreateDateTime;
            dishDto.ModifyDateTime = dish.ModifyDateTime;

            return dishDto;
        }

        public async Task<DishDetailsDto> UpdateDishAsync(Guid id, DishDetailsDto dishDto)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id && x.DeleteDate == null);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {id} does not found in database!");
            }

            dish.Name = dishDto.Name;
            dish.Description = dishDto.Description;
            dish.Price = dishDto.Price;
            dish.Image = dishDto.Image;
            dish.IsVegetarian = dishDto.IsVegetarian;
            dish.Category = dishDto.Category;
            dish.ModifyDateTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            dishDto.Id = dish.Id;
            dishDto.CreateDateTime = dish.CreateDateTime;
            dishDto.ModifyDateTime = dish.ModifyDateTime;

            return dishDto;
        }

        public async Task<bool> DeleteDishAsync(Guid id)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id && x.DeleteDate == null);

            if (dish == null)
            {
                return false;
            }

            dish.DeleteDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}