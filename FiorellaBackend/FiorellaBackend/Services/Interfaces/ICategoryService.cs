using FiorellaBackend.Models;

namespace FiorellaBackend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id,bool isTracked);

        Task<Category> GetByIdSoftDeletedAsync(int id);
        Task<Category> GetByNameAsync(string name);

        Task CreateAsync(Category category);

        Task DeleteAsync(Category category);
        Task EditAsync(Category category);

        Task SoftDeleteAsync(Category category);

        Task<List<Category>> GetArchiveDatasAsync();
        Task<List<ArchiveCategory>> GetCategoryArchivesAsync();

        Task ExtractAsync(Category category);
    }
}
