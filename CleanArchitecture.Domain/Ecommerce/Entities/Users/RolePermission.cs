using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Ecommerce.Entities.Users
{
    public class RolePermission
    {
        public int Id { get; set; } 
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
