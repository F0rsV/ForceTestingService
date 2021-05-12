using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Services;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ForceTestingService.ApplicationCore.UnitTests.ServiceTests
{
    [TestFixture]
    public class QuestionServiceTests
    {
        private IQuestionService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new QuestionService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllQuestionsOfSpecificTopicAsync_Returns1Element()
        {
            var act = await _service.GetAllQuestionsOfSpecificTopicAsync(1);
            act.Count().Should().Be(1);
        }

        [Test]
        public async Task GetQuestionByIdAsync_ItemExists_ReturnsQuestionDto()
        {
            var act = await _service.GetQuestionByIdAsync(1);
            act.Should().BeOfType<QuestionDto>();
        }

        [Test]
        public void GetQuestionByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.GetQuestionByIdAsync(228);
            act.Should().Throw<Exception>(); //TODO Exceptions
        }

        [Test]
        public async Task CreateQuestionWithEmptyAnswersAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Questions.CountAsync();
            await _service.CreateQuestionWithEmptyAnswersAsync(new QuestionDto()
            {
                Id = 10, Answers = new List<AnswerDto>(), TaskInfo = "TestTaskInfo", TopicId = 1,
                TopicName = "TestTopicName"
            });

            _context.Questions.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
        }

        [Test]
        public async Task DeleteQuestionByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Questions.CountAsync();
            await _service.DeleteQuestionByIdAsync(1);

            _context.Questions.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Questions.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public void DeleteQuestionByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.DeleteQuestionByIdAsync(228);
            act.Should().Throw<Exception>(); //TODO Exceptions
        }

        [Test]
        public async Task UpdateQuestionAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateQuestionAsync(new QuestionDto()
            {
                Id = 1,
                Answers = new List<AnswerDto>(),
                TaskInfo = "ChangedTaskInfo",
                TopicId = 1,
                TopicName = "TestTopicName"
            });

            var cartAfterUpdate = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.TaskInfo.Should().NotBe(cartBeforeUpdate.TaskInfo);
        }
    }
}