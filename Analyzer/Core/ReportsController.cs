using Mineshard.Persistence.Repos;

namespace Mineshard.Analyzer.Core;

public class ReportsController
{
    private readonly IReportsRepo repo;
    private readonly Reporter reporter;

    public ReportsController(IReportsRepo repo, Reporter reporter)
    {
        this.repo = repo;
        this.reporter = reporter;
    }

    public async Task RunAnalysis(Guid id)
    {
        var report = this.repo.GetOne(id);
        if (report != null)
        {
            var analysis = await Reporter.AnalyzeAsync(report);
            this.repo.Update(analysis, report);
        }
    }
}
