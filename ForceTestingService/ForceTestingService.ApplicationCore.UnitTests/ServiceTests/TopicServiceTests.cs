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
    public class TopicServiceTests
    {
        private ITopicService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new TopicService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllTopicsAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Topics.CountAsync();
            _service.GetAllTopicsAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetTopicByIdAsync_ItemExists_ReturnsTopicDto()
        {
            var act = await _service.GetTopicByIdAsync(1);
            act.Should().BeOfType<TopicDto>();
        }

        [Test]
        public void GetTopicByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.GetTopicByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task CreateTopicAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Topics.CountAsync();
            await _service.CreateTopicAsync(new TopicDto()
            {
                Id = 10, Name = "TopicNameTest01", Questions = new List<QuestionDto>(), SubjectId = 1,
                SubjectName = "SubjName"
            });

            _context.Topics.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
        }

        [Test]
        public async Task DeleteTopicByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Topics.CountAsync();
            await _service.DeleteTopicByIdAsync(1);

            _context.Topics.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Topics.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public void DeleteTopicByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.DeleteTopicByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task UpdateTopicAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.Topics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateTopicAsync(new TopicDto()
            {
                Id = 1, Name = "SomeChangedName", Questions = new List<QuestionDto>(), SubjectId = 1,
                SubjectName = "SubjName"
            });

            var cartAfterUpdate = await _context.Topics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.Name.Should().NotBe(cartBeforeUpdate.Name);
        }
    }
}