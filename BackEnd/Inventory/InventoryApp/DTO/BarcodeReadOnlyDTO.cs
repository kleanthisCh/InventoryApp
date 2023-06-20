namespace InventoryApp.DTO
{
    public class BarcodeReadOnlyDTO
    {
        public string BarcodeId { get; set; } = null!;

        public int Quantity { get; set; }

        public string Size { get; set; } = null!;

        public int ProductId { get; set; }

        public string Model { get; set; } = null!;
    }
}
