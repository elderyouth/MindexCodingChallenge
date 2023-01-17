using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
  public interface ICompensationService
  {
    Compensation CreateCompensation(Compensation compensation);
    Compensation GetCompensation(string employeeId);
  }
}