using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        private SubjectMapper _subjectMapper;
        public SubjectMapper SubjectMapper => _subjectMapper ??= new SubjectMapper();

        private TopicMapper _topicMapper;
        public TopicMapper TopicMapper => _topicMapper ??= new TopicMapper();

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        {
            var subjectEntities = await _unitOfWork.SubjectRepository.
                GetAllAsync(null, null, new[] {"Topics"});
            return SubjectMapper.Map(subjectEntities);
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(int subjectId)
        {
            var subjectEntityToGet = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (subjectEntityToGet == null)
                throw new Exception("No subject with this ID was found"); //TODO Exceptions

            var subjectDto = SubjectMapper.Map(subjectEntityToGet);
            var topicsEntities = await _unitOfWork.TopicRepository
                .GetAllAsync(topic => topic.SubjectId == subjectId);

            subjectDto.Topics = (ICollection<TopicDto>)TopicMapper.Map(topicsEntities); ;
            return subjectDto;
        }

        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subjectEntity = SubjectMapper.Map(subjectDto);
            await _unitOfWork.SubjectRepository.InsertAsync(subjectEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateSubjectAsync(SubjectDto subjectDto)
        {
            var subjectEntity = SubjectMapper.Map(subjectDto);
            _unitOfWork.SubjectRepository.Update(subjectEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteSubjectByIdAsync(int subjectId)
        {
            var subjectEntityToDelete = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (subjectEntityToDelete == null)
                throw new Exception("No subject with this ID was found"); //TODO Exceptions
            
            _unitOfWork.SubjectRepository.Delete(subjectEntityToDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}