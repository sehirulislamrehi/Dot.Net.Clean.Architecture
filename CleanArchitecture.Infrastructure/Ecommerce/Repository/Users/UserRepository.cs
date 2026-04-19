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

        public async Task<IEnumerable<User>> GetUserData(JsonElement? queryParam = null)
        {
            var query = _dbContext.User.AsQueryable();

            if (queryParam.HasValue)
            {
                var param = queryParam.Value;

                if (param.TryGetProperty("Username", out var username))
                {
                    var usernameValue = username.GetString();

                    if (!string.IsNullOrEmpty(usernameValue))
                    {
                        query = query.Where(u =>
                            u.Username == usernameValue);
                    }
                }

                if (param.TryGetProperty("Email", out var email))
                {
                    var emailValue = email.GetString();

                    if (!string.IsNullOrEmpty(emailValue))
                    {
                        query = query.Where(u =>
                            u.Email == emailValue);
                    }
                }
            }

            return query;

        }

    }
}
