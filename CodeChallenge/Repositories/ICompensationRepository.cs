using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
  public interface ICompensationRepository
  {
    Compensation GetCompesationById(string id);
    Compensation AddCompesation(Compensation compensation);
    Task SaveAsync();
  }
}
