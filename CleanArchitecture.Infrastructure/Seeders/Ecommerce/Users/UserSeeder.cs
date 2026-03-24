using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;

namespace CleanArchitecture.Infrastructure.Seeders.Ecommerce.Users
{
    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationEFCoreDbContext context)
        {

            var users = new List<User>();
            var user = new User
            {
                Username = "376395",
                FullName = "MD Sehirul Islam Rehi",
                Email = "mdsehirulislamrehi@gmail.com",
                Phone = "01858361812",
                RoleId = null,
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            users.Add(user);

            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
