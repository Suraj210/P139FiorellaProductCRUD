using AutoMapper;
using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Data;
using FiorellaBackend.Helpers.Extentions;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context,IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _context.Products.Where(m => m.Id == id).Include(m=>m.Category).Include(m=>m.Images).FirstOrDefaultAsync();
           

            foreach (var item in product.Images)
            {

                string path = _env.GetFilePath("img/", item.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var data = await _context.Products.Include(m=>m.Category).Include(m=>m.Images).ToListAsync();

            return _mapper.Map<List<ProductVM>>(data);
        }

        public async Task<Product> GetAllByIdWithIncludeAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted && m.Id == id)
                                                              .Include(m => m.Images)
                                                              .Include(m => m.Category)
                                                              .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllByTakeWithImagesAsync(int take)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                          .Include(m => m.Images)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)=> await _context.Products.FindAsync(id);

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products= await _context.Products
                                        .Include(m => m.Images)
                                        .Include(m=>m.Category)
                                        .Skip((page*take)-take)
                                        .Take(take)
                                        .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }
    }
}
