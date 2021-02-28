using System.Collections.Generic;

namespace ForceTestingService.Infrastructure.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}