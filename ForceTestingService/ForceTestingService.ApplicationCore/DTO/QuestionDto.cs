using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        [Required]
        public string TaskInfo { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }

        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }
}