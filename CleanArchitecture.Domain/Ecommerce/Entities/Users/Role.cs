using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Ecommerce.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
