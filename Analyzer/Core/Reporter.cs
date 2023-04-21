﻿using Mineshard.Persistence.Models;

using Mineshard.Persistence.Repos;

namespace Mineshard.Analyzer.Core;

public class Reporter
{
    private readonly IReportsRepo repo;

    public Reporter(IReportsRepo repo)
    {
        this.repo = repo;
    }

    public void Analyze(Guid reportId, string url)
    {
        var report = this.repo.GetOne(reportId);
        if (report != null)
        {
            var analysis = new Analysis(url);
            var results = CopyMinimal(report);
            if (analysis.Status == Analysis.RepoStatus.Cloned)
            {
                TranscribeAnalysis(results, analysis);
            }
            else
            {
                results.Status = Report.ReportStatus.Failed;
            }
            this.repo.Update(report, results);
        }
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