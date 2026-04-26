using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRole(JsonElement? queryParam = null);
    }
}
