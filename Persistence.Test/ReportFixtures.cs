using Mineshard.Persistence.Models;

using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Test;

public class ReportFixtures
{
    public static User Requestor
    {
        get =>
            new User
            {
                Email = "busancio@bunsan.io",
                Name = "Bunsancio",
                Username = "bunsancio",
                Role = new Role { Description = "Can read", Name = "Reader" }
            };
    }

    public static List<Committer> SomeCommitters
    {
        get =>
            new List<Committer>
            {
                new Committer { Name = "bob", NumCommits = 30 },
                new Committer { Name = "alice", NumCommits = 30 }
            };
    }

    public static Branch MainBranch
    {
        get => new Branch { Name = "main" };
    }

    public static List<MonthlyLoad> YearlyReport
    {
        get =>
            new List<MonthlyLoad>
            {
                new MonthlyLoad { Month = 1, NumCommits = 5 },
                new MonthlyLoad { Month = 2, NumCommits = 5 },
                new MonthlyLoad { Month = 3, NumCommits = 5 },
                new MonthlyLoad { Month = 4, NumCommits = 5 },
                new MonthlyLoad { Month = 5, NumCommits = 5 },
                new MonthlyLoad { Month = 6, NumCommits = 5 },
                new MonthlyLoad { Month = 7, NumCommits = 5 },
                new MonthlyLoad { Month = 8, NumCommits = 5 },
                new MonthlyLoad { Month = 9, NumCommits = 5 },
                new MonthlyLoad { Month = 10, NumCommits = 5 },
                new MonthlyLoad { Month = 11, NumCommits = 5 },
                new MonthlyLoad { Month = 12, NumCommits = 5 }
            };
    }

    public static Provider GithubProvider
    {
        get => new Provider { Name = "bunsancio", Url = "github.com/bunsancio/providence" };
    }

    public static Repository GithubRepo
    {
        get =>
            new Repository
            {
                Name = "providence",
                Reports = new List<Report>(),
                Provider = GithubProvider,
                ProviderUsername = GithubProvider.Name,
                Requestor = Requestor
            };
    }

    public static Report SampleReport
    {
        get =>
            new Report
            {
                Id = Guid.Parse("0da92302-a07f-495c-8a56-a56c31629d0b"),
                Committers = SomeCommitters,
                Branches = new List<Branch> { MainBranch },
                CommitsPerMonth = YearlyReport,
                NumCommitsOnMain = 60,
                Repository = GithubRepo,
                RepositoryId = GithubRepo.Id,
                Status = Report.ReportStatus.Ready,
            };
    }

    public static Report SampleFailedReport
    {
        get =>
            new Report
            {
                Id = Guid.Parse("c966d5da-98a5-49e1-9f00-39fcf7be5e8e"),
                Repository = GithubRepo,
                RepositoryId = GithubRepo.Id,
                Status = Report.ReportStatus.Failed,
            };
    }

    public static Report SamplePendindReport
    {
        get =>
            new Report
            {
                Id = Guid.Parse("3e51a6b3-e899-4831-95d7-571070f80cc4"),
                Repository = GithubRepo,
                RepositoryId = GithubRepo.Id,
                Status = Report.ReportStatus.Pending,
            };
    }
}
