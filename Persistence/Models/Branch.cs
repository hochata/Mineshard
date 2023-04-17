namespace Mineshard.Persistence.Models;

public class Branch
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid ReportId { get; set; }
    public Report? Report { get; set; }
}
