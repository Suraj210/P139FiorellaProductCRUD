using FiorellaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorellaBackend.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<IActionResult> Index() {


            return View(await _basketService.GetBasketDatas());
        
        
        }
     
    }
}
