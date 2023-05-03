using Microsoft.Extensions.Configuration;

using Mineshard.Analyzer.Controllers;
using Mineshard.Analyzer.Core;
using Mineshard.Persistence.Models;
using Mineshard.Persistence.Repos;

using Moq;

namespace Mineshard.Analyzer.Test;

public class ReportsControllerTest
{
    private readonly ReportsController controller;
    private readonly Reporter reporter;
    private readonly Mock<IReportsRepo> mockRepo;

    public ReportsControllerTest()
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

        this.mockRepo = new Mock<IReportsRepo>();
        this.reporter = new Reporter(config);
        this.controller = new ReportsController(this.mockRepo.Object, this.reporter);
    }

    [Fact]
    public async Task TestRunAsyncAnalyzer()
    {
        var sample = ReportFixtures.SampleReport;
        this.mockRepo.Setup(r => r.GetById(sample.Id)).Returns(sample);

        await this.controller.RunAnalysisAync(sample.Id);
        this.mockRepo.Verify(r => r.Update(It.IsAny<Report>(), It.IsAny<Report>()), Times.Once);
    }

    [Fact]
    public void TestRunAnalyzer()
    {
        var sample = ReportFixtures.SampleReport;
        this.mockRepo.Setup(r => r.GetById(sample.Id)).Returns(sample);

        this.controller.RunAnalysis(sample.Id);
        this.mockRepo.Verify(r => r.Update(It.IsAny<Report>(), It.IsAny<Report>()), Times.Once);
    }
}
