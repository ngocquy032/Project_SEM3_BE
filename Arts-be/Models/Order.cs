using System;
using System.Collections.Generic;

namespace Arts_be.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? StreetAdress { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PaymentType { get; set; }

    public string? Country { get; set; }

    public string? Town { get; set; }

    public string? Notes { get; set; }

    public string? District { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual User? User { get; set; }
}
