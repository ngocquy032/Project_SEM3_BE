using Arts_be.Models.DTO;

namespace Arts_be.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);

        //string CreatePaymentUrl(OrderDTO model, HttpContext context);
        //object CreatePaymentUrl(OrderDTO model, HttpContext httpContext);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
        PaymentInformationModel GetPaymentModelFromCache(int userId);

    }
}


    
