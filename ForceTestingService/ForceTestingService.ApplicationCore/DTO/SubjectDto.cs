using System.Collections.Generic;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TopicDto> Topics { get; set; }
    }
}