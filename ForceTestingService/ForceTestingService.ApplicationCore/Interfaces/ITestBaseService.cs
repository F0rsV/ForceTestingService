using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface ITestBaseService
    {
        Task<IEnumerable<TestBaseDto>> GetAllTestBasesAsync();
        Task<TestBaseDto> GetTestBaseByIdAsync(int testBaseId);
        Task CreateTestBaseAsync(TestBaseDto testBaseDto);
        Task UpdateTestBaseAsync(TestBaseDto testBaseDto);
        Task DeleteTestBaseByIdAsync(int testBaseId);
    }
}