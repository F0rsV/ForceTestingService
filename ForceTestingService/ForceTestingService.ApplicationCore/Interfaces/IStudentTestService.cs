using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Models;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface IStudentTestService
    {
        Task<IEnumerable<StudentTest>> CreateStudentTestsListForUserAsync(User user);
        Task<IEnumerable<TestResultDto>> GetTestResultsForUserAsync(User user, int testBaseId);
        Task<CraftedTest> CraftRandomTestFromTestBaseAsync(User user, int testBaseId);
        Task SubmitTestAsync(CraftedTest craftedTest);
    }
}