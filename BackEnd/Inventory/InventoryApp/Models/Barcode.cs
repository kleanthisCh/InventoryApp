using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Barcode
{
    public string BarcodeId { get; set; } = null!;

    public int Quantity { get; set; }

    public string Size { get; set; } = null!;

    public int ProductId { get; set; }

    public string Model { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
