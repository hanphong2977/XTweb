namespace XTweb.Models
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string Tel { get; set; }
        public double Amount { get; set; }
        public string OrderInfo { get; set; }
        public string PaymentMethod { get; set; }
        public long OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }

        public int malichhen { get; set; }
    }
}
