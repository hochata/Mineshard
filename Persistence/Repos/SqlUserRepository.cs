using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Repos
{
    public class SqlUserRepository : IUserRepository, IDisposable
    {
        private readonly RepoAnalysisContext _analysisContext;

        public SqlUserRepository()
        {
            _analysisContext = new RepoAnalysisContext();
        }

        public User Create(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            _analysisContext.Add(user);
            _analysisContext.SaveChanges();
            return user;
        }

        public User? Delete(User user)
        {
            _analysisContext.Remove(user);
            _analysisContext.SaveChanges();
            return user;
        }

        public List<User> GetAll()
        {
            return _analysisContext.Users.Include("Role").ToList();
        }

        public User? GetById(Guid id)
        {
            return _analysisContext.Users.Include("Role").FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string? username)
        {
            return _analysisContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? Update(User user)
        {
            User? existingUser = _analysisContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser is null)
                return null;

            existingUser.UpdatedAt = DateTime.UtcNow;
            existingUser.Username = user.Username;
            existingUser.RoleId = user.RoleId;
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            _analysisContext.SaveChanges();
            return existingUser;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
