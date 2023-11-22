using FiorellaBackend.Data;
using FiorellaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderInfoController : Controller
    {


        private readonly AppDbContext _context;

        public SliderInfoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            SliderInfos sliderInfos =await _context.SliderInfos.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
            return View(sliderInfos);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderInfos sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            return View(sliderInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            SliderInfos sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            return View(sliderInfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}
