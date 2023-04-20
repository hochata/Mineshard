using System.ComponentModel.DataAnnotations;

namespace Mineshard.Api.Models.DTO.User
{
    public class RegisterUserRequest
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(24)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
