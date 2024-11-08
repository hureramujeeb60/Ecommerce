using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter the Category name")]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Product>? Product { get; set; }
    }
}
