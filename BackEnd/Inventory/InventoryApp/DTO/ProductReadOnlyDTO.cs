using InventoryApp.Models;

namespace InventoryApp.DTO
{
    public class ProductReadOnlyDTO
    {
        public int ProductId { get; set; }

        public string Model { get; set; } = null!;

        public int GenderId { get; set; }

        public int TypeId { get; set; }

        public int ManufacturerId { get; set; }

        public string Description { get; set; } = "";

        public string? GenderDescription { get; set; }
        public string? TypeDescription { get; set; }
        public string? ManufacturerName { get; set; }

        public virtual ICollection<Barcode> Barcodes { get; set; } = new List<Barcode>();
    }
}
