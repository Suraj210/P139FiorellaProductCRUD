using FiorellaBackend.Data;
using FiorellaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            IEnumerable<SliderImg> sliderImgs =await _context.SliderImgs.Where(m=>!m.SoftDeleted).ToListAsync();
            return View(sliderImgs);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id) 
        {
            if (id is null) return BadRequest();

            SliderImg sliderImg =await _context.SliderImgs.FirstOrDefaultAsync(m=>m.Id == id);

            if (sliderImg is null) return NotFound();

            return View(sliderImg); 
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            SliderImg sliderImg = await _context.SliderImgs.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderImg is null) return NotFound();

            return View(sliderImg);
        }

    }
}
