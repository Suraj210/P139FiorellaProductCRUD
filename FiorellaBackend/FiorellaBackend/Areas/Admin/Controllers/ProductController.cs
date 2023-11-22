using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorellaBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page=1,int take=4)
        {

            List<ProductVM> paginatedDatas =await _productService.GetPaginatedDatasAsync(page, take);

            int pageeCount = await _productService.GetCountAsync();

            ViewBag.count = pageeCount/take;
            return View(paginatedDatas);
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetAllByIdWithIncludeAsync((int)id);

            if(product == null) return NotFound();

            return View(product);

        }
    }
}
