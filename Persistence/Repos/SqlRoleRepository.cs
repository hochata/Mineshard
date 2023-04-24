using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Repos
{
    public class SqlRoleRepository : IRoleRepository, IDisposable
    {
        private readonly RepoAnalysisContext _analysisContext;

        public SqlRoleRepository()
        {
            _analysisContext = new RepoAnalysisContext();
        }

        public Role? GetByName(string roleName)
        {
            return _analysisContext.Roles.FirstOrDefault(r => r.Name == roleName);
        }

        public void Dispose()
        {
            _analysisContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
