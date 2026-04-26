using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.User
{
    public class EditUserRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }

    }
}
