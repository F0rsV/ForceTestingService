using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;

        private TopicMapper _topicMapper;
        public TopicMapper TopicMapper => _topicMapper ??= new TopicMapper();

        private QuestionMapper _questionMapper;
        public QuestionMapper QuestionMapper => _questionMapper ??= new QuestionMapper();

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsAsync()
        {
            var topicEntities = await _unitOfWork.TopicRepository.
                GetAllAsync(null, null, new[] { "Subject" });
            return TopicMapper.Map(topicEntities);
        }

        public async Task<TopicDto> GetTopicByIdAsync(int topicId)
        {
            var topicEntityToGet = await _unitOfWork.TopicRepository.GetByIdAsync(topicId);
            if (topicEntityToGet == null)
                throw new Exception("No topic with this ID was found"); //TODO Exceptions

            topicEntityToGet.Subject = await _unitOfWork.SubjectRepository.GetByIdAsync(topicEntityToGet.SubjectId);

            var topicDto = TopicMapper.Map(topicEntityToGet);
            var questionsEntities = await _unitOfWork.QuestionRepository
                .GetAllAsync(question => question.TopicId == topicId, null, new[] {"Topic"});

            topicDto.Questions = (ICollection<QuestionDto>) QuestionMapper.Map(questionsEntities);
            return topicDto;
        }

        public async Task CreateTopicAsync(TopicDto topicDto)
        {
            var topicEntity = TopicMapper.Map(topicDto);
            await _unitOfWork.TopicRepository.InsertAsync(topicEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTopicAsync(TopicDto topicDto)
        {
            var topicEntity = TopicMapper.Map(topicDto);
            _unitOfWork.TopicRepository.Update(topicEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTopicByIdAsync(int topicId)
        {
            var topicEntityToDelete = await _unitOfWork.TopicRepository.GetByIdAsync(topicId);
            if (topicEntityToDelete == null)
                throw new Exception("No Topic with this ID was found"); //TODO Exceptions

            _unitOfWork.TopicRepository.Delete(topicEntityToDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}