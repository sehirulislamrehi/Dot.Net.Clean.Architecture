using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Roles
{
    public class EditRoleViewModel
    {
        public List<Module> Modules { get; set; }
        public Role Role { get; set; }
    }
}
