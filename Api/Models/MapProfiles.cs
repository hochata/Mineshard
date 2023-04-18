using System.Globalization;

using AutoMapper;

using Mineshard.Api.Models.Reports;
using Mineshard.Persistence.Models;

namespace Mineshard.Api.Models;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<Branch, string>().ConvertUsing(branch => branch.Name);
        CreateMap<Report.ReportStatus, string>().ConvertUsing(status => status.ToString());
        CreateMap<Report, MinimalReport>()
            .ForMember(r => r.Url, opts => opts.MapFrom((r, _) => r.Repository.Provider.Url))
            .ForMember(r => r.UserName, opts => opts.MapFrom((r, _) => r.Repository.Provider.Name));

        CreateMap<Report, FullReport>()
            .ForMember(view => view.Committers, opts => opts.MapFrom(GetCommitters))
            .ForMember(view => view.CommitsPerMonth, opts => opts.MapFrom(GetMonthlyCommits))
            .ForMember(
                view => view.RepositoryName,
                opts => opts.MapFrom((r, _) => r.Repository.Name)
            )
            .ForMember(view => view.Url, opts => opts.MapFrom((r, _) => r.Repository.Provider.Url))
            .ForMember(
                view => view.UserName,
                opts => opts.MapFrom((r, _) => r.Repository.Provider.Name)
            );
    }

    private Dictionary<string, int> GetCommitters(Report r, FullReport v)
    {
        var res = new Dictionary<string, int>();
        var committers = r.Committers == null ? new List<Committer>() : r.Committers;
        foreach (var c in committers)
        {
            res[c.Name] = c.NumCommits;
        }
        return res;
    }

    private Dictionary<string, int> GetMonthlyCommits(Report r, FullReport v)
    {
        var res = new Dictionary<string, int>();
        var commits = r.CommitsPerMonth == null ? new List<MonthlyLoad>() : r.CommitsPerMonth;
        foreach (var m in commits)
        {
            var month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(m.Month);
            res[month] = m.NumCommits;
        }
        return res;
    }
}
