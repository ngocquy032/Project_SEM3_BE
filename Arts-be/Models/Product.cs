using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public int? ProductCategoryId { get; set; }

    public string? Title { get; set; }

    public string? SKU { get; set; }

    public string? Brands { get; set; }

    public string? CommentBrands { get; set; }


    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public decimal? Weight { get; set; }

    public string? Tags { get; set; }

    public string? Availability { get; set; }

    public decimal? Sale { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();
}
