using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Models
{
    public class Repository
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public required string ProviderUsername { get; set; }

        public Guid ProviderId { get; set; }
        public Guid RequestorId { get; set; }

        public required User Requestor { get; set; }
        public required Provider Provider { get; set; }
        public required ICollection<Report> Reports { get; set; }
    }
}
