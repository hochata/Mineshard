namespace Mineshard.Persistence.Models;

// TODO: Remove this stub before merging
public class Repo
{
    public Guid Id { get; set; }
}

public class Report
{
    public enum ReportStatus
    {
        Pending,
        Failed,
        Ready
    }

    public Guid Id { get; set; }
    public Guid RepoId { get; set; }
    public Repo? Repo { get; set; }
    public int NumCommitsOnMain { get; set; }
    public ReportStatus status { get; set; }
}
