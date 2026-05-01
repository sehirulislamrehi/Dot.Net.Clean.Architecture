using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Users;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Ecommerce.Repository.UserModule.Users
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
            var query = _dbContext.Users
                .Include(u => u.Role)
                .AsQueryable();

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

                if (param.TryGetProperty("Id", out var id))
                {
                    var idValue = id.GetInt32();
                        query = query.Where(u => u.Id == idValue);
                }
                if (param.TryGetProperty("IdNotEqual", out var idNotEqual))
                {
                    var idValue = idNotEqual.GetInt32();
                    query = query.Where(u => u.Id != idValue);
                }
            }

            return query.OrderByDescending(u => u.Id);
        }

        public async Task<User> SaveUser(User user)
        {
            if(user.Id == 0)
            {
                await _dbContext.Users.AddAsync(user);
            }
            await _dbContext.SaveChangesAsync();
            return user;
        }

    }
}
