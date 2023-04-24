using Microsoft.Extensions.Configuration;

using Mineshard.Analyzer.Core;
using Mineshard.Persistence.Models;

namespace Mineshard.Analyzer.Test;

public class ReporterTest
{
    private readonly Report report;
    private readonly Reporter reporter;
    private readonly Report failedReport;

    public ReporterTest()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    { "GitUser:Name", "someone" },
                    { "GitUser:Email", "some@one.en" }
                }
            )
            .Build();

        this.reporter = new Reporter(config);
        this.report = this.reporter.Analyze(ReportFixtures.SamplePendingReport);
        this.failedReport = this.reporter.Analyze(ReportFixtures.SampleFailedReport);
    }

    [Fact]
    public void TestSuccessfullReport()
    {
        Assert.NotNull(this.report);
        Assert.Equal(Report.ReportStatus.Ready, this.report.Status);
    }

    [Fact]
    public void TestFailedReport()
    {
        Assert.NotNull(this.failedReport);
        Assert.Equal(Report.ReportStatus.Failed, this.failedReport.Status);
        Assert.Null(this.failedReport.Branches);
        Assert.Null(this.failedReport.Committers);
        Assert.Null(this.failedReport.CommitsPerMonth);
        Assert.Null(this.failedReport.NumCommitsOnMain);
    }

    [Fact]
    public void TestSimpleFields()
    {
        Assert.NotNull(this.report.Branches);
        Assert.True(this.report.Branches.Count() > 0);
    }

    [Fact]
    public void TestCompoundFields()
    {
        Assert.NotNull(this.report.CommitsPerMonth);
        Assert.NotEmpty(this.report.CommitsPerMonth);
        Assert.NotNull(this.report.Committers);
        Assert.NotEmpty(this.report.Committers);
    }

    [Fact]
    public async Task TestAsyncReport()
    {
        var otherReport = await this.reporter.AnalyzeAsync(ReportFixtures.SampleReport);
        Assert.NotNull(otherReport);
    }
}
