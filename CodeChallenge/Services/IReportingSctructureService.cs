using CodeChallenge.Models;

namespace CodeChallenge.Services
{
  public interface IReportingSctructureService
  {
    ReportingStructure GetReportingStructure(string employeeId);
  }
}
