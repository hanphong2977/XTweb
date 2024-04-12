using XTweb.Models;

namespace XTweb.Services
{
    public interface IVnPayService
    {
        string CreatePaymentURL(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
            
    }
}
