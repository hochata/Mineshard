using Mineshard.Analyzer.Core;

using Mineshard.Persistence.Repos;

namespace Mineshard.Analyzer.Controllers;

public class ReportsController : IReportsController
{
    private readonly IReportsRepo repo;
    private readonly Reporter reporter;

    public ReportsController(IReportsRepo repo, Reporter reporter)
    {
        this.repo = repo;
        this.reporter = reporter;
    }

    public Task RunAnalysisAync(Guid id) => Task.Run(() => this.RunAnalysis(id));

    public void RunAnalysis(Guid id)
    {
        var report = this.repo.GetById(id);
        if (report != null)
        {
            var analysis = this.reporter.Analyze(report);
            this.repo.Update(analysis, report);
        }
    }
}
