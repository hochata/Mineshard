using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Mineshard.Api.Broker;
using Mineshard.Api.Controllers;
using Mineshard.Api.Mappings;

using Mineshard.Api.Models.Reports;

using Mineshard.Persistence.Models;

using Mineshard.Persistence.Repos;

using Moq;

namespace Mineshard.Api.Test;

public class ReportsControllerTest
{
    private readonly ReportsController controller;
    private readonly Mock<IReportsRepo> mockRepo;
    private readonly Mock<IUserRepository> mockUsers;
    private readonly Mock<IProducer> mockBroker;
    private readonly IMapper mapper;

    public ReportsControllerTest()
    {
        this.mockRepo = new Mock<IReportsRepo>();
        this.mockUsers = new Mock<IUserRepository>();
        this.mockBroker = new Mock<IProducer>();
        var mapConf = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfiles()));

        this.mapper = new Mapper(mapConf);
        this.controller = new ReportsController(
            this.mockRepo.Object,
            this.mockUsers.Object,
            this.mapper,
            this.mockBroker.Object
        );
    }

    [Fact]
    public void TestGetOne()
    {
        var id = Guid.Parse("0da92302-a07f-495c-8a56-a56c31629d0b");
        this.mockRepo.Setup(r => r.GetById(id)).Returns(ReportFixtures.SampleReport);

        var result = Assert.IsType<OkObjectResult>(this.controller.GetOne(id));
        var report = Assert.IsType<FullReport>(result.Value);
        Assert.Equal("Ready", report.Status);
    }

    [Fact]
    public void TestGetOneNonExistent()
    {
        var id = new Guid();
        this.mockRepo.Setup(r => r.GetById(id)).Returns((Report?)null);
        Assert.IsType<NotFoundResult>(this.controller.GetOne(id));
    }

    [Fact]
    public void TestGetOnePending()
    {
        var id = Guid.Parse("3e51a6b3-e899-4831-95d7-571070f80cc4");
        this.mockRepo.Setup(r => r.GetById(id)).Returns(ReportFixtures.SamplePendindReport);

        var result = Assert.IsType<OkObjectResult>(this.controller.GetOne(id));
        var report = Assert.IsType<BaseReport>(result.Value);
        Assert.Equal("Pending", report.Status);
    }

    [Fact]
    public void TestGetAll()
    {
        this.mockRepo
            .Setup(r => r.GetAll())
            .Returns(new List<Report>() { ReportFixtures.SampleReport });

        var result = Assert.IsType<OkObjectResult>(this.controller.GetAll());
        var reports = Assert.IsType<List<BaseReport>>(result.Value);
        foreach (var r in reports)
        {
            Assert.IsType<FullReport>(r);
        }
        Assert.Single(reports);
    }

    [Fact]
    public void TestRequestNewReport()
    {
        var repo = ReportFixtures.GithubRepo;
        this.mockRepo.Setup(r => r.GetRepositoryById(repo.Id)).Returns(repo);

        var redirect = Assert.IsType<CreatedAtActionResult>(
            this.controller.RequestNewAnalysis(repo.Id)
        );
        var report = Assert.IsType<BaseReport>(redirect.Value);
        this.mockRepo.Verify(x => x.Add(It.IsAny<Report>()), Times.Once());
    }

    [Fact]
    public void TestRequestReportForExistingRepo()
    {
        var repo = ReportFixtures.GithubRepo;
        var provider = ReportFixtures.GithubProvider;
        var requestor = ReportFixtures.Requestor;
        this.mockRepo.Setup(r => r.GetRepositoryById(repo.Id)).Returns(repo);
        this.mockRepo.Setup(r => r.GetProviderByName(provider.Name)).Returns(repo.Provider);
        this.mockUsers.Setup(r => r.GetById(requestor.Id)).Returns(repo.Requestor);

        var reportRequest = new ReportRequest
        {
            Provider = provider.Name,
            ProviderUser = repo.ProviderUsername,
            RepositoryName = repo.Name,
            UserId = requestor.Id,
            Token = ""
        };
        var redirect = Assert.IsType<CreatedAtActionResult>(
            this.controller.RequestAnalysis(reportRequest)
        );
        var report = Assert.IsType<BaseReport>(redirect.Value);
        this.mockRepo.Verify(x => x.Add(It.IsAny<Report>()), Times.Once());
        this.mockBroker.Verify(x => x.Send(report.Id), Times.Once());
    }

    [Fact]
    public void TestRequestReportForNewRepo()
    {
        var repo = ReportFixtures.GithubRepo;
        var provider = ReportFixtures.GithubProvider;
        var requestor = ReportFixtures.Requestor;
        var addedRepos = new List<Repository>();
        this.mockRepo
            .Setup(r => r.GetRepositoryById(It.IsAny<Guid>()))
            .Returns(addedRepos.FirstOrDefault);
        this.mockRepo
            .Setup(
                r =>
                    r.GetRepositoryByName(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()
                    )
            )
            .Returns((Repository?)null);
        this.mockRepo.Setup(r => r.GetProviderByName(provider.Name)).Returns(repo.Provider);
        this.mockRepo
            .Setup(r => r.AddRepository(It.IsAny<Repository>()))
            .Callback((Repository repo) => addedRepos.Add(repo));
        this.mockUsers.Setup(r => r.GetById(requestor.Id)).Returns(repo.Requestor);

        var reportRequest = new ReportRequest
        {
            Provider = provider.Name,
            ProviderUser = repo.ProviderUsername,
            RepositoryName = repo.Name,
            UserId = requestor.Id,
            Token = ""
        };

        var res = this.controller.RequestAnalysis(reportRequest);
        var redirect = Assert.IsType<CreatedAtActionResult>(res);

        var report = Assert.IsType<BaseReport>(redirect.Value);
        this.mockRepo.Verify(x => x.Add(It.IsAny<Report>()), Times.Once());
        this.mockRepo.Verify(x => x.AddRepository(It.IsAny<Repository>()), Times.Once());
        this.mockBroker.Verify(x => x.Send(It.IsAny<Guid>()), Times.Once());
    }
}
