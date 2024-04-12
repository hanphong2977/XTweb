namespace XTweb.Models
{
    public class ModelPay
    {
        public VnPaymentResponseModel VnPaymentResponseModel { get; set; }
       
        public ModelPay()
        {
           VnPaymentResponseModel = new VnPaymentResponseModel();
        
        }
    } 
}
