using System.Linq;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class QuestionMapper: GenericMapper<Question, QuestionDto>
    {
        private readonly AnswerMapper _answerMapper = new AnswerMapper();
        public override Question Map(QuestionDto dto)
        {
            return new Question()
            {
                Id = dto.Id,
                TaskInfo = dto.TaskInfo,
                TopicId = dto.TopicId
            };
        }

        public override QuestionDto Map(Question entity)
        {
            return new QuestionDto()
            {
                Id = entity.Id,
                TaskInfo = entity.TaskInfo,
                TopicId = entity.TopicId,
                TopicName = entity.Topic.Name
            };
        }
    }
}