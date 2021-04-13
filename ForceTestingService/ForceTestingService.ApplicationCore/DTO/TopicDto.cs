using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class TopicDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
