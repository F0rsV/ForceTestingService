using System;

namespace ForceTestingService.ApplicationCore.DTO
{
    public class TestResultDto
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public DateTime Date { get; set; }

        public int StudentUserId { get; set; }
    }
}