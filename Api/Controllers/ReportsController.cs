using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshard.Api.Models;
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
    public IActionResult GetAll() =>
        Ok(this.mapper.Map<List<Report>, List<ReportView>>(this.repo.GetAll()));

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetOne([FromRoute] Guid id)
    {
        var report = this.repo.GetOne(id);
        if (report == null)
            return NotFound();
        else
            return Ok(this.mapper.Map<Report, ReportView>(report));
    }
}
