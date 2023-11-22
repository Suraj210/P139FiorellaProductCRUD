using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Models;

namespace FiorellaBackend.Services.Interfaces
{
    public interface IProductService
    {

        Task<List<ProductVM>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);

        Task<List<Product>> GetAllByTakeWithImagesAsync(int take);
        Task<Product> GetAllByIdWithIncludeAsync(int id);

        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);

        Task<int> GetCountAsync();
    }
}
