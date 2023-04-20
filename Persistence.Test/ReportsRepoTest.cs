using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Mineshard.Persistence.Context;
using Mineshard.Persistence.Repos;

namespace Mineshard.Persistence.Test;

public class ReportsRepoTest
{
    private readonly SqliteConnection conn;
    private readonly RepoAnalysisContext context;
    private readonly ReportsDbRepo repo;

    public ReportsRepoTest()
    {
        this.conn = new SqliteConnection("Filename=:memory:");
        this.conn.Open();

        var opts = new DbContextOptionsBuilder<RepoAnalysisContext>().UseSqlite(this.conn).Options;
        this.context = new RepoAnalysisContext(opts);

        this.context.Database.Migrate();
        if (this.context.Reports != null)
        {
            this.context.Reports.Add(ReportFixtures.SampleReport);
            this.context.SaveChanges();
        }

        this.repo = new ReportsDbRepo(this.context);
    }

    [Fact]
    public void GetAllTest()
    {
        var reports = this.repo.GetAll();
        Assert.Single(reports);
    }

    [Fact]
    public void GetOneRealTest()
    {
        var id = ReportFixtures.SampleReport.Id;
        var report = this.repo.GetOne(id);
        Assert.NotNull(report);
        Assert.Equal(id, report.Id);
    }

    [Fact]
    public void GetOneNonExistentTest()
    {
        var report = this.repo.GetOne(new Guid());
        Assert.Null(report);
    }
}
