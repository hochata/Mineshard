namespace Mineshard.Persistence.Models;

public class Committer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int NumCommits { get; set; }
    public Guid ReportId { get; set; }
    public Report? Report { get; set; }
}
