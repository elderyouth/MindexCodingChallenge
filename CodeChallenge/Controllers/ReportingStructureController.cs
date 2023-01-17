using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
  [Route("api/reporting")]
  [ApiController]
  public class ReportingStructureController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly IReportingSctructureService _reportingSctructureService;

    public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingSctructureService reportingSctructureService)
    {
      _logger = logger;
      _reportingSctructureService = reportingSctructureService;
    }

    [HttpGet("{employeeId}", Name ="GetReportingStructure")]
    public IActionResult GetReportingStructure(string employeeId)
    {
      _logger.LogDebug($"Received GET request for {employeeId} in Reporting Structure ");
      var reportingStructure = _reportingSctructureService.GetReportingStructure(employeeId);
      if(reportingStructure== null) { return NotFound(); }
      return Ok(reportingStructure);
    }


  }
}
