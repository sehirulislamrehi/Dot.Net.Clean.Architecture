using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Ecommerce.Entities.Common
{
    public class Modules
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Icon { get; set; }
        public int Position { get; set; }
        public string? Route { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public ICollection<SubModules> SubModules {  get; set; } = new List<SubModules>();
        public ICollection<Permissions> Permissions {  get; set; } = new List<Permissions>();
    }
}
