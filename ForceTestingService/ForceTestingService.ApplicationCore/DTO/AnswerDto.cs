using System.ComponentModel.DataAnnotations;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class AnswerDto
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}