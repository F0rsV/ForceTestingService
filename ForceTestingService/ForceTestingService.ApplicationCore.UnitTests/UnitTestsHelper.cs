using System;
using System.Collections.Generic;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForceTestingService.ApplicationCore.UnitTests
{
    internal static class UnitTestsHelper
    {
        public static DbContextOptions<ApplicationDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            using var context = new ApplicationDbContext(options);
            SeedData(context);
            return options;
        }

        private static void SeedData(ApplicationDbContext context)
        {
            context.Users.Add(new User() {Id = 1, FirstName = "TestFName1", LastName = "TestLName1"});
            context.Users.Add(new User() {Id = 2, FirstName = "TestFName2", LastName = "TestLName2"});

            context.Subjects.Add(new Subject() {Id = 1, Name = "SubName01", Topics = new List<Topic>()});
            context.Subjects.Add(new Subject() {Id = 2, Name = "SubName02", Topics = new List<Topic>()});

            context.Topics.Add(new Topic()
            {
                Id = 1, Name = "TopicName01", Questions = new List<Question>(), SubjectId = 1
            });
            context.Topics.Add(new Topic()
            {
                Id = 2, Name = "TopicName02", Questions = new List<Question>(), SubjectId = 2
            });

            context.Questions.Add(new Question()
                {Id = 1, Answers = new List<Answer>(), TaskInfo = "TaskInfo01", TopicId = 1});
            context.Questions.Add(new Question()
                {Id = 2, Answers = new List<Answer>(), TaskInfo = "TaskInfo02", TopicId = 2});

            context.Answers.Add(new Answer()
                {Id = 1, Text = "Text01", IsCorrect = true, QuestionId = 1});
            context.Answers.Add(new Answer()
                {Id = 2, Text = "Text02", IsCorrect = false, QuestionId = 2});

            context.TestBases.Add(new TestBase()
            {
                Id = 1, Name = "Name01", AmountOfSeconds = 100, NumOfQuestions = 2, NumOfTries = 3, TopicId = 1
            });
            context.TestBases.Add(new TestBase()
            {
                Id = 2, Name = "Name02", AmountOfSeconds = 50, NumOfQuestions = 3, NumOfTries = 2, TopicId = 1
            });

            context.TestResults.Add(new TestResult()
            {
                Id = 1,
                Date = new DateTime(2000, 10, 24),
                Score = 50.5f,
                TestBaseId = 1,
                StudentUserId = 1
            });

            context.TestResults.Add(new TestResult()
            {
                Id = 2,
                Date = new DateTime(2000, 10, 04),
                Score = 90.5f,
                TestBaseId = 2,
                StudentUserId = 2
            });

            context.SaveChanges();
        }
    }
}