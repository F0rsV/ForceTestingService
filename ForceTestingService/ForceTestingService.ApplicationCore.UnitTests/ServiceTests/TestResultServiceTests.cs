using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Services;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ForceTestingService.ApplicationCore.UnitTests.ServiceTests
{
    [TestFixture]
    public class TestResultServiceTests
    {
        private ITestResultService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _service = new TestResultService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetTestResultsByTestBaseIdAsync_ReturnsAllElements()
        {
            var act = await _service.GetTestResultsByTestBaseIdAsync(1);
            act.Count().Should().Be(1);
        }
    }
}