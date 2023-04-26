namespace Mineshard.Persistence.Models;

public class Provider
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }

    public string BuildUrl(string user, string repo) => $"{this.Url}/{user}/{repo}";
}
