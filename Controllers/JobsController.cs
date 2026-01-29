using JobFinder.Api.DTOs;
using JobFinder.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? keyword)
    {
        var jobs = _jobService.Search(keyword)
            .Select(j => new JobDto
            {
                Title = j.Title,
                Company = j.Company,
                Location = j.Location,
                ContractType = j.ContractType,
                TechStack = j.TechStack,
                Url = j.Url,
                Source = j.Source
            });

        return Ok(jobs);
    }
}