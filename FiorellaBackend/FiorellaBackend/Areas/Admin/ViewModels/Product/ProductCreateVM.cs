using System.ComponentModel.DataAnnotations;

namespace FiorellaBackend.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
