using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface ITestResultService
    {
        Task<IEnumerable<TestResultDto>> GetTestResultsByTestBaseIdAsync(int testBaseId);
    }
}