using System.ComponentModel.DataAnnotations;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class TestBaseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Minimum one question allowed")]
        public int NumOfQuestions { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int AmountOfSeconds { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Minimum one try allowed")]
        public int NumOfTries { get; set; }

        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }
}