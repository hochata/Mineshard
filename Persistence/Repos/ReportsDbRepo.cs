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

    public Report? GetById(Guid id) =>
        this.context.Reports == null
            ? null
            : this.context.Reports
                .Include(r => r.Repository)
                .Include(r => r.Repository.Provider)
                .FirstOrDefault(x => x.Id == id);

    public List<Report> GetAll()
    {
        if (this.context.Reports == null)
            return new List<Report>();

        return this.context.Reports
            .Include(r => r.Repository)
            .Include(r => r.Repository.Provider)
            .ToList();
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

    public Provider? GetProviderByName(string name) =>
        this.context.Providers == null
            ? null
            : this.context.Providers.FirstOrDefault(x => x.Name == name);

    public Repository? GetRepositoryById(Guid id) =>
        this.context.Repositories == null
            ? null
            : this.context.Repositories.FirstOrDefault(x => x.Id == id);

    public Repository? GetRepositoryByName(string provider, string user, string name)
    {
        if (this.context.Repositories == null)
            return null;

        return this.context.Repositories.FirstOrDefault(
            r =>
                r.Name == name
                && r.ProviderUsername == user
                && r.Provider != null
                && r.Provider.Name == provider
        );
    }

    public bool Add(Report source)
    {
        if (context.Reports == null)
            return false;

        this.context.Reports.Add(source);
        this.context.SaveChanges();

        return true;
    }

    public bool AddRepository(Repository repo)
    {
        if (context.Repositories == null)
            return false;

        this.context.Repositories.Add(repo);
        this.context.SaveChanges();

        return true;
    }
}
