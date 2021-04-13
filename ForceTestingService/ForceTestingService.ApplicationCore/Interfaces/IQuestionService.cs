using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionsOfSpecificTopicAsync(int topicId);
        Task<QuestionDto> GetQuestionByIdAsync(int questionId);
        Task CreateQuestionWithEmptyAnswersAsync(QuestionDto questionDto);
        Task UpdateQuestionAsync(QuestionDto questionDto);
        Task DeleteQuestionByIdAsync(int questionId);
    }
}