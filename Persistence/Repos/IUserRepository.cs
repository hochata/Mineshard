using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Repos
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetByUsername(string? username);
        User? GetById(Guid id);
        User Create(User user);
        User? Update(User user);
        User? Delete(User user);
    }
}
