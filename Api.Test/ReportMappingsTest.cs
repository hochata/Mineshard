using System.Globalization;

using AutoMapper;

using Mineshard.Api.Mappings;
using Mineshard.Api.Models.Reports;

using Mineshard.Persistence.Models;

namespace Mineshard.Api.Test;

public class ReportMappingsTest
{
    private readonly IMapper mapper;

    public ReportMappingsTest()
    {
        var mapConf = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfiles()));

        this.mapper = new Mapper(mapConf);
    }

    [Fact]
    public void TestMinimalReportMapping()
    {
        var report = ReportFixtures.SamplePendindReport;
        var reportView = this.mapper.Map<Report, MinimalReport>(report);

        var expected = new MinimalReport
        {
            Id = report.Id,
            Status = "Pending",
            RepositoryName = "Mineshard",
            UserName = "hochata",
            Url = "https://github.com/hochata/Mineshard"
        };

        Assert.Equal(expected.Id, reportView.Id);
        Assert.Equal(expected.Status, reportView.Status);
        Assert.Equal(expected.RepositoryName, reportView.RepositoryName);
        Assert.Equal(expected.UserName, reportView.UserName);
        Assert.Equal(expected.Url, reportView.Url);
    }

    [Fact]
    public void TestFullReportMapping()
    {
        var report = ReportFixtures.SampleReport;
        var reportView = this.mapper.Map<Report, FullReport>(report);
        var expected = new FullReport
        {
            Id = report.Id,
            Branches = new List<string> { "main" },
            CommitsPerMonth = new Dictionary<string, int>
            {
                { ToMonth(1), 5 },
                { ToMonth(2), 5 },
                { ToMonth(3), 5 },
                { ToMonth(4), 5 },
                { ToMonth(5), 5 },
                { ToMonth(6), 5 },
                { ToMonth(7), 5 },
                { ToMonth(8), 5 },
                { ToMonth(9), 5 },
                { ToMonth(10), 5 },
                { ToMonth(11), 5 },
                { ToMonth(12), 5 }
            },
            Status = "Ready",
            RepositoryName = "Mineshard",
            NumCommitsOnMain = 60,
            UserName = "hochata",
            Url = "https://github.com/hochata/Mineshard",
            Committers = new Dictionary<string, int> { { "bob", 30 }, { "alice", 30 } }
        };
        Assert.Equal(expected.Id, reportView.Id);
        Assert.Equal(expected.Status, reportView.Status);
        Assert.Equal(expected.RepositoryName, reportView.RepositoryName);
        Assert.Equal(expected.UserName, reportView.UserName);
        Assert.Equal(expected.Url, reportView.Url);
        Assert.Equal(expected.Branches, reportView.Branches);
        Assert.Equal(expected.CommitsPerMonth, reportView.CommitsPerMonth);
        Assert.Equal(expected.Committers, reportView.Committers);
        Assert.Equal(expected.NumCommitsOnMain, reportView.NumCommitsOnMain);
    }

    private static string ToMonth(int month) =>
        DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
}
