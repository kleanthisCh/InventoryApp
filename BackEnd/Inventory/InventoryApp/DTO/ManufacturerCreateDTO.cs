using InventoryApp.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class ManufacturerCreateDTO
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {1} characters")]
        public string? ManufacturerName { get; set; }

        public override string ToString()
        {
            return $" ManufacturerName = {ManufacturerName}";
        }
    }
}
