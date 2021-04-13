using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        private TestResultMapper _testResultMapper;
        public TestResultMapper TestResultMapper => _testResultMapper ??= new TestResultMapper();

        public TestResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TestResultDto>> GetTestResultsByTestBaseIdAsync(int testBaseId)
        {
            var testResultEntities = await _unitOfWork.TestResultRepository.GetAllAsync(
                testResult => testResult.TestBaseId == testBaseId,
                x => x.OrderBy(testResult => testResult.StudentUser.LastName), 
                new[] {"TestBase", "StudentUser"});

            return TestResultMapper.Map(testResultEntities);
        }
    }
}