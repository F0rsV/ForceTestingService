using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Models
{
    public class StudentTest
    {
        public int TestBaseId { get; set; }

        public string TestName { get; set; }
        public string SubjectName { get; set; }
        public string TopicName { get; set; }
        public int NumOfQuestions { get; set; }
        public int AmountOfSeconds { get; set; }
        public int NumOfTriesLeft { get; set; }
        public bool IsPassed { get; set; }

        public StudentTest(TestBase testBase, int numOfTriesLeft, bool isPassed)
        {
            TestBaseId = testBase.Id;
            TestName = testBase.Name;
            SubjectName = testBase.Topic.Subject.Name;
            TopicName = testBase.Topic.Name;
            NumOfQuestions = testBase.NumOfQuestions;
            AmountOfSeconds = testBase.AmountOfSeconds;
            NumOfTriesLeft = numOfTriesLeft;
            IsPassed = isPassed;
        }
    }
}