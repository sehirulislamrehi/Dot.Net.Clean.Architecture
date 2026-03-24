using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.DTOs.Users;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.IServices.Users
{
    public interface IAuthService
    {
        Task<ApiResponse<User>> HandleLogin(LoginRequestDTO loginRequestDTO, HttpContext httpContext);
    }
}
