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
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly ISubjectService _subjectService;
        public TopicController(ITopicService topicService, ISubjectService subjectService)
        {
            _topicService = topicService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _topicService.GetAllTopicsAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TopicDto topicDto)
        {
            if (ModelState.IsValid)
            {
                await _topicService.CreateTopicAsync(topicDto);
                return RedirectToAction(nameof(Index));
            }

            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Name", topicDto.SubjectId);
            return View(topicDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Name");

            var topicDtoToEdit = await _topicService.GetTopicByIdAsync(id);
            return View(topicDtoToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TopicDto topicDto)
        {
            if (ModelState.IsValid)
            {
                await _topicService.UpdateTopicAsync(topicDto);
                return RedirectToAction(nameof(Index));
            }

            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Name", topicDto.SubjectId);
            return View(topicDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var topicDtoToDelete = await _topicService.GetTopicByIdAsync(id);
            return View(topicDtoToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _topicService.DeleteTopicByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
