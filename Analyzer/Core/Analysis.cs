using LibGit2Sharp;

using Mineshard.Analyzer.Models;

namespace Mineshard.Analyzer.Core;

public class Analysis : IDisposable
{
    public enum RepoStatus
    {
        Nop,
        Cloning,
        Cloned,
        Failed
    }

    private readonly string localPath;
    private Repository? gitRepo;
    public RepoStatus Status { get; private set; } = RepoStatus.Nop;
    private readonly string mergeUser;
    private readonly string mergeEmail;

    public Analysis(string url, string name, string mergeUser, string mergeEmail)
    {
        this.Status = RepoStatus.Cloning;
        this.mergeUser = mergeUser;
        this.mergeEmail = mergeEmail;
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        this.localPath = Path.Join(appData, "Mineshard", "Repos", name);

        if (Directory.Exists(this.localPath))
        {
            try
            {
                this.Pull();
                this.Status = RepoStatus.Cloned;
            }
            catch (LibGit2SharpException e)
            {
                Console.WriteLine(e);
                if (Directory.Exists(this.localPath))
                    Directory.Delete(this.localPath, true);

                this.Clone(url);
            }
        }
        else
        {
            this.Clone(url);
        }
    }

    public int NumCommits
    {
        get
        {
            if (this.Status == RepoStatus.Cloned && this.gitRepo != null)
                return this.gitRepo.Commits.Count();
            else
                throw new InvalidDataException("Repository is not ready!");
        }
    }

    public IEnumerable<string> Branches
    {
        get
        {
            if (this.Status == RepoStatus.Cloned && this.gitRepo != null)
                return from b in this.gitRepo.Branches select b.FriendlyName;
            else
                throw new InvalidDataException("Repository is not ready!");
        }
    }

    public IEnumerable<Committer> Committers
    {
        get
        {
            if (this.Status == RepoStatus.Cloned && this.gitRepo != null)
            {
                return from c in this.gitRepo.Commits
                       group c by c.Author.Name into authorCommits
                       select new Committer
                       {
                           Name = authorCommits.Key,
                           CommitCount = authorCommits.Count()
                       };
            }
            else
                throw new InvalidDataException("Repository is not ready!");
        }
    }

    public IEnumerable<Month> CommitsPerMonth
    {
        get
        {
            if (this.Status == RepoStatus.Cloned && this.gitRepo != null)
            {
                return from c in this.gitRepo.Commits
                       where c.Author.When >= DateTime.Now.AddYears(-1)
                       group c by c.Author.When.Month into commitsPerMonth
                       select new Month
                       {
                           Code = commitsPerMonth.Key,
                           NumCommits = commitsPerMonth.Count()
                       };
            }
            else
                throw new InvalidDataException("Repository is not ready!");
        }
    }

    private void Pull()
    {
        this.gitRepo = new Repository(localPath);
        var opts = new PullOptions();
        opts.FetchOptions = new FetchOptions();
        var signature = new Signature(
            this.mergeUser,
            this.mergeEmail,
            new DateTimeOffset(DateTime.Now)
        );

        Commands.Pull(this.gitRepo, signature, opts);
    }

    private void Clone(string url)
    {
        try
        {
            var path = Repository.Clone(url, localPath);
            this.gitRepo = new Repository(path);
            this.Status = RepoStatus.Cloned;
        }
        catch (LibGit2SharpException e)
        {
            Console.WriteLine(e);
            this.Status = RepoStatus.Failed;
        }
    }

    void IDisposable.Dispose()
    {
        if (this.gitRepo != null)
        {
            this.gitRepo.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}
