using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        private AnswerMapper _answerMapper;
        public AnswerMapper AnswerMapper => _answerMapper ??= new AnswerMapper();

        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AnswerDto>> GetAllAnswersOfSpecificQuestionAsync(int questionId)
        {
            var answers = await _unitOfWork.AnswerRepository
                .GetAllAsync(answer => answer.QuestionId == questionId, null, new[] { "Question" });

            return AnswerMapper.Map(answers);
        }

        public async Task<AnswerDto> GetAnswerByIdAsync(int answerId)
        {
            var answerEntityToGet = await _unitOfWork.AnswerRepository.GetByIdAsync(answerId);
            if (answerEntityToGet == null)
                throw new Exception("No answer with this ID was found");

            answerEntityToGet.Question = await _unitOfWork.QuestionRepository.GetByIdAsync(answerEntityToGet.QuestionId);

            var answerDto = AnswerMapper.Map(answerEntityToGet); ;
            return answerDto;
        }

        public async Task CreateAnswerAsync(AnswerDto answerDto)
        {
            var answerEntity = AnswerMapper.Map(answerDto);
            await _unitOfWork.AnswerRepository.InsertAsync(answerEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAnswerAsync(AnswerDto answerDto)
        {
            var answerEntity = AnswerMapper.Map(answerDto);
            _unitOfWork.AnswerRepository.Update(answerEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAnswerByIdAsync(int answerId)
        {
            var answerEntityToDelete = await _unitOfWork.AnswerRepository.GetByIdAsync(answerId);
            if (answerEntityToDelete == null)
                throw new Exception("No answer with this ID was found");

            _unitOfWork.AnswerRepository.Delete(answerEntityToDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}