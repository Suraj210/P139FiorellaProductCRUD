using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Data;
using FiorellaBackend.Helpers;
using FiorellaBackend.Helpers.Extentions;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public ProductController(IProductService productService
                                ,ICategoryService categoryService
                                ,IWebHostEnvironment env
                                ,AppDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Index(int page=1,int take=4)
        {

            List<ProductVM> dbPaginatedDatas =await _productService.GetPaginatedDatasAsync(page, take);


            int pageCount =await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas=new(dbPaginatedDatas,page,pageCount);
            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int pageCount = await _productService.GetCountAsync();

            var result = (int)Math.Ceiling((decimal)(pageCount) / take);

            return result;
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetAllByIdWithIncludeAsync((int)id);

            if(product == null) return NotFound();

            return View(product);

        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            ViewBag.categories = await GetCategoriesAsync();

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            foreach (var photo in product.Photos)
            {

                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only image format");
                    return View(product);
                }

                if (!photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200 kb");
                    return View(product);
                }
            }

            List<ProductImage> newImages = new();

            foreach (var photo in product.Photos)
            {
                string fileName = $"{Guid.NewGuid()}-{photo.FileName}";

                string path = _env.GetFilePath("img", fileName);

                await photo.SaveFileAsync(path);

                newImages.Add(new ProductImage { Image = fileName });
            }

            newImages.FirstOrDefault().IsMain=true;

            await _context.ProductImages.AddRangeAsync(newImages);

            await _context.Products.AddAsync(new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Images=newImages

            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        private async Task<SelectList> GetCategoriesAsync()
        {
            return new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
        }
    }
}
