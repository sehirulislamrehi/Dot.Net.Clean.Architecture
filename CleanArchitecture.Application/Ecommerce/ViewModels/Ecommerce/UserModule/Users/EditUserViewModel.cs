using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.ViewModels.Ecommerce.UserModule.Users
{
    public class EditUserViewModel
    {
        public List<Role> Roles { get; set; }
        public User User { get; set; }
    }
}
