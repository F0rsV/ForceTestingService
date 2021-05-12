using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Models;
using ForceTestingService.ApplicationCore.Services;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace ForceTestingService.ApplicationCore.UnitTests.ServiceTests
{
    [TestFixture]
    public class StudentTestServiceTests
    {
        private IStudentTestService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new StudentTestService(new UnitOfWork(_context));
        }

        [Test]
        public async Task CreateStudentTestsListForUserAsync_Returns2Elements()
        {
            var user = await _context.Users.FindAsync(1);
            var act = await _service.CreateStudentTestsListForUserAsync(user);
            act.Count().Should().Be(2);
        }

        [Test]
        public async Task GetTestResultsForUserAsync_Returns1Elements()
        {
            var user = await _context.Users.FindAsync(1);
            var act = await _service.GetTestResultsForUserAsync(user, 1);
            act.Count().Should().Be(1);
        }

        [Test]
        public async Task SubmitTestAsync_AddsNewTestResultElement()
        {
            var user = await _context.Users.FindAsync(1);
            var craftedTest = new CraftedTest()
            {
                Date = DateTime.Now,
                Questions = new List<QuestionDto>(),
                Score = 10,
                TestBaseDto = new TestBaseDto(),
                User = user
            };

            await _service.SubmitTestAsync(craftedTest);

            _context.TestResults.Count().Should().Be(3);
        }
    }
}