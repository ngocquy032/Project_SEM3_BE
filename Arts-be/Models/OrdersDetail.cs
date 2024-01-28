using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class OrdersDetail
{
    public int OrderDetailsId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }
    public string Title { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public decimal? OriginalPrice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
