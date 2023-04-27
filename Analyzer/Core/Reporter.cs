using Mineshard.Persistence.Models;

namespace Mineshard.Analyzer.Core;

public class Reporter
{
    private readonly string mergeUser;
    private readonly string mergeEmail;

    public Reporter(IConfiguration config)
    {
        this.mergeUser =
            config.GetValue<string>("GitUser:Name")
            ?? throw new ArgumentNullException(
                nameof(config),
                "Missing ExchangeName configuration"
            );
        this.mergeEmail =
            config.GetValue<string>("GitUser:Email")
            ?? throw new ArgumentNullException(
                nameof(config),
                "Missing ExchangeName configuration"
            );
    }

    public Task<Report> AnalyzeAsync(Report report) => Task.Run(() => Analyze(report));

    public Report Analyze(Report report)
    {
        var repo = report.Repository;
        var analysis = new Analysis(
            repo.BuildUrl(repo.ProviderUsername, repo.Name),
            repo.Name,
            this.mergeUser,
            this.mergeEmail
        );
        var results = CopyMinimal(report);
        if (analysis.Status == Analysis.RepoStatus.Cloned)
            TranscribeAnalysis(results, analysis);
        else
            results.Status = Report.ReportStatus.Failed;
        return results;
    }

    private static void TranscribeAnalysis(Report results, Analysis analysis)
    {
        results.NumCommitsOnMain = analysis.NumCommits;
        var branches = new List<Branch>();
        foreach (var b in analysis.Branches)
        {
            branches.Add(new Branch { Name = b, ReportId = results.Id });
        }
        results.Branches = branches;

        var committers = new List<Committer>();
        foreach (var c in analysis.Committers)
        {
            committers.Add(
                new Committer
                {
                    Name = c.Name,
                    NumCommits = c.CommitCount,
                    ReportId = results.Id
                }
            );
        }
        results.Committers = committers;

        var months = new List<MonthlyLoad>();
        foreach (var m in analysis.CommitsPerMonth)
        {
            months.Add(
                new MonthlyLoad
                {
                    Month = m.Code,
                    NumCommits = m.NumCommits,
                    ReportId = results.Id
                }
            );
        }
        results.CommitsPerMonth = months;
        results.Status = Report.ReportStatus.Ready;
    }

    private static Report CopyMinimal(Report r)
    {
        return new Report
        {
            Id = r.Id,
            Repository = r.Repository,
            RepositoryId = r.RepositoryId,
            Status = r.Status
        };
    }
}
