using System.Collections.Generic;

namespace ForceTestingService.Infrastructure.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}