using FiorellaBackend.Data;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using FiorellaBackend.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public HomeController(AppDbContext context, IProductService productService, IBasketService basketService)
        {
            _context = context;
            _productService = productService;
            _basketService = basketService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderImg> sliderImg =await _context.SliderImgs.ToListAsync();
            SliderInfos sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();
            List<Blog> blog = await _context.Blogs.Where(m => !m.SoftDeleted).ToListAsync();
            List<Product> product = await _productService.GetAllByTakeWithImagesAsync(8);
            List<Category> categories= await _context.Categories.Where(m=>!m.SoftDeleted).ToListAsync();
            AboutMain aboutMain = await _context.AboutMains.Where(m=>!m.SoftDeleted).FirstOrDefaultAsync();
            List<AboutFeature> aboutFeatures= await _context.AboutFeatures.Where(m => !m.SoftDeleted).ToListAsync();
            List<Expert> experts= await _context.Experts.Where(m => !m.SoftDeleted).ToListAsync();
            Subscribe subscribes = await _context.Subscribes.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
            List<Say> says = await _context.Says.Where(m=>!m.SoftDeleted).ToListAsync() ;
            List<Instagram> instaPhotos = await _context.InstagramPhotos.Where(m => !m.SoftDeleted).ToListAsync();

            HomeVM modules = new()
            {
                SliderImgs = sliderImg,
                SliderInfos = sliderInfo,
                Blogs = blog,
                Products =product,
                Categories=categories,
                AboutMains=aboutMain,
                AboutFeatures=aboutFeatures,
                Experts=experts,
                Subscribes=subscribes,
                Says=says,
                InstagramPhotos=instaPhotos
            };

            return View(modules);
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