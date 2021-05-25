using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private QuestionMapper _questionMapper;
        public QuestionMapper QuestionMapper => _questionMapper ??= new QuestionMapper();

        private AnswerMapper _answerMapper;
        public AnswerMapper AnswerMapper => _answerMapper ??= new AnswerMapper();

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsOfSpecificTopicAsync(int topicId)
        {
            var questions = await _unitOfWork.QuestionRepository
                .GetAllAsync(question => question.TopicId == topicId, null, new []{"Answers", "Topic"});

            var questionsDtOs = QuestionMapper.Map(questions);
            foreach (var questionDto in questionsDtOs)
            {
                var answersEntities = await _unitOfWork.AnswerRepository
                    .GetAllAsync(answer => answer.QuestionId == questionDto.Id, null, new[] {"Question"});

                questionDto.Answers = (ICollection<AnswerDto>) AnswerMapper.Map(answersEntities);
            }

            return questionsDtOs;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int questionId)
        {
            var questionEntityToGet = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId);
            if (questionEntityToGet == null)
                throw new Exception("No question with this ID was found"); //TODO Exceptions

            questionEntityToGet.Topic = await _unitOfWork.TopicRepository.GetByIdAsync(questionEntityToGet.TopicId);

            var questionDto = QuestionMapper.Map(questionEntityToGet);
            var answersEntities = await _unitOfWork.AnswerRepository
                .GetAllAsync(answer => answer.QuestionId == questionId, null, new []{ "Question" });

            questionDto.Answers = (ICollection<AnswerDto>) AnswerMapper.Map(answersEntities);
            return questionDto;
        }

        public async Task CreateQuestionWithEmptyAnswersAsync(QuestionDto questionDto)
        {
            var questionEntity = QuestionMapper.Map(questionDto);
            await _unitOfWork.QuestionRepository.InsertAsync(questionEntity);
            await _unitOfWork.CommitAsync();
        }


        public async Task UpdateQuestionAsync(QuestionDto questionDto)
        {
            var questionEntity = QuestionMapper.Map(questionDto);
            _unitOfWork.QuestionRepository.Update(questionEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteQuestionByIdAsync(int questionId)
        {
            var questionEntityToDelete = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId);
            if (questionEntityToDelete == null)
                throw new Exception("No Question with this ID was found"); //TODO Exceptions

            _unitOfWork.QuestionRepository.Delete(questionEntityToDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}