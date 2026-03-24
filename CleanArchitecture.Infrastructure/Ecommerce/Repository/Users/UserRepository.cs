using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.Users;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Ecommerce.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private ApplicationEFCoreDbContext _dbContext;
        public UserRepository(
            ApplicationEFCoreDbContext dbContext
        ) { 
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUserData(JsonElement queryParam)
        {
            var query = _dbContext.User.AsQueryable();

            if (queryParam.TryGetProperty("Username", out var username))
            {
                query = query.Where(u => u.Username == username.GetString());
            }

            if (queryParam.TryGetProperty("Email", out var email))
            {
                query = query.Where(u => u.Email == email.GetString());
            }

            return query;

        }

    }
}
