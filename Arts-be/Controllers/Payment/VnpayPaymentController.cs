using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arts_be.Models;
using Arts_be.Models.DTO;
using Arts_be.Services;

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

        [HttpGet]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Redirect("https://localhost:7055/api/VnpayPayment" + response.OrderId);
        }


    }
}