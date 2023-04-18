using System.Globalization;

using AutoMapper;

using Mineshard.Persistence.Models;

namespace Mineshard.Api.Models;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<Branch, string>().ConvertUsing(branch => branch.Name);

        CreateMap<Report, ReportView>()
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

    private Dictionary<string, int> GetCommitters(Report r, ReportView v)
    {
        var res = new Dictionary<string, int>();
        foreach (var c in r.Committers)
        {
            res[c.Name] = c.NumCommits;
        }
        return res;
    }

    private Dictionary<string, int> GetMonthlyCommits(Report r, ReportView v)
    {
        var res = new Dictionary<string, int>();
        foreach (var m in r.CommitsPerMonth)
        {
            var month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(m.Month);
            res[month] = m.NumCommits;
        }
        return res;
    }
}
