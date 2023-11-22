using FiorellaBackend.Data;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category category)
        {

            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id, bool isTracked)
        {
            return isTracked ? await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) : await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Category>> GetArchiveDatasAsync()
        {
            return await _context.Categories.IgnoreQueryFilters().Where(m=>m.SoftDeleted).ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task SoftDeleteAsync(Category category)
        {
            category.SoftDeleted=true;
            await _context.SaveChangesAsync();
        }

        public async Task ExtractAsync(Category category)
        {
            category.SoftDeleted = false;
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetByIdSoftDeletedAsync(int id)
        {
            var data = await _context.Categories.Where(m => m.SoftDeleted).FirstOrDefaultAsync(c => c.Id == id);
            return data;
        }

        public async Task<List<ArchiveCategory>> GetCategoryArchivesAsync()
        {
            return await _context.ArchiveCategories.ToListAsync();
        }
    }
}
