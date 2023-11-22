using AutoMapper;
using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Data;
using FiorellaBackend.Models;
using FiorellaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            List<Product> products= await _context.Products.Where(m => !m.SoftDeleted)
                                        .Include(m => m.Images)
                                        .Skip((page*take)-take)
                                        .Take(take)
                                        .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }
    }
}
