namespace XTweb.Models
{
    public class VnPaymentRequestModel
    {
        public string customername {  get; set; }

        public string customerphone { get; set; }

        public DateTime DateCreate { get; set; }

        public double Amount { get; set; }

        public int MalichHen { get; set; }
    }
}
