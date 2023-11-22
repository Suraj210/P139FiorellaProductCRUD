using FiorellaBackend.Areas.Admin.ViewModels.Category;
using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Data;
using FiorellaBackend.Helpers;
using FiorellaBackend.Models;
using FiorellaBackend.Services;
using FiorellaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {

            List<CategoryVM> dbPaginatedDatas = await _categoryService.GetPaginatedDatasAsync(page, take);


            int pageCount = await GetPageCountAsync(take);

            Paginate<CategoryVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int pageCount = await _categoryService.GetCountAsync();

            var result = (int)Math.Ceiling((decimal)(pageCount) / take);

            return result;
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            Category existCategory = await _categoryService.GetByNameAsync(category.Name);

            if (existCategory is not null)
            {
                ModelState.AddModelError("Name", "This name is already exists");
                return View();
            }

           await _categoryService.CreateAsync(category);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category dbCategory = await _categoryService.GetByIdAsync(id,true);

           await _categoryService.DeleteAsync(dbCategory);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Category dbCategory = await _categoryService.GetByIdAsync(id, true);

            await _categoryService.SoftDeleteAsync(dbCategory);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetByIdAsync((int)id,false);

            if (category is null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id,Category category)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }

            Category dbCategory = await _categoryService.GetByIdAsync((int)id,false);

            if (dbCategory is null) return NotFound();

            if(category.Name.Trim()== dbCategory.Name.Trim()) return RedirectToAction(nameof(Index));

            Category existCategory = await _categoryService.GetByNameAsync(category.Name);

            if (existCategory is not null)
            {
                ModelState.AddModelError("Name", "This name is already exists");
                return View(category);
            }

            await _categoryService.EditAsync(category);

            return RedirectToAction("Index");

        }
    }
}
