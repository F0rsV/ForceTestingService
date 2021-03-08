namespace ForceTestingService.ApplicationCore.DTO
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfQuestions { get; set; }
        public int AmountOfSeconds { get; set; }
        public int NumOfTries { get; set; }

        public int TopicId { get; set; }
    }
}