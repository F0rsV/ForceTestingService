using System;
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
    public class AnswerServiceTests
    {
        private IAnswerService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new AnswerService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllAnswersOfSpecificQuestionAsync_Returns1Element()
        {
            var act = await _service.GetAllAnswersOfSpecificQuestionAsync(1);
            act.Count().Should().Be(1);
        }

        [Test]
        public async Task GetAnswerByIdAsync_ItemExists_ReturnsAnswerDto()
        {
            var act = await _service.GetAnswerByIdAsync(1);
            act.Should().BeOfType<AnswerDto>();
        }

        [Test]
        public void GetAnswerByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.GetAnswerByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task CreateAnswerAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Answers.CountAsync();
            await _service.CreateAnswerAsync(new AnswerDto()
            {
                Id = 10,
                IsCorrect = true,
                QuestionId = 1,
                Text = "TestAnswerText"
            });

            _context.Answers.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
        }

        [Test]
        public async Task DeleteAnswerByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Answers.CountAsync();
            await _service.DeleteAnswerByIdAsync(1);

            _context.Answers.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Answers.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public void DeleteAnswerByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.DeleteAnswerByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task UpdateAnswerAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateAnswerAsync(new AnswerDto()
            {
                Id = 1,
                IsCorrect = true,
                QuestionId = 1,
                Text = "ChangedAnswerText"
            });

            var cartAfterUpdate = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.Text.Should().NotBe(cartBeforeUpdate.Text);
        }
    }
}