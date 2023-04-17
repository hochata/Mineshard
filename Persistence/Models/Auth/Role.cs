using System;
using System.Collections.Generic;
using System.Linq;
namespace Mineshard.Persistence.Models.Auth
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
