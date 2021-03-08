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
                Answers = dto.Answers.Select(answerDto => _answerMapper.Map(answerDto)).ToList(),
                TopicId = dto.TopicId
            };
        }

        public override QuestionDto Map(Question entity)
        {
            return new QuestionDto()
            {
                Id = entity.Id,
                TaskInfo = entity.TaskInfo,
                Answers = entity.Answers.Select(answerEntity => _answerMapper.Map(answerEntity)).ToList(),
                TopicId = entity.TopicId
            };
        }
    }
}