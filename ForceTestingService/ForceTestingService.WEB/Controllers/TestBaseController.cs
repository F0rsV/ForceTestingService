using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForceTestingService.WEB.Controllers
{
//    [Authorize(Roles = "teacher")]
    public class TestBaseController : Controller
    {
        private readonly ITestBaseService _testBaseService;
        private readonly ITopicService _topicService;
        private readonly ITestResultService _testResultService;
        public TestBaseController(ITestBaseService testBaseService, ITopicService topicService, ITestResultService testResultService)
        {
            _testBaseService = testBaseService;
            _topicService = topicService;
            _testResultService = testResultService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _testBaseService.GetAllTestBasesAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            ViewData["TopicId"] = new SelectList(topics, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TestBaseDto testBaseDto)
        {
            if (ModelState.IsValid)
            {
                await _testBaseService.CreateTestBaseAsync(testBaseDto);
                return RedirectToAction(nameof(Index));
            }

            var topics = await _topicService.GetAllTopicsAsync();
            ViewData["TopicId"] = new SelectList(topics, "Id", "Name", testBaseDto.TopicId);
            return View(testBaseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var topics = await _topicService.GetAllTopicsAsync();
            ViewData["TopicId"] = new SelectList(topics, "Id", "Name");

            var testBaseDtoToEdit = await _testBaseService.GetTestBaseByIdAsync(id);
            return View(testBaseDtoToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TestBaseDto testBaseDto)
        {
            if (ModelState.IsValid)
            {
                await _testBaseService.UpdateTestBaseAsync(testBaseDto);
                return RedirectToAction(nameof(Index));
            }

            var topics = await _topicService.GetAllTopicsAsync();
            ViewData["TopicId"] = new SelectList(topics, "Id", "Name", testBaseDto.TopicId);
            return View(testBaseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var testBaseDtoToDelete = await _testBaseService.GetTestBaseByIdAsync(id);
            return View(testBaseDtoToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _testBaseService.DeleteTestBaseByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowResults(int id)
        {
            var model = await _testResultService.GetTestResultsByTestBaseIdAsync(id);
            return View(model);
        }
    }
}
