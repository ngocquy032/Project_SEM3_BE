// File: OrderDTO.cs
namespace Arts_be.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAdress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PaymentType { get; set; }

        public decimal? Amount { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Notes { get; set; }
        public string District { get; set; }
        public string OrderStatus { get; set; }

        public object OrdersDetails { get; internal set; }
    }
}
