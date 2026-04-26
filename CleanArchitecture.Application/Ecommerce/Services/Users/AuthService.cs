using Azure;
using BCrypt.Net;
using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.DTOs.Users;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.Services.Users
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        public AuthService(
            IUserRepository userRepository    
        ) { 
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<User>> HandleLogin(LoginRequestDTO loginRequestDTO, HttpContext httpContext)
        {
            var queryParam = JsonSerializer.SerializeToElement(new
            {
                Username = loginRequestDTO.Username
            });
            var userQuery = await _userRepository.GetUserData(queryParam);
            var user = userQuery.FirstOrDefault();


            if (user == null)
            {
                return new ApiResponse<User>
                {
                    HttpCode = 404,
                    Status = false,
                    Message = "User not found",
                    Values = null
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password))
            {
                return new ApiResponse<User>
                {
                    HttpCode = 401,
                    Status = false,
                    Message = "Invalid password"
                };
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            return new ApiResponse<User>
            {
                HttpCode = 200,
                Status = true,
                Message = "Login Success",
                Values = user
            };
        }

    }
}
