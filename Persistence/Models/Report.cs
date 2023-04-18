namespace Mineshard.Persistence.Models;

public class Report
{
    public enum ReportStatus
    {
        Pending,
        Failed,
        Ready
    }

    public required Guid Id { get; set; }
    public required Guid RepositoryId { get; set; }
    public required Repository Repository { get; set; }
    public int NumCommitsOnMain { get; set; }
    public required ReportStatus Status { get; set; }
    public required IEnumerable<Branch> Branches { get; set; }
    public required IEnumerable<Committer> Committers { get; set; }
    public required IEnumerable<MonthlyLoad> CommitsPerMonth { get; set; }
}
