using System.Linq;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class TopicMapper: GenericMapper<Topic, TopicDto>
    {
        public override Topic Map(TopicDto dto)
        {
            return new Topic()
            {
                Id = dto.Id,
                Name = dto.Name,
                SubjectId = dto.SubjectId
            };
        }

        public override TopicDto Map(Topic entity)
        {
            return new TopicDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                SubjectId = entity.SubjectId,
                SubjectName = entity.Subject.Name
            };
        }
    }
}