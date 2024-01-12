using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? StreetAddress { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public string? Description { get; set; }

    public string? PostcodeZip { get; set; }

    public string? Level { get; set; }

    public string? Country { get; set; }

    public string? Town { get; set; }

    public string? District { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
