namespace Mineshard.Api.Models.Reports;

public class MinimalReport : ReportView
{
    public required Guid Id { get; set; }
    public required string RepositoryName { get; set; }
    public required string UserName { get; set; }
    public required string Url { get; set; }
    public required string Status { get; set; }
}
