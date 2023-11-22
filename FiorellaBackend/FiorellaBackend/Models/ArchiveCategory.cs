using System.ComponentModel.DataAnnotations;

namespace FiorellaBackend.Models
{
    public class ArchiveCategory
    {
        [Required(ErrorMessage = "Can't be empty")]
        [MaxLength(10, ErrorMessage = "Max length must be 10 symbols")]
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public int Id { get; set; }
    }
}
