using FiorellaBackend.Models;
using FiorellaBackend.ViewModels;

namespace FiorellaBackend.Services.Interfaces
{
    public interface IBasketService
    {

        void AddBasket(int id,Product product);
        Task<List<BasketDetailVM>> GetBasketDatas();

        int GetCount();

    }
}
