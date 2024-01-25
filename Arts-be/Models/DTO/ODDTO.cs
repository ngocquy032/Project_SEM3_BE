using System.Reflection;

namespace Arts_be.Models.DTO
{
    public class ODDTO
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal OriginPrice { get; set; }
        public decimal total { get; set; }
    }
}
