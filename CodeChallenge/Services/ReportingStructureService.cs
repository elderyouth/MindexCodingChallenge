using CodeChallenge.Models;
using CodeChallenge.Repositories;
using System;
using System.Collections;

namespace CodeChallenge.Services
{
  public class ReportingStructureService : IReportingSctructureService
  {
    private readonly IEmployeeRepository _employeeRepository;

    public ReportingStructureService(IEmployeeRepository employeeRepository)
    {
      _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Base implementation to get Reporting Structure
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    public ReportingStructure GetReportingStructure(string employeeId)
    {
      Employee employee = _employeeRepository.GetByIdIncludeDirectReports(employeeId);
      return new ReportingStructure()
      {
        Employee = employee,
        NumberofReports = GetNumberofReports(employeeId)
      };
    }

    /// <summary>
    /// Obtain total number of reportees under specified employee
    /// </summary>
    /// <param name="employee"></param>
    /// <returns>Number of reporting employees thru a tree search</returns>
    private int GetNumberofReports(string employeeId)
    {
      var employee = _employeeRepository.GetByIdIncludeDirectReports(employeeId);
      var directReport = employee.DirectReports;
      if (employee == null || directReport == null) return 0;
      int reportingNumber = directReport.Count;


      foreach (var reportee in directReport)
      {
        //Recursive search until reportee no long er has element in DirectReports
        reportingNumber += GetNumberofReports(reportee.EmployeeId);
      }

      return reportingNumber;

    }
  }
}
