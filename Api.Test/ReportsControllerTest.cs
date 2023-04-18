using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Mineshard.Api.Controllers;
using Mineshard.Api.Models;
using Mineshard.Api.Models.Reports;

using Mineshard.Persistence.Models;

using Mineshard.Persistence.Repos;

using Moq;

namespace Mineshard.Api.Test;

public class ReportsControllerTest
{
    private readonly ReportsController controller;
    private readonly Mock<IReportsRepo> mockRepo;
    private readonly IMapper mapper;

    public ReportsControllerTest()
    {
        this.mockRepo = new Mock<IReportsRepo>();
        var mapConf = new MapperConfiguration(c => c.AddProfile(new MapProfiles()));

        this.mapper = new Mapper(mapConf);
        this.controller = new ReportsController(this.mockRepo.Object, this.mapper);
    }

    [Fact]
    public void TestGetOne()
    {
        var id = Guid.Parse("0da92302-a07f-495c-8a56-a56c31629d0b");
        this.mockRepo.Setup(r => r.GetOne(id)).Returns(ReportFixtures.SampleReport);

        var result = Assert.IsType<OkObjectResult>(this.controller.GetOne(id));
        var report = Assert.IsType<FullReport>(result.Value);
        Assert.Equal("Ready", report.Status);
    }

    [Fact]
    public void TestGetOneNonExistent()
    {
        var id = new Guid();
        this.mockRepo.Setup(r => r.GetOne(id)).Returns((Report?)null);
        Assert.IsType<NotFoundResult>(this.controller.GetOne(id));
    }

    [Fact]
    public void TestGetOnePending()
    {
        var id = Guid.Parse("3e51a6b3-e899-4831-95d7-571070f80cc4");
        this.mockRepo.Setup(r => r.GetOne(id)).Returns(ReportFixtures.SamplePendindReport);

        var result = Assert.IsType<OkObjectResult>(this.controller.GetOne(id));
        var report = Assert.IsType<MinimalReport>(result.Value);
        Assert.Equal("Pending", report.Status);
    }

    [Fact]
    public void TestGetAll()
    {
        this.mockRepo
            .Setup(r => r.GetAll())
            .Returns(new List<Report>() { ReportFixtures.SampleReport });

        var result = Assert.IsType<OkObjectResult>(this.controller.GetAll());
        var reports = Assert.IsType<List<ReportView>>(result.Value);
        foreach (var r in reports)
        {
            Assert.IsType<FullReport>(r);
        }
        Assert.Single(reports);
    }
}
