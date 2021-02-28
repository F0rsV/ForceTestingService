using System.Collections.Generic;

namespace ForceTestingService.Infrastructure.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string TaskInfo { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }
}