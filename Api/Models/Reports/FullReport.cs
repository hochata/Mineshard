namespace Mineshard.Api.Models.Reports;

public class FullReport : BaseReport
{
    public int NumCommitsOnMain { get; set; }
    public required List<string> Branches { get; set; }
    public required Dictionary<string, int> Committers { get; set; }
    public required Dictionary<string, int> CommitsPerMonth { get; set; }
}
