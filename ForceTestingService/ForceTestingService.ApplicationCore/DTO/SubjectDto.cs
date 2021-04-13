using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class SubjectDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<TopicDto> Topics { get; set; }
    }
}