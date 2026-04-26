using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.User;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users;
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
        Task<DatatableResponse<IEnumerable<User>>> GetUserData(DataTableRequest request);
        Task<ApiResponse<CreateUserViewModel>> HandleCreateUserModalLogic();
        Task<ApiResponse<User>> CreateUser(CreateUserRequest request);
        Task<ApiResponse<EditUserViewModel>> HandleEditUserModalLogic(int id);
        Task<ApiResponse<User>> EditUser(EditUserRequest request, int id);
        Task<User> GetCurrentAuthUser();

    }
}
