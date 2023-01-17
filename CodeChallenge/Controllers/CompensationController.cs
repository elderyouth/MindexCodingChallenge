using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;
using CodeChallenge.Data;

namespace CodeChallenge.Controllers
{
  [ApiController]
  [Route("api/compensation")]
  public class CompensationController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly ICompensationService _compensationService;

    public CompensationController(ICompensationService compensationService, ILogger<CompensationController> logger)
    {
      _compensationService = compensationService;
      _logger = logger;
    }

    [HttpGet("{employeeId}", Name ="GetCompensation")]
    public IActionResult GetCompensation(string employeeId)
    {
      _logger.LogDebug($"Received GET request for {employeeId} Compensation");
      var compensation = _compensationService.GetCompensation(employeeId);
      if (compensation == null) return NotFound();

      return Ok(compensation);
    }

    [HttpPost]
    public IActionResult CreateCompensation([FromBody] Compensation compensation)
    {
      _logger.LogDebug($"Received Post request for new Compensation for {compensation.Employee.LastName} {compensation.Employee.FirstName} effective {compensation.EffectiveDate}");
      _compensationService.CreateCompensation(compensation);
      return CreatedAtRoute("GetCompensation", new { employeeId = compensation.Employee.EmployeeId }, compensation);
    }


  }
}
