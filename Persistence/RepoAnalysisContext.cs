using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mineshard.Persistence;

public class RepoAnalysisContext : DbContext
{
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
