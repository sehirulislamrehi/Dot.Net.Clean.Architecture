using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.Role
{
    public class CreateRoleRequest
    {
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "At least one permission must be selected")]
        public List<int> PermissionIds { get; set; } = new();
    }
}
