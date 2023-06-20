using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class BarcodeUpdateDTO
    {

        [Required(ErrorMessage = "The {0} field is required")]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {1} characters")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string Size { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be maximum of {1} characters")]
        public string Model { get; set; } = null!;
    }
}
