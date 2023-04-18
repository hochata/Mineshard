namespace Mineshard.Api.Models.DTO.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
