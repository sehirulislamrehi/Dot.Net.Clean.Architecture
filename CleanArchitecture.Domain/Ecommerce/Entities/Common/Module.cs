using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Ecommerce.Entities.Common
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Icon { get; set; }
        public int Position { get; set; }
        public string? Route { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public ICollection<SubModule> SubModules {  get; set; } = new List<SubModule>();
        public ICollection<Permission> Permissions {  get; set; } = new List<Permission>();
    }
}
