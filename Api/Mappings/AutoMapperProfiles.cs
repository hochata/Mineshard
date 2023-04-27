using System.Globalization;

using AutoMapper;

using Mineshard.Api.Models.DTO.User;
using Mineshard.Api.Models.Reports;

using Mineshard.Persistence.Models;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Users Mappings
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>()
            .ForMember(
                u => u.Role,
                opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "")
            );

        CreateMap<Branch, string>().ConvertUsing(branch => branch.Name);
        CreateMap<Report.ReportStatus, string>().ConvertUsing(status => status.ToString());
        CreateMap<Report, BaseReport>()
            .ForMember(
                r => r.Url,
                opts =>
                    opts.MapFrom(
                        (r, _) =>
                            r.Repository.Provider == null
                                ? ""
                                : r.Repository.Provider.BuildUrl(
                                    r.Repository.ProviderUsername,
                                    r.Repository.Name
                                )
                    )
            )
            .ForMember(
                r => r.UserName,
                opts => opts.MapFrom((r, _) => r.Repository.ProviderUsername)
            );

        CreateMap<Report, FullReport>()
            .ForMember(view => view.Committers, opts => opts.MapFrom(GetCommitters))
            .ForMember(view => view.CommitsPerMonth, opts => opts.MapFrom(GetMonthlyCommits))
            .ForMember(
                view => view.RepositoryName,
                opts => opts.MapFrom((r, _) => r.Repository.Name)
            )
            .ForMember(
                view => view.Url,
                opts =>
                    opts.MapFrom(
                        (r, _) =>
                            r.Repository.Provider == null
                                ? ""
                                : r.Repository.Provider.BuildUrl(
                                    r.Repository.ProviderUsername,
                                    r.Repository.Name
                                )
                    )
            )
            .ForMember(
                view => view.UserName,
                opts => opts.MapFrom((r, _) => r.Repository.ProviderUsername)
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
