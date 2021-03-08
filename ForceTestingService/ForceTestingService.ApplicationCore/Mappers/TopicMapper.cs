using System.Linq;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class TopicMapper: GenericMapper<Topic, TopicDto>
    {
        private readonly QuestionMapper _questionMapper = new QuestionMapper();

        public override Topic Map(TopicDto dto)
        {
            return new Topic()
            {
                Id = dto.Id,
                Name = dto.Name,
                Questions = dto.Questions.Select(topicDto => _questionMapper.Map(topicDto)).ToList(),
                SubjectId = dto.SubjectId
            };
        }

        public override TopicDto Map(Topic entity)
        {
            return new TopicDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Questions = entity.Questions.Select(topicEntity => _questionMapper.Map(topicEntity)).ToList(),
                SubjectId = entity.SubjectId
            };
        }
    }
}