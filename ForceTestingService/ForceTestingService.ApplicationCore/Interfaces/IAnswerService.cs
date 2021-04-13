using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<AnswerDto>> GetAllAnswersOfSpecificQuestionAsync(int questionId);
        Task<AnswerDto> GetAnswerByIdAsync(int answerId);
        Task CreateAnswerAsync(AnswerDto answerDto);
        Task UpdateAnswerAsync(AnswerDto answerDto);
        Task DeleteAnswerByIdAsync(int answerId);
    }
}