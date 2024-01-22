// File: OrderDTO.cs
namespace Arts_be.Models.DTO
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Street_Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Payment_Type { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Notes { get; set; }
        public string District { get; set; }
        
    }
}
