using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Model { get; set; } = null!;

    public int GenderId { get; set; }

    public int TypeId { get; set; }

    public int ManufacturerId { get; set; }

    public string Description { get; set; } = "";

    public virtual ICollection<Barcode> Barcodes { get; set; } = new List<Barcode>();

    public virtual Gender Gender { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;
}
