namespace Mineshard.Persistence.Models;

public class Report
{
    public enum ReportStatus
    {
        Pending,
        Failed,
        Ready
    }

    public Guid Id { get; set; }
    public Guid RepositoryId { get; set; }
    public Repository? Repository { get; set; }
    public int NumCommitsOnMain { get; set; }
    public ReportStatus Status { get; set; }
    public required IEnumerable<Branch> Branches { get; set; }
    public required IEnumerable<Committer> Commiters { get; set; }
    public required IEnumerable<MonthlyLoad> CommitsPerMonth { get; set; }
}
