using Arts_be.Libraries;
using Arts_be.Models.DTO;
using Arts_be.Services;
using Arts_be.Models;
using Newtonsoft.Json;
using System.Text;
using System.Web;
using Microsoft.Extensions.Caching.Memory;

namespace Arts_be.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public VnPayService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            _memoryCache.Set($"Order_{model.UserID}", model, TimeSpan.FromMinutes(10));
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
           
            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Email} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", $"{urlCallBack}?UserID={model.UserID}");
            pay.AddRequestData("vnp_TxnRef", tick);
            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }
       

        public PaymentInformationModel GetPaymentModelFromCache(int userId)
        {
            return _memoryCache.Get<PaymentInformationModel>($"Order_{userId}");
        }
        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            return response;
        }
        
    }
}
