using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arts_be.Models;
using Arts_be.Models.DTO;
using Arts_be.Services;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Azure;

namespace Arts_be.Controllers.Payment
{

    [Route("api/[controller]")]
    [ApiController]
    public class VnpayPaymentController : ControllerBase
    {      
            private readonly EProjectContext _context;
            private readonly IVnPayService _vnPayService;

        public VnpayPaymentController(EProjectContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }

        [HttpGet("Check")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            int userId = int.Parse(HttpContext.Request.Query["UserID"]);
            var model = _vnPayService.GetPaymentModelFromCache(userId); // Corrected method call
            if (response.Success == true)
            {
                Order order = new Order
                {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetAdress = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    PaymentType = model.OrderType,
                    Country = model.Country,
                    Town = model.Town,
                    Notes = model.Notes,
                    District = model.District
                };
                _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                int orderID = order.OrderId;
                foreach (var orderDetail in model.orderDetails)
                {
                    OrdersDetail ordersDetail = new OrdersDetail
                    {
                        OrderId = orderID,
                        ProductId = orderDetail.ProductID,
                        Quantity = orderDetail.Quantity,
                        Price = orderDetail.Price,
                        OriginalPrice = orderDetail.OriginPrice,
                        Total = orderDetail.total
                    };
                    _context.OrdersDetails.AddAsync(ordersDetail);
                    await _context.SaveChangesAsync();
                   // Cập nhật lại số lượng
                    Product product = await _context.Products.FindAsync(orderDetail.ProductID);
                    if (product != null)
                    {
                        product.Quantity -= orderDetail.Quantity;
                        await _context.SaveChangesAsync();
                    }
                }

                return Redirect($"~/success-page?OrderID={orderID}");
            }
            else
            {
                // Payment failed, handle accordingly
                // Redirect to a failure page or return an error response
                return Redirect("~/fail-page");
            }
        }


    }
}