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
    public class TestBaseServiceTests
    {
        private ITestBaseService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new TestBaseService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllTestBasesAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.TestBases.CountAsync();
            _service.GetAllTestBasesAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetTestBaseByIdAsync_ItemExists_ReturnsTestBaseDto()
        {
            var act = await _service.GetTestBaseByIdAsync(1);
            act.Should().BeOfType<TestBaseDto>();
        }

        [Test]
        public void GetTestBaseByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.GetTestBaseByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task CreateTestBaseAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.TestBases.CountAsync();
            await _service.CreateTestBaseAsync(new TestBaseDto()
            {
                Id = 10,
                AmountOfSeconds = 100,
                Name = "TestName",
                NumOfQuestions = 5,
                NumOfTries = 3,
                TopicId = 1,
                TopicName = "TestTopicName"
            });

            _context.TestBases.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
        }

        [Test]
        public async Task DeleteTestBaseByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.TestBases.CountAsync();
            await _service.DeleteTestBaseByIdAsync(1);

            _context.TestBases.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.TestBases.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public void DeleteTestBaseByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.DeleteTestBaseByIdAsync(228);
            act.Should().Throw<Exception>();
        }

        [Test]
        public async Task UpdateTestBaseAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.TestBases.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateTestBaseAsync(new TestBaseDto()
            {
                Id = 1,
                AmountOfSeconds = 100,
                Name = "ChangedTestName",
                NumOfQuestions = 5,
                NumOfTries = 3,
                TopicId = 1,
                TopicName = "TestTopicName"
            });

            var cartAfterUpdate = await _context.TestBases.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.Name.Should().NotBe(cartBeforeUpdate.Name);
        }
    }
}