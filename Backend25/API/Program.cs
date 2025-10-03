
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using DataAccess.DBContext;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ElsheenaDbContext>(x =>
            x.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ElsheenaDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, Role, ElsheenaDbContext, Guid>>() // Identity Configuration
            .AddRoleStore<RoleStore<Role, ElsheenaDbContext, Guid>>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
