using System.Collections.Generic;
using System.Linq;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class SubjectMapper : GenericMapper<Subject, SubjectDto>
    {
        public override Subject Map(SubjectDto dto)
        {
            return new Subject()
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        public override SubjectDto Map(Subject entity)
        {
            return new SubjectDto()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}