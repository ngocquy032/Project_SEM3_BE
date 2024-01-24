namespace Arts_be.Models.DTO
{
    public class CashOrderDTO
    {
        public UserDataDTO User { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class UserDataDTO
    {
       public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Country { get; set; }

        public string District { get; set; }

        public string StreetAddress { get; set; }
    }

    public class CartItemDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}
