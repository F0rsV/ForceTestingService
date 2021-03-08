using System.Collections.Generic;
using System.Linq;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class SubjectMapper : GenericMapper<Subject, SubjectDto>
    {
        private readonly TopicMapper _topicMapper = new TopicMapper();

        public override Subject Map(SubjectDto dto)
        {
            return new Subject()
            {
                Id = dto.Id,
                Name = dto.Name,
                Topics = dto.Topics.Select(topicDto => _topicMapper.Map(topicDto)).ToList()
            };
        }

        public override SubjectDto Map(Subject entity)
        {
            return new SubjectDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Topics = entity.Topics.Select(entityDto => _topicMapper.Map(entityDto)).ToList()
            };
        }
    }
}