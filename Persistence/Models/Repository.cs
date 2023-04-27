using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Persistence.Models;

public class Repository
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ProviderUsername { get; set; }

    public Guid ProviderId { get; set; }
    public Guid RequestorId { get; set; }

    public User? Requestor { get; set; }
    public Provider? Provider { get; set; }
    public ICollection<Report>? Reports { get; set; }

    public string BuildUrl(string user, string repo) =>
        this.Provider == null ? "" : this.Provider.BuildUrl(user, repo);
}
