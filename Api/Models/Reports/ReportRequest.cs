using System.ComponentModel.DataAnnotations;

namespace Mineshard.Api;

public class ReportRequest
{
    [Required]
    public required Guid UserId { get; set; }

    [Required]
    public required string Provider { get; set; }

    [Required]
    public required string ProviderUser { get; set; }

    [Required]
    public required string RepositoryName { get; set; }

    [Required]
    public required string Token { get; set; }
}
