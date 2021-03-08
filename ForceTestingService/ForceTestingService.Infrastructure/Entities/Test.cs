namespace ForceTestingService.Infrastructure.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfQuestions { get; set; }
        public int AmountOfSeconds { get; set; }
        public int NumOfTries { get; set; }

        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }
}