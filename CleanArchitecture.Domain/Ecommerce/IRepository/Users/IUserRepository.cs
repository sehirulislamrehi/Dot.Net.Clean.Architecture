using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Ecommerce.IRepository.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserData(JsonElement queryParam);
    }
}
