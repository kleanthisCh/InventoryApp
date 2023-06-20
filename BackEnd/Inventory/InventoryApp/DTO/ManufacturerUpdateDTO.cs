using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class ManufacturerUpdateDTO
    {
        //public int ManufacturerId { get; set; }
        [Required]
        [StringLength(50)]
        public string? ManufacturerName { get; set; }


        public override string ToString()
        {
            return $" ManufacturerName = {ManufacturerName}";
        }
    }
}
