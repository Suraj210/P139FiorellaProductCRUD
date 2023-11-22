using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using FiorellaBackend.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorellaBackend.Services
{
    public class BasketService : IBasketService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;

        public BasketService( IHttpContextAccessor contextAccessor, IProductService productService)
        {
            
            _contextAccessor = contextAccessor;
            _productService = productService;

        }
        public void AddBasket(int id,Product product)
        {
            List<BasketVM> basket;

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }


            BasketVM existProduct = basket.Find(x => x.Id == product.Id);
            if (existProduct is null)
            {
                basket.Add(new BasketVM { Id = product.Id, Count = 1, Price = product.Price });

            }
            else
            {
                existProduct.Count++;
            }

            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public async Task<List<BasketDetailVM>> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            List<BasketDetailVM> basketDetailList = new();

            foreach (var item in basket)
            {
                Product existProduct = await _productService.GetAllByIdWithIncludeAsync(item.Id);
                basketDetailList.Add(new BasketDetailVM
                {

                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Description = existProduct.Description,
                    Price = existProduct.Price,
                    Count = item.Count,
                    Total = existProduct.Price * item.Count,
                    Category = existProduct.Category.Name,
                    Image = existProduct.Images.FirstOrDefault(m => m.IsMain).Image
                });

            };

            return basketDetailList;
        }

        public int GetCount()
        {
            List<BasketVM> basket;

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m=>m.Count);
        }
    }
}
