using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.Services;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FluentAssertions;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;

namespace ForceTestingService.ApplicationCore.UnitTests.ServiceTests
{
    [TestFixture]
    public class SubjectServiceTests
    {
        private ISubjectService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new SubjectService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllSubjectsAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Subjects.CountAsync();
            _service.GetAllSubjectsAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetSubjectByIdAsync_ItemExists_ReturnsSubjectDto()
        {
            var act = await _service.GetSubjectByIdAsync(1);
            act.Should().BeOfType<SubjectDto>();
        }

        [Test]
        public void GetSubjectByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.GetSubjectByIdAsync(228);
            act.Should().Throw<Exception>(); //TODO Exceptions
        }

        [Test]
        public async Task CreateSubjectAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Subjects.CountAsync();
            await _service.CreateSubjectAsync(new SubjectDto() {Id = 10, Name = "SubjectDtoName01", Topics = new List<TopicDto>()});

            _context.Subjects.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
        }

        [Test]
        public async Task DeleteSubjectByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Subjects.CountAsync();
            await _service.DeleteSubjectByIdAsync(1);

            _context.Subjects.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Subjects.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public void DeleteSubjectByIdAsync_ItemDoesNotExist_ThrowsException()
        {
            Func<Task> act = async () => await _service.DeleteSubjectByIdAsync(228);
            act.Should().Throw<Exception>(); //TODO Exceptions
        }

        [Test]
        public async Task UpdateSubjectAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateSubjectAsync(new SubjectDto() { Id = 1, Name = "ChangedName", Topics = new List<TopicDto>()});

            var cartAfterUpdate = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.Name.Should().NotBe(cartBeforeUpdate.Name);
        }
    }
}
