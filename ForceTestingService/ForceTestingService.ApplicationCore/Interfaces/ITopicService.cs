using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDto>> GetAllTopicsAsync();
        Task<TopicDto> GetTopicByIdAsync(int topicId);
        Task CreateTopicAsync(TopicDto topicDto);
        Task UpdateTopicAsync(TopicDto topicDto);
        Task DeleteTopicByIdAsync(int topicId);
    }
}