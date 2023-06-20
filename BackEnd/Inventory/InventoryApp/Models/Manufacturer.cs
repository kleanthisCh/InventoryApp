﻿using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"ManufacturerId = {ManufacturerId} - ManufacturerName = {ManufacturerName}";
    }
    
}
