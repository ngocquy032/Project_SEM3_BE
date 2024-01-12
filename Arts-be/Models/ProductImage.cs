using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class ProductImage
{
    public int ProductImagesId { get; set; }

    public string? Path { get; set; }

    public string? Description { get; set; }
}
