using Mineshard.Analyzer.Core;

namespace Mineshard.Analyzer.Test;

public class AnalysisTest
{
    private const string testUrl = "https://github.com/hochata/Mineshard";
    private const string failedTestUrl = "https://github.com/hochata/Mine";
    private readonly Analysis failedAnalysis;
    private readonly Analysis analysis;

    public AnalysisTest()
    {
        this.analysis = new Analysis(testUrl, "thisRepo", "someone", "some@on.en");
        this.failedAnalysis = new Analysis(failedTestUrl, "failedRepo", "other", "oth@er.es");
    }

    [Fact]
    public void TestSuccessfullAnalysis()
    {
        Assert.NotNull(this.analysis);
        Assert.Equal(Analysis.RepoStatus.Cloned, this.analysis.Status);
    }

    [Fact]
    public void TestFailedAnalysis()
    {
        Assert.NotNull(this.failedAnalysis);
        Assert.Equal(Analysis.RepoStatus.Failed, this.failedAnalysis.Status);
    }

    [Fact]
    public void TestSimpleAnalysisMetrics()
    {
        Assert.True(this.analysis.NumCommits > 0);
    }

    [Fact]
    public void TestComposedMetrics()
    {
        Assert.True(this.analysis.Committers.Count() >= 2);
        Assert.Distinct(this.analysis.Committers);
        Assert.Contains("main", this.analysis.Branches);
        Assert.NotEmpty(this.analysis.CommitsPerMonth);
    }
}
