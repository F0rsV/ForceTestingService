using System;

namespace ForceTestingService.Infrastructure.Entities
{
    public class TestResult
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public DateTime Date { get; set; }

        public TestBase TestBase { get; set; }
        public int TestBaseId { get; set; }
        public User StudentUser { get; set; }
        public int StudentUserId { get; set; }
    }
}