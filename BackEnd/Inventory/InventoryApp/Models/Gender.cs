using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderDescription { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
