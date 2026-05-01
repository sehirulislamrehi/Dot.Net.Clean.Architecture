using Azure.Core;
using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.User;
using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.Services.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor) { 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DatatableResponse<IEnumerable<User>>> GetUserData(DataTableRequest request)
        {
            var query = await _userRepository.GetUserData();
            var total = query.Count();
            var data = query
                .Skip(request.Start)
                .Take(request.Length)
                .ToList();

            return new DatatableResponse<IEnumerable<User>>
            {
                Draw = request.Draw,
                RecordsTotal = total,
                RecordsFiltered = total,
                Data = data,
            };
        }

        public async Task<ApiResponse<CreateUserViewModel>> HandleCreateUserModalLogic()
        {
            var roleQuery = await _roleRepository.GetAllRole();
            var roles = roleQuery.ToList();
            var data = new CreateUserViewModel
            {
                Roles = roles
            };

            return new ApiResponse<CreateUserViewModel>
            {
                Status = true,
                HttpCode = 200,
                Message = "OK",
                Values = data
            };
        }

        public async Task<ApiResponse<User>> CreateUser(CreateUserRequest request)
        {
            var obj = new
            {
                Username = request.Username ?? ""
            };

            var json = JsonSerializer.Serialize(obj);
            var queryParam = JsonSerializer.Deserialize<JsonElement>(json);
            var userExistQuery = await _userRepository.GetUserData(queryParam);
            var userExists = userExistQuery.FirstOrDefault();

            if (userExists != null)
            {
                return new ApiResponse<User>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "User already exists with same username"
                };
            }

            var passwordHasher = new PasswordHasher<User>();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                FullName = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Password = hashedPassword,
                RoleId = request.RoleId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsSuperAdmin = false
            };

            user = await _userRepository.SaveUser(user);

            return new ApiResponse<User>
            {
                Status = true,
                HttpCode = 200,
                Message = "New user created"
            };
        }

        public async Task<ApiResponse<EditUserViewModel>> HandleEditUserModalLogic(int id)
        {
            var roleQuery = await _roleRepository.GetAllRole();
            var roles = roleQuery.ToList();

            var obj = new
            {
                Id = id
            };

            var json = JsonSerializer.Serialize(obj);
            var queryParam = JsonSerializer.Deserialize<JsonElement>(json);
            var userExistQuery = await _userRepository.GetUserData(queryParam);
            var user = userExistQuery.FirstOrDefault();

            if (user == null)
            {
                return new ApiResponse<EditUserViewModel>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "User does not exists",
                };
            }

            var data = new EditUserViewModel
            {
                Roles = roles,
                User = user
            };

            return new ApiResponse<EditUserViewModel>
            {
                Status = true,
                HttpCode = 200,
                Message = "OK",
                Values = data
            };
        }

        public async Task<ApiResponse<User>> EditUser(EditUserRequest request, int id)
        {
            var obj = new
            {
                Username = request.Username ?? "",
                IdNotEqual = id
            };

            var json = JsonSerializer.Serialize(obj);
            var queryParam = JsonSerializer.Deserialize<JsonElement>(json);
            var userExistQuery = await _userRepository.GetUserData(queryParam);
            var userExists = userExistQuery.FirstOrDefault();

            if (userExists != null)
            {
                return new ApiResponse<User>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "User already exists with same username"
                };
            }

            var obj2 = new
            {
                Id = id
            };
            json = JsonSerializer.Serialize(obj2);
            queryParam = JsonSerializer.Deserialize<JsonElement>(json);
            userExistQuery = await _userRepository.GetUserData(queryParam);
            var user = userExistQuery.FirstOrDefault();

            if (user == null)
            {
                return new ApiResponse<User>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "No user found"
                };
            }

            user.Username = request.Username;
            user.FullName = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.RoleId = request.RoleId;
            user.UpdatedAt = DateTime.Now;

            user = await _userRepository.SaveUser(user);

            return new ApiResponse<User>
            {
                Status = true,
                HttpCode = 200,
                Message = "User updated"
            };

        }


        public async Task<User> GetCurrentAuthUser()
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;

            if (httpUser == null || !httpUser.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var queryParam = new
            {
                Id = Convert.ToInt32(userId)
            };

            var json = JsonSerializer.Serialize(queryParam);
            var param = JsonSerializer.Deserialize<JsonElement>(json);

            var userExistQuery = await _userRepository.GetUserData(param);

            return userExistQuery.FirstOrDefault();
        }
    }
}
