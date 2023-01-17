using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
  public class CompensationRepository : ICompensationRepository
  {
    private readonly ILogger<CompensationRepository> _logger;
    private readonly CompensationContext _compensationContext;

    public CompensationRepository(ILogger<CompensationRepository> logger, CompensationContext compensationContext)
    {
      _logger= logger;
      _compensationContext = compensationContext;
    }

    public Compensation AddCompesation(Compensation compensation)
    {
      _compensationContext.Compensations.Add(compensation);
      return compensation;
    }

    public Compensation GetCompesationById(string employeeId)
    {
      return _compensationContext.Compensations.Include(x => x.Employee).SingleOrDefault(x => x.Employee.EmployeeId == employeeId);
    }

    public async Task SaveAsync()
    {
      await _compensationContext.SaveChangesAsync();
    }

  }
}
