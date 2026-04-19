using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.IServices.Users
{
    public interface IUserService
    {
        Task<DatatableResponse<IEnumerable<User>>> GetUserData();
    }
}
