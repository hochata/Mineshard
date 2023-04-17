namespace Mineshard.Persistence.Models;

public class MonthlyLoad
{
    public Guid Id { get; set; }
    public int Month { get; set; }
    public int NumCommits { get; set; }
    public Guid ReportId { get; set; }
    public Report? Report { get; set; }
}
