using CodeChallenge.Models;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
  public class CompensationService : ICompensationService
  {
    private readonly ICompensationRepository _compensationRepository;

    public CompensationService(ICompensationRepository compensationRepository)
    {
      _compensationRepository = compensationRepository;
    }
    /// <summary>
    /// Creates a new Compensation entry to the DB
    /// </summary>
    /// <param name="compensation"></param>
    /// <returns></returns>
    public Compensation CreateCompensation(Compensation compensation)
    {
      if (compensation == null) return null;

      _compensationRepository.AddCompesation(compensation);
      _compensationRepository.SaveAsync().Wait();
      return compensation;
    }

    /// <summary>
    /// Retrieves Compensation based on an employee's ID
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns>A compensation object or null depending if the employee has already been added</returns>
    public Compensation GetCompensation(string employeeId)
    {
      return string.IsNullOrEmpty(employeeId) ? null : _compensationRepository.GetCompesationById(employeeId);
    }
  }
}
