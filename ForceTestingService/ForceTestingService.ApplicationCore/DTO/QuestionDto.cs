using System.Collections.Generic;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string TaskInfo { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }

        public int TopicId { get; set; }
    }
}