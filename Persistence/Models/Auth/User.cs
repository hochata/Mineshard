namespace Mineshard.Persistence.Models.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
