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
    private readonly IConfiguration? configuration;

    public RepoAnalysisContext()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        this.configuration = builder.Build();
        if (this.configuration == null)
            throw new InvalidDataException("Couldn't find configuration file");
        this.connectionString = this.configuration.GetConnectionString("MineshardDb");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(this.connectionString);
    }
}
