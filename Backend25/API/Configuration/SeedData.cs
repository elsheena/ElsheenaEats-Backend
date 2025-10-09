using Core.Models;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration
{
    public static class SeedData
    {
        public static async Task SeedDishesAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ElsheenaDbContext>();

            if (!await context.Dishes.AnyAsync())
            {
                var dishes = new List<Dish>
                {
                    new Dish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Margherita Pizza",
                        Description = "Classic pizza with fresh tomato sauce, mozzarella cheese, and fresh basil leaves",
                        Price = 12.99,
                        Image = "https://example.com/margherita.jpg",
                        IsVegetarian = true,
                        AverageRating = 4.5f,
                        Category = Category.Pizza,
                        CreateDateTime = DateTime.UtcNow,
                        ModifyDateTime = DateTime.UtcNow
                    },
                    new Dish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Chicken Wok",
                        Description = "Delicious chicken wok with vegetables and special sauce",
                        Price = 10.99,
                        Image = "https://example.com/chicken-wok.jpg",
                        IsVegetarian = false,
                        AverageRating = 4.2f,
                        Category = Category.Wok,
                        CreateDateTime = DateTime.UtcNow,
                        ModifyDateTime = DateTime.UtcNow
                    },
                    new Dish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vegetable Soup",
                        Description = "Fresh vegetable soup with seasonal ingredients",
                        Price = 8.99,
                        Image = "https://example.com/vegetable-soup.jpg",
                        IsVegetarian = true,
                        AverageRating = 4.0f,
                        Category = Category.Soup,
                        CreateDateTime = DateTime.UtcNow,
                        ModifyDateTime = DateTime.UtcNow
                    },
                    new Dish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Chocolate Cake",
                        Description = "Rich chocolate cake with chocolate frosting and fresh berries",
                        Price = 6.99,
                        Image = "https://example.com/chocolate-cake.jpg",
                        IsVegetarian = true,
                        AverageRating = 4.8f,
                        Category = Category.Dessert,
                        CreateDateTime = DateTime.UtcNow,
                        ModifyDateTime = DateTime.UtcNow
                    },
                    new Dish
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fresh Orange Juice",
                        Description = "Freshly squeezed orange juice, no additives",
                        Price = 3.99,
                        Image = "https://example.com/orange-juice.jpg",
                        IsVegetarian = true,
                        AverageRating = 4.3f,
                        Category = Category.Drink,
                        CreateDateTime = DateTime.UtcNow,
                        ModifyDateTime = DateTime.UtcNow
                    }
                };

                context.Dishes.AddRange(dishes);
                await context.SaveChangesAsync();
            }
        }
    }
}