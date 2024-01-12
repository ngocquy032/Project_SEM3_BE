using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class BlogComment
{
    public int BlogCommentId { get; set; }

    public int? BlogId { get; set; }

    public int? UserId { get; set; }

    public string? Email { get; set; }

    public string? NameTitle { get; set; }

    public string? Messages { get; set; }

    public virtual Blog? Blog { get; set; }

    public virtual User? User { get; set; }
}
