using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Ecommerce.Entities.Common
{
    public class SubModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public int Position { get; set; }
        public string Route { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
