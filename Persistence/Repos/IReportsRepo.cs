using Mineshard.Persistence.Models;

namespace Mineshard.Persistence.Repos;

public interface IReportsRepo
{
    List<Report> GetAll();
    Report? GetById(Guid id);
    bool Add(Report source);
    void Update(Report source, Report dest);
    Provider? GetProviderByName(string name);
    Repository? GetRepositoryById(Guid id);
    Repository? GetRepositoryByName(string provider, string user, string name);
    bool AddRepository(Repository repo);
}
