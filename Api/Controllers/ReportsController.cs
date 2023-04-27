using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Mineshard.Api.Models.Reports;
using Mineshard.Persistence.Models;
using Mineshard.Persistence.Repos;

namespace Mineshard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsRepo repo;
    private readonly IUserRepository userRepo;
    private readonly IMapper mapper;

    public ReportsController(IReportsRepo repo, IUserRepository userRepo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
        this.userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = new List<ReportView>();
        foreach (var r in this.repo.GetAll())
            res.Add(this.MapReport(r));
        return Ok(res);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetOne([FromRoute] Guid id)
    {
        var report = this.repo.GetById(id);
        if (report == null)
            return NotFound();
        else
            return Ok(this.MapReport(report));
    }

    [HttpPost]
    public IActionResult RequestAnalysis([FromBody] ReportRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = this.userRepo.GetById(request.UserId);
        if (user == null)
        {
            ModelState.AddModelError("UserId", "Not associated user found");
            return BadRequest(ModelState);
        }

        var provider = this.repo.GetProviderByName(request.Provider);
        if (provider == null)
        {
            ModelState.AddModelError("Provider", "Unregistered provider");
            return BadRequest(ModelState);
        }

        var repo = this.repo.GetRepositoryByName(
            provider.Name,
            request.ProviderUser,
            request.RepositoryName
        );
        if (repo == null)
        {
            repo = new Repository
            {
                Name = request.RepositoryName,
                ProviderId = provider.Id,
                Provider = null,
                RequestorId = user.Id,
                Requestor = null,
                ProviderUsername = request.ProviderUser,
                Reports = new List<Report>()
            };
            this.repo.AddRepository(repo);
        }

        return RequestNewAnalysis(repo.Id);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult RequestNewAnalysis([FromRoute] Guid id)
    {
        var repo = this.repo.GetRepositoryById(id);
        if (repo == null)
        {
            ModelState.AddModelError("RepositoryId", "Not associated repository found");
            return BadRequest(ModelState);
        }
        var report = new Report { Status = Report.ReportStatus.Pending, Repository = repo };
        this.repo.Add(report);

        return CreatedAtAction(
            nameof(GetOne),
            new { id = report.Id },
            this.mapper.Map<Report, BaseReport>(report)
        );
    }

    private BaseReport MapReport(Report r) =>
        r.Status == Report.ReportStatus.Ready
            ? this.mapper.Map<Report, FullReport>(r)
            : this.mapper.Map<Report, BaseReport>(r);
}
