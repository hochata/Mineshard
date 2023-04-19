using Mineshard.Api.Models.DTO.Role;

namespace Mineshard.Api.Models.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public RoleDto? Role { get; set; }
    }
}
