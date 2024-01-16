using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int? UserId { get; set; }

    public string? NameBlog { get; set; }

    public string? Images { get; set; }

    public string? NameCategory { get; set; }

    public string? Content { get; set; }

    public DateTime created_at { get; set; }

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual User? User { get; set; }
}
