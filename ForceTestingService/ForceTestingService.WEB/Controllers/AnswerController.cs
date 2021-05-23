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
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        public async Task<IActionResult> Index(int questionId)
        {
            var model = await _answerService.GetAllAnswersOfSpecificQuestionAsync(questionId);
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            ViewBag.Question = question;

            return View(model);
        }

        public async Task<IActionResult> Create(int questionId)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            ViewBag.Question = question;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AnswerDto answerDto)
        {
            if (ModelState.IsValid)
            {
                await _answerService.CreateAnswerAsync(answerDto);
                return RedirectToAction("Index", new { questionId = answerDto.QuestionId });
            }

            var question = await _questionService.GetQuestionByIdAsync(answerDto.QuestionId);
            ViewBag.Question = question;
            return View(answerDto);
        }

        public async Task<IActionResult> Edit(int id, int questionId)
        {;
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            ViewBag.Question = question;

            var answerDtoToEdit = await _answerService.GetAnswerByIdAsync(id);
            return View(answerDtoToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AnswerDto answerDto)
        {
            if (ModelState.IsValid)
            {
                await _answerService.UpdateAnswerAsync(answerDto);
                return RedirectToAction("Index", new { questionId = answerDto.QuestionId });
            }

            var question = await _questionService.GetQuestionByIdAsync(answerDto.QuestionId);
            ViewBag.Question = question;
            return View(answerDto);
        }

        public async Task<IActionResult> Delete(int id, int questionId)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            ViewBag.Question = question;

            var answerDtoToDelete = await _answerService.GetAnswerByIdAsync(id);
            return View(answerDtoToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerDto = await _answerService.GetAnswerByIdAsync(id);

            await _answerService.DeleteAnswerByIdAsync(id);
            return RedirectToAction("Index", new { questionId = answerDto.QuestionId });
        }
    }
}
