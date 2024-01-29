using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arts_be.Models;
using Arts_be.Models.DTO;
using Arts_be.Helpter;
using Arts_be.Services;

namespace Arts_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
   
        private readonly EProjectContext _context;

        private readonly IEmailService emailService;

        public OrdersController(EProjectContext context, IEmailService service)
        {
            _context = context;
            this.emailService = service;
        }

        [HttpPost("SendMail")]

        public async Task<IActionResult> SendMail()
        {
            try {
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = "quynnth2208032@fpt.edu.vn";
                mailrequest.Subject = "Welcome to Arts Shop";
                mailrequest.Body = GetHtmlcontent();
                await emailService.SendEmailAsync(mailrequest);
                return Ok();
            }
            catch (Exception ex) {
                throw;
            }
        }

        private string GetHtmlcontent()
        {
            string Response = "<div style=\"width:100%;background-color:lightblue;text-align:center;margin:10px\">";
            Response += "<h1>Welcome to Nihira Techiees</h1>";
            Response += "<img src=\"https://yt3.googleusercontent.com/v5hyLB4am6E0GZ3y-JXVCxT9g8157eSeNggTZKkWRSfq_B12sCCiZmRhZ4JmRop-nMA18D2IPw=s176-c-k-c0x00ffffff-no-rj\" />";
            Response += "<h2>Thanks for subscribed us</h2>";
            Response += "<a href=\"https://www.youtube.com/channel/UCsbmVmB_or8sVLLEq4XhE_A/join\">Please join membership by click the link</a>";
            Response += "<div><h1> Contact us : nihiratechiees@gmail.com</h1></div>";
            Response += "</div>";
            return Response;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentInformationModel>> PostOrder(PaymentInformationModel model)
        {
            Order order = new Order
            {
                UserId = model.UserID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAdress = model.Address,
                Email = model.Email,
                Phone = model.Phone,
                PaymentType = model.OrderType,
                Country = model.Country,
                Town = model.Town,
                Notes = model.Notes,
                District = model.District,
                OrderStatus = "Waiting for confirmation",
                Amount = model.Amount,
             
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
                    Title = orderDetail.Title,
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

            return model;
        }



        // DELETE: api/Orders/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        // GET: api/Orders/orderDTO

        [HttpGet("orderDTO")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersDTO()
        {
            try
            {
                var result = await _context.Orders
                    .Include(o => o.OrdersDetails)
                    .Select(o => new OrderDTO
                    {
                        OrderId = o.OrderId,
                        UserId = o.UserId,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        StreetAdress = o.StreetAdress,
                        Email = o.Email,
                        Phone = o.Phone,
                        PaymentType = o.PaymentType,
                        Amount = o.Amount,
                        Country = o.Country,
                        Town = o.Town,
                        Notes = o.Notes,
                        District = o.District,
                        OrderStatus = o.OrderStatus,
                        OrdersDetails = o.OrdersDetails.Select(od => new OrdersDetail
                        {
                            ProductId = od.ProductId,
                            Title = od.Title,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = od.OriginalPrice,
                            Total = od.Total
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("orderDTO/{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrderDTO(int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrdersDetails)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    return NotFound();
                }

                var orderDTO = new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    StreetAdress = order.StreetAdress,
                    Email = order.Email,
                    Phone = order.Phone,
                    PaymentType = order.PaymentType,
                    Amount = order.Amount,
                    Country = order.Country,
                    Town = order.Town,
                    Notes = order.Notes,
                    District = order.District,
                    OrderStatus = order.OrderStatus,
                    OrdersDetails = order.OrdersDetails.Select(od => new OrdersDetail
                    {
                        ProductId = od.ProductId,
                        Title = od.Title,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        OriginalPrice = od.OriginalPrice,
                        Total = od.Total
                    }).ToList()
                };

                return Ok(orderDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
