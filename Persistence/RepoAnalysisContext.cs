using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Mineshard.Persistence.Models;

namespace Mineshard.Persistence;

public class RepoAnalysisContext : DbContext
{
    public DbSet<Report>? Reports { get; set; }
    public DbSet<Branch>? Branches { get; set; }
    public DbSet<Committer>? Commiters { get; set; }
    public DbSet<MonthlyLoad>? MonthlyLoads { get; set; }

    private readonly string? connectionString;

    public RepoAnalysisContext()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(GetConfigurationPath())
            .AddJsonFile("appsettings.json");

        var configuration = builder.Build();
        if (configuration == null)
            throw new InvalidDataException("Couldn't find configuration file");
        this.connectionString = configuration.GetConnectionString("MineshardDb");
    }

    private static string GetConfigurationPath()
    {
        var cwd = Directory.GetParent(Directory.GetCurrentDirectory());
        return cwd == null ? Directory.GetCurrentDirectory() : cwd.FullName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(this.connectionString);
    }
}
