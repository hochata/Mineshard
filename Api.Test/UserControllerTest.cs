using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Mineshard.Api.Controllers;
using Mineshard.Api.Mappings;
using Mineshard.Api.Models.DTO.User;
using Mineshard.Persistence.Models.Auth;
using Mineshard.Persistence.Repos;

using Moq;

namespace Mineshard.Api.Test
{
    public class UserControllerTest
    {
        private readonly Mock<IUserRepository> mockUsers;
        private readonly Mock<IRoleRepository> mockRoles;
        private readonly Mapper mapper;
        private readonly UsersController controller;

        private readonly List<Role> roles = new List<Role>
        {
            new Role
            {
                Id = new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e"),
                Name = "Admin",
                Description = "Administrator role",
                CreatedAt = DateTime.UtcNow,
            },
            new Role
            {
                Id = new Guid("c309fa92-2123-47be-b397-a1c77adb502c"),
                Name = "Collaborator",
                Description = "Collaborator role",
                CreatedAt = DateTime.UtcNow
            }
        };

        public UserControllerTest()
        {
            this.mockUsers = new Mock<IUserRepository>();
            this.mockRoles = new Mock<IRoleRepository>();

            this.mockRoles.Setup(r => r.GetByName("Admin")).Returns(roles[0]);
            this.mockRoles.Setup(r => r.GetByName("Collaborator")).Returns(roles[1]);

            var mapConf = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfiles()));

            this.mapper = new Mapper(mapConf);
            this.controller = new UsersController(
                this.mockUsers.Object,
                this.mapper,
                this.mockRoles.Object
            );
        }

        [Fact]
        public void TestGetById()
        {
            var id = new Guid("73bc25af-20b1-49bb-ad54-b775b9ec1ae2");
            this.mockUsers.Setup(u => u.GetById(id)).Returns(UserFixtures.User);

            var result = Assert.IsType<OkObjectResult>(this.controller.GetById(id));
            var report = Assert.IsType<UserDto>(result.Value);
        }

        [Fact]
        public void TestGetAll()
        {
            this.mockUsers.Setup(r => r.GetAll()).Returns(new List<User>() { UserFixtures.User });
            var result = Assert.IsType<OkObjectResult>(this.controller.GetAll());
            var users = Assert.IsType<List<UserDto>>(result.Value);
            foreach (var u in users)
            {
                Assert.IsType<UserDto>(u);
            }
            Assert.Single(users);
        }

        [Fact]
        public void TestRegister()
        {
            mockUsers.Setup(u => u.Create(It.IsAny<User>())).Returns((User usr) => usr);
            IActionResult result = controller.Register(
                new RegisterUserRequest()
                {
                    Email = "newuser@mail.com",
                    Name = "New User",
                    Username = "NewUsername",
                    Role = "Admin"
                }
            );

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TestDelete()
        {
            var id = new Guid("73bc25af-20b1-49bb-ad54-b775b9ec1ae2");
            this.mockUsers.Setup(r => r.GetById(id)).Returns(UserFixtures.User);

            var result = Assert.IsType<OkResult>(this.controller.Delete(id));
        }

        [Fact]
        public void TestUpdate()
        {
            var id = new Guid("73bc25af-20b1-49bb-ad54-b775b9ec1ae2");
            User user = UserFixtures.User;
            this.mockUsers.Setup(u => u.GetById(id)).Returns(user);
            this.mockUsers
                .Setup(u => u.Update(user))
                .Callback(
                    () =>
                        user.Role = new Role()
                        {
                            Name = "Writer",
                            Description = "Writer",
                            Id = user.RoleId
                        }
                );

            string name = "updated name";
            string email = "new@mail.com";
            string username = "updatedUsername";
            string role = "Collaborator";

            var result = Assert.IsType<OkObjectResult>(
                controller.Update(
                    id,
                    new UpdateUserRequest()
                    {
                        Email = email,
                        Name = name,
                        Username = username,
                        Role = role
                    }
                )
            );

            var resultUser = Assert.IsType<UserDto>(result.Value);

            Assert.Equal(email, resultUser.Email);
            Assert.Equal(name, resultUser.Name);
            Assert.Equal(username, resultUser.Username);
            Assert.Equal(role, resultUser.Role);
        }
    }
}
