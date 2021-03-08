using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class TestMapper: GenericMapper<Test, TestDto>
    {
        public override Test Map(TestDto dto)
        {
            return new Test()
            {
                Id = dto.Id,
                Name = dto.Name,
                NumOfQuestions = dto.NumOfQuestions,
                AmountOfSeconds = dto.AmountOfSeconds,
                NumOfTries = dto.NumOfTries,
                TopicId = dto.TopicId
            };
        }

        public override TestDto Map(Test entity)
        {
            return new TestDto()
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