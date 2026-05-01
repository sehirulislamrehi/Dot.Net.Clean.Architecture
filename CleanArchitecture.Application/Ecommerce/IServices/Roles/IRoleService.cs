using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.Role;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Roles;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.IServices.Roles
{
    public interface IRoleService
    {
        Task<DatatableResponse<IEnumerable<Role>>> GetRoleData(DataTableRequest request);
        Task<ApiResponse<CreateRoleViewModel>> HandleCreateRoleModalLogic();
        Task<ApiResponse<Role>> CreateRole(CreateRoleRequest request);
        Task<ApiResponse<EditRoleViewModel>> HandleEditRoleModalLogic(int id);
    }
}
