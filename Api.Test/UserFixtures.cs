using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Api.Test
{
    internal class UserFixtures
    {
        public static User User
        {
            get => new User()
            {
                Id = new Guid("73bc25af-20b1-49bb-ad54-b775b9ec1ae2"),
                Name = "UserFixture",
                Email = "user@mail.com",
                Username = "UsernameFixture",
                Role = new Role { Description = "Can read", Name = "Reader" }
            };
        }
    }
}
