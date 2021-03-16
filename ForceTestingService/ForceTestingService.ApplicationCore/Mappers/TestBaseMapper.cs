using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class TestBaseMapper : GenericMapper<TestBase, TestBaseDto>
    {
        public override TestBase Map(TestBaseDto dto)
        {
            return new TestBase()
            {
                Id = dto.Id,
                Name = dto.Name,
                NumOfQuestions = dto.NumOfQuestions,
                AmountOfSeconds = dto.AmountOfSeconds,
                NumOfTries = dto.NumOfTries,
                TopicId = dto.TopicId
            };
        }

        public override TestBaseDto Map(TestBase entity)
        {
            return new TestBaseDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                NumOfQuestions = entity.NumOfQuestions,
                AmountOfSeconds = entity.AmountOfSeconds,
                NumOfTries = entity.NumOfTries,
                TopicId = entity.TopicId
            };
        }
    }
}