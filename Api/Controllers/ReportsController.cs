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
    private readonly IMapper mapper;

    public ReportsController(IReportsRepo repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
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
        var report = this.repo.GetOne(id);
        if (report == null)
            return NotFound();
        else
            return Ok(this.MapReport(report));
    }

    private ReportView MapReport(Report r) =>
        r.Status == Report.ReportStatus.Ready
            ? this.mapper.Map<Report, FullReport>(r)
            : this.mapper.Map<Report, MinimalReport>(r);
}
