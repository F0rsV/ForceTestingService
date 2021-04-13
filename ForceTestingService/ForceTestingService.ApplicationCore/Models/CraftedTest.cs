using System;
using System.Collections.Generic;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Models
{
    public class CraftedTest
    {
        public User User { get; set; }
        public TestBaseDto TestBaseDto { get; set; }
        public DateTime Date { get; set; }
        public float Score { get; set; }

        public IEnumerable<QuestionDto> Questions { get; set; }

        public CraftedTest(){}

        public CraftedTest(User user, TestBaseDto testBaseDto)
        {
            User = user;
            TestBaseDto = testBaseDto;
            Date = DateTime.UtcNow;
            Score = 0;
            Questions = new List<QuestionDto>();
        }
    }
}