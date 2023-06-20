using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class GenderUpdateDTO
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(20, ErrorMessage = "The {0} field must be maximum of {1} characters")]
        public string GenderDescription { get; set; } = null!;
    }
}
