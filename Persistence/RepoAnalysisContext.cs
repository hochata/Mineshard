using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Mineshard.Persistence.Models;
using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence;

public class RepoAnalysisContext : DbContext
{
    public DbSet<Report>? Reports { get; set; }
    public DbSet<Branch>? Branches { get; set; }
    public DbSet<Committer>? Commiters { get; set; }
    public DbSet<MonthlyLoad>? MonthlyLoads { get; set; }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRoleId = new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e");
        var collaboratorRoleId = new Guid("c309fa92-2123-47be-b397-a1c77adb502c");

        var roles = new List<Role>
        {
            new Role
            {
                Id = adminRoleId,
                Name = "Admin",
                Description = "Administrator role",
                CreatedAt = DateTime.UtcNow,
            },
            new Role
            {
                Id = collaboratorRoleId,
                Name = "Collaborator",
                Description = "Collaborator role",
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Role>().HasData(roles);
    }
}
