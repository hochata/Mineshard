using System.ComponentModel.DataAnnotations;

using Mineshard.Api.Models.DTO.Role;

namespace Mineshard.Api.Models.DTO.User
{
    public class UpdateUserRequest
    {
        [Required]
        [MaxLength(60)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(60)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        public required string Role { get; set; }
    }
}
