using BusinessLogicLayer.Constants;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
    public static class ConfigureIdentity
    {
        public static async Task ConfigureIdentityAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Create roles if they don't exist
            await CreateRoleIfNotExistsAsync(roleManager, ApplicationRoleNames.Administrator);
            await CreateRoleIfNotExistsAsync(roleManager, ApplicationRoleNames.Manager);
            await CreateRoleIfNotExistsAsync(roleManager, ApplicationRoleNames.Cook);
            await CreateRoleIfNotExistsAsync(roleManager, ApplicationRoleNames.Customer);

            // Create admin user if doesn't exist
            var adminEmail = "admin@elsheena.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Administrator",
                    Address = "System Address",
                    Gender = Gender.Male,
                    CreateDateTime = DateTime.UtcNow,
                    ModifyDateTime = DateTime.UtcNow,
                    EmailConfirmed = true,
                    RefreshToken = null,
                    RefreshExpiration = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, ApplicationRoleNames.Administrator);
                }
            }
        }

        private static async Task CreateRoleIfNotExistsAsync(RoleManager<Role> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };
                
                await roleManager.CreateAsync(role);
            }
        }
    }
}