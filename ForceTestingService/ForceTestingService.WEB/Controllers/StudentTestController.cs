using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Models;
using ForceTestingService.Infrastructure.Entities;
using ForceTestingService.WEB.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForceTestingService.WEB.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentTestController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IStudentTestService _studentTestService;

        public StudentTestController(UserManager<User> userManager, IStudentTestService studentTestService)
        {
            _userManager = userManager;
            _studentTestService = studentTestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTests()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _studentTestService.CreateStudentTestsListForUserAsync(user);
            return View(model);
        }

        public async Task<IActionResult> ViewTestResults(int testBaseId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _studentTestService.GetTestResultsForUserAsync(user, testBaseId);
            return View(model);
        }

        public async Task<IActionResult> PassTest(int testBaseId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _studentTestService.CraftRandomTestFromTestBaseAsync(user, testBaseId);
            if (model == null)
            {
                return RedirectToAction("ViewTestResults", new { testBaseId });
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PassTest(CraftedTest craftedTest)
        {
            craftedTest.User = await _userManager.GetUserAsync(HttpContext.User);
            await _studentTestService.SubmitTestAsync(craftedTest);
            return RedirectToAction(nameof(GetTests));
        }
    }
}
