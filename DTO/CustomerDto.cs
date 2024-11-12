using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTO
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Please enter the customer name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Valid Email Address is required")]
        public string Email { get; set; }
    }
}
