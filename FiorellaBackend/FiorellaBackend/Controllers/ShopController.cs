using FiorellaBackend.Data;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using FiorellaBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorellaBackend.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShopController(AppDbContext context, IProductService productService, IBasketService basketService)
        {
            _context = context;
            _productService = productService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {

            int productsCount = _context.Products.Where(m => !m.SoftDeleted).Count();

            ViewBag.ProductCount = productsCount;

            List<Product> products =await _context.Products.Where(m=>!m.SoftDeleted).Include(m=>m.Images).Take(3).ToListAsync();         

            return View(products);
        }

        public async Task<IActionResult> ShowMore(int skipCount)
        {
           

            List<Product> products = await _context.Products.Where(m => !m.SoftDeleted)
                                                            .Include(m=>m.Images)
                                                            .Skip(skipCount)
                                                            .Take(1)
                                                            .ToListAsync();

            return PartialView("_ProductsPartial",products);
            

        }


        public async Task<IActionResult> ShowLess()
        {

            List<Product> products = await _context.Products.Where(m => !m.SoftDeleted)
                                                            .Include(m => m.Images)
                                                            .Take(3)
                                                            .ToListAsync();

            return PartialView("_ProductsPartial", products);

        }



        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();


            _basketService.AddBasket((int)id, product);

            return Ok();
        }

    }
}
