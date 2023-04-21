using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Repos
{

    public interface IRoleRepository
    {
        Role? GetByName(string roleName);
    }
}
