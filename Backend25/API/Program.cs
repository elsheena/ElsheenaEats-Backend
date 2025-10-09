using System.Security.Claims;
using System.Text;
using API.Configuration;
using BusinessLogicLayer.Configuration;
using BusinessLogicLayer.Constants;
using BusinessLogicLayer.Services;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ElsheenaDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            // Register services
            builder.Services.AddScoped<IDishService, DishService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ElsheenaDbContext>();

            // JWT configuration
            var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
            builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);

            var jwtConfiguration = jwtSection.Get<JwtBearerTokenSettings>();

            if (jwtConfiguration?.SecretKey != null)
            {
                var key = Encoding.ASCII.GetBytes(jwtConfiguration.SecretKey);

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = jwtConfiguration.Audience,
                        ValidIssuer = jwtConfiguration.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true
                    };
                });

                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy(ApplicationRoleNames.Administrator,
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .RequireRole(ApplicationRoleNames.Administrator)
                            .RequireClaim(ClaimTypes.Role, ApplicationRoleNames.Administrator).Build());

                    options.AddPolicy(ApplicationRoleNames.Manager,
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .RequireRole(ApplicationRoleNames.Manager, ApplicationRoleNames.Administrator)
                            .Build());

                    options.AddPolicy(ApplicationRoleNames.Cook,
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .RequireRole(ApplicationRoleNames.Cook, ApplicationRoleNames.Manager, ApplicationRoleNames.Administrator)
                            .Build());
                });
            }

            var app = builder.Build();

            try
            {
                using var serviceScope = app.Services.CreateScope();
                var context = serviceScope.ServiceProvider.GetService<ElsheenaDbContext>();
                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.ConfigureIdentityAsync();
            await app.SeedDishesAsync();
            app.Run();
        }
    }
}
