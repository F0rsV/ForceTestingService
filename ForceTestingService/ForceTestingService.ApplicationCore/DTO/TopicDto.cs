using System.Collections.Generic;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

        public int SubjectId { get; set; }
    }
}
