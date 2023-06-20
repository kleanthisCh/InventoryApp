using InventoryApp.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class ProductCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Model { get; set; } = null!;

        [Required]
        [Range(1, 9)]
        public int GenderId { get; set; }

        [Required]
        [Range(1, 3)]
        public int TypeId { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; } = "";
    }
}
