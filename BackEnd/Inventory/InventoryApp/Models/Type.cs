using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public string TypeDescription { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
