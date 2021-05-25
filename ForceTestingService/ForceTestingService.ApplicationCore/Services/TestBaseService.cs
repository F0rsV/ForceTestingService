using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class TestBaseService : ITestBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        private TestBaseMapper _testBaseMapper;
        public TestBaseMapper TestBaseMapper => _testBaseMapper ??= new TestBaseMapper();

        public TestBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TestBaseDto>> GetAllTestBasesAsync()
        {
            var testBaseEntities = await _unitOfWork.TestBaseRepository.
                GetAllAsync(null, null, new[] { "Topic" });
            return TestBaseMapper.Map(testBaseEntities);
        }

        public async Task<TestBaseDto> GetTestBaseByIdAsync(int testBaseId)
        {
            var testBaseEntityToGet = await _unitOfWork.TestBaseRepository.GetByIdAsync(testBaseId);
            if (testBaseEntityToGet == null)
                throw new Exception("No TestBase with this Id was found");

            testBaseEntityToGet.Topic = await _unitOfWork.TopicRepository.GetByIdAsync(testBaseEntityToGet.TopicId);

            var testBaseDto = TestBaseMapper.Map(testBaseEntityToGet);
            return testBaseDto;
        }

        public async Task CreateTestBaseAsync(TestBaseDto testBaseDto)
        {
            var testBaseEntity = TestBaseMapper.Map(testBaseDto);
            await _unitOfWork.TestBaseRepository.InsertAsync(testBaseEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTestBaseAsync(TestBaseDto testBaseDto)
        {
            var testBaseEntity = TestBaseMapper.Map(testBaseDto);
            _unitOfWork.TestBaseRepository.Update(testBaseEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTestBaseByIdAsync(int testBaseId)
        {
            var testBaseEntityToDelete = await _unitOfWork.TestBaseRepository.GetByIdAsync(testBaseId);
            if (testBaseEntityToDelete == null)
                throw new Exception("No TestBase with this ID was found");

            _unitOfWork.TestBaseRepository.Delete(testBaseEntityToDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}