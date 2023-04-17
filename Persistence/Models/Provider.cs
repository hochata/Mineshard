using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineshard.Persistence.Models
{
    public class Provider
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public required string ProviderUrl { get; set; }
    }
}
