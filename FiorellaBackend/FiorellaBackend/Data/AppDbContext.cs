using FiorellaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorellaBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        public DbSet<SliderImg> SliderImgs { get; set; }

        public DbSet<SliderInfos> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<AboutMain> AboutMains { get; set; }

        public DbSet<AboutFeature> AboutFeatures { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Say> Says { get; set; }
        public DbSet<Instagram> InstagramPhotos { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ArchiveCategory> ArchiveCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
        }


    }
}
