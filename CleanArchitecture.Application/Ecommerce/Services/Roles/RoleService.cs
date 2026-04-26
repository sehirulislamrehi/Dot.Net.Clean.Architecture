using CleanArchitecture.Application.Ecommerce.IServices.Roles;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Roles;
using CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.Ecommerce.Services.Roles
{
    public class RoleService : IRoleService
    {

        private IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<DatatableResponse<IEnumerable<Role>>> GetRoleData(DataTableRequest request)
        {
            var query = await _roleRepository.GetAllRole();
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

        //public async Task<ApiResponse<CreateUserViewModel>> HandleCreateRoleModalLogic()
        //{
        //    return new ApiResponse<CreateRoleViewModel>
        //    {
        //        Status = true,
        //        HttpCode = 200,
        //        Message = "OK",
        //        Values = 
        //    };
        //}
    }
}
