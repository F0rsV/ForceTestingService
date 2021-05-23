using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForceTestingService.WEB.Controllers
{
//    [Authorize(Roles = "teacher")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITopicService _topicService;
        public QuestionController(IQuestionService questionService, ITopicService topicService)
        {
            _questionService = questionService;
            _topicService = topicService;
        }

        public async Task<IActionResult> Index(int topicId)
        {
            var model = await _questionService.GetAllQuestionsOfSpecificTopicAsync(topicId);
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            ViewBag.Topic = topic;
            return View(model);
        }

        public async Task<IActionResult>Create(int topicId)
        {
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            ViewBag.Topic = topic;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
                await _questionService.CreateQuestionWithEmptyAnswersAsync(questionDto);
                return RedirectToAction("Index", new { topicId = questionDto.TopicId });
            }

            var topic = await _topicService.GetTopicByIdAsync(questionDto.TopicId);
            ViewBag.Topic = topic;
            return View(questionDto);
        }

        public async Task<IActionResult> Edit(int id, int topicId)
        {
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            ViewBag.Topic = topic;

            var questionDtoToEdit = await _questionService.GetQuestionByIdAsync(id);
            return View(questionDtoToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(QuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
                await _questionService.UpdateQuestionAsync(questionDto);
                return RedirectToAction("Index", new { topicId = questionDto.TopicId });
            }

            var topic = await _topicService.GetTopicByIdAsync(questionDto.TopicId);
            ViewBag.Topic = topic;
            return View(questionDto);
        }

        public async Task<IActionResult> Delete(int id, int topicId)
        {
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            ViewBag.Topic = topic;

            var questionDtoToDelete = await _questionService.GetQuestionByIdAsync(id);
            return View(questionDtoToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionDto = await _questionService.GetQuestionByIdAsync(id);

            await _questionService.DeleteQuestionByIdAsync(id);
            return RedirectToAction("Index", new { topicId = questionDto.TopicId });
        }
    }
}
