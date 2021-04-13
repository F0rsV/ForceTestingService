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
    [Authorize(Roles = "teacher")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _subjectService.GetAllSubjectsAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubjectDto subjectDto)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.CreateSubjectAsync(subjectDto);
                return RedirectToAction(nameof(Index));
            }
            return View(subjectDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subjectDtoToEdit = await _subjectService.GetSubjectByIdAsync(id);
            return View(subjectDtoToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubjectDto subjectDto)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.UpdateSubjectAsync(subjectDto);
                return RedirectToAction(nameof(Index));
            }
            return View(subjectDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subjectDtoToDelete = await _subjectService.GetSubjectByIdAsync(id);
            return View(subjectDtoToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subjectService.DeleteSubjectByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
