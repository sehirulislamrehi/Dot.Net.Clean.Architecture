using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.Role;
using CleanArchitecture.Application.Ecommerce.IServices.Roles;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Roles;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.CommonModule;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.Ecommerce.Services.Roles
{
    public class RoleService : IRoleService
    {

        private IRoleRepository _roleRepository;
        private IModuleRepository _moduleRepository;
        public RoleService(IRoleRepository roleRepository, IModuleRepository moduleRepository)
        {
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<DatatableResponse<IEnumerable<Role>>> GetRoleData(DataTableRequest request)
        {
            var obj = new
            {
                Name = request.Search ?? ""
            };

            var json = JsonSerializer.Serialize(obj);
            var queryParam = JsonSerializer.Deserialize<JsonElement>(json);

            var query = await _roleRepository.GetAllRole(queryParam);
            var total = query.Count();
            var data = query
                .Skip(request.Start)
                .Take(request.Length)
                .ToList();

            return new DatatableResponse<IEnumerable<Role>>
            {
                Draw = request.Draw,
                RecordsTotal = total,
                RecordsFiltered = total,
                Data = data,
            };
        }

        public async Task<ApiResponse<CreateRoleViewModel>> HandleCreateRoleModalLogic()
        {
            var modules = await _moduleRepository.GetModules();

            var response = new CreateRoleViewModel
            {
                Modules = modules.ToList(),
            };

            return new ApiResponse<CreateRoleViewModel>
            {
                Status = true,
                HttpCode = 200,
                Message = "OK",
                Values = response
            };
        }

        public async Task<ApiResponse<Role>> CreateRole(CreateRoleRequest request)
        {
            var obj = new
            {
                Name = request.Name ?? ""
            };

            var json = JsonSerializer.Serialize(obj);
            var queryParam = JsonSerializer.Deserialize<JsonElement>(json);
            var roleExistQuery = await _roleRepository.GetAllRole(queryParam);
            var roleExists = roleExistQuery.FirstOrDefault();

            if (roleExists != null)
            {
                return new ApiResponse<Role>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "Role already exists"
                };
            }

            var role = new Role
            {
                Name = request.Name,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RolePermissions = request.PermissionIds.Select(id => new RolePermission
                {
                    PermissionId = id,
                }).ToList()
            };

            var result = await _roleRepository.CreateRole(role);

            return new ApiResponse<Role>
            {
                Status = true,
                HttpCode = 200,
                Message = $"New {role.Name} role created"
            };
        }


        public async Task<ApiResponse<EditRoleViewModel>> HandleEditRoleModalLogic(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return new ApiResponse<EditRoleViewModel>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "No role found"
                };
            }
            var modules = await _moduleRepository.GetModules();

            var datas = new EditRoleViewModel
            {
                Role = role,
                Modules = modules.ToList()
            };
            return new ApiResponse<EditRoleViewModel>
            {
                Status = true,
                HttpCode = 200,
                Message = "Response ok",
                Values = datas
            };
        }

        public async Task<ApiResponse<Role>> EditRole(CreateRoleRequest request, int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return new ApiResponse<Role>
                {
                    Status = false,
                    HttpCode = 200,
                    Message = "No role found"
                };
            }

            role.Name = request.Name;
            role.IsActive = request.IsActive == 1 ? true : false;

            var result = await _roleRepository.EditRole(role, request.PermissionIds);

            return new ApiResponse<Role>
            {
                Status = true,
                HttpCode = 200,
                Message = $"{role.Name} role updated"
            };
        }
    }
}
