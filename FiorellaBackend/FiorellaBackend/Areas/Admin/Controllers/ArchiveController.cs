using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorellaBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly ICategoryService _categoryService;

        public ArchiveController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Category()
        {

            return View(await _categoryService.GetCategoryArchivesAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Extract(int id)
        {
            Category dbCategory = await _categoryService.GetByIdSoftDeletedAsync(id);

            await _categoryService.ExtractAsync(dbCategory);

            return RedirectToAction("Category");
        }
    }
}
