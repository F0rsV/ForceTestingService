using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class AnswerMapper: GenericMapper<Answer, AnswerDto>
    {
        public override Answer Map(AnswerDto dto)
        {
            return new Answer()
            {
                Id = dto.Id,
                Text = dto.Text,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId
            };
        }

        public override AnswerDto Map(Answer entity)
        {
            return new AnswerDto()
            {
                Id = entity.Id,
                Text = entity.Text,
                IsCorrect = entity.IsCorrect,
                QuestionId = entity.QuestionId
            };
        }
    }
}