using System.Text.Json.Serialization;

namespace Mineshard.Api.Models.Reports;

[JsonDerivedType(typeof(FullReport))]
public class BaseReport
{
    public required Guid Id { get; set; }
    public required string RepositoryName { get; set; }
    public required string UserName { get; set; }
    public required string Url { get; set; }
    public required string Status { get; set; }
}
