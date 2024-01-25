using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;

namespace Arts_be.Models.DTO
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Notes { get; set; }
        public string District { get; set; }
        public List<ODDTO> orderDetails { get; set; }

       
    }
}

