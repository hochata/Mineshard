using Mineshard.Persistence.Models;

namespace Mineshard.Persistence.Repos;

public class ReportsDbRepo : IReportsRepo
{
    private readonly RepoAnalysisContext context;

    public ReportsDbRepo(RepoAnalysisContext context)
    {
        this.context = context;
    }

    public Report? GetOne(Guid id)
    {
        if (this.context.Reports == null)
            return null;
        var report = this.context.Reports.FirstOrDefault(x => x.Id == id);

        return report;
    }

    public List<Report> GetAll()
    {
        if (this.context.Reports == null)
            return new List<Report>();

        return this.context.Reports.ToList();
    }
}
