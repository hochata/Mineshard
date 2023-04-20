using Microsoft.EntityFrameworkCore;

using Mineshard.Persistence.Context;

using Mineshard.Persistence.Models;

namespace Mineshard.Persistence.Repos;

public sealed class ReportsDbRepo : IReportsRepo, IDisposable
{
    private readonly RepoAnalysisContext context;

    public ReportsDbRepo(DbContextOptions<RepoAnalysisContext> opts)
    {
        this.context = new RepoAnalysisContext(opts);
    }

    public ReportsDbRepo(RepoAnalysisContext context)
    {
        this.context = context;
    }

    public ReportsDbRepo()
    {
        this.context = new RepoAnalysisContext();
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

    public void Dispose()
    {
        this.context.Dispose();
    }

    public void Update(Report source, Report dest)
    {
        dest.Status = source.Status;
        dest.Branches = source.Branches;
        dest.Committers = source.Committers;
        dest.CommitsPerMonth = source.CommitsPerMonth;

        this.context.SaveChanges();
    }
}
