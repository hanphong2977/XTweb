using Microsoft.EntityFrameworkCore.Metadata.Internal;
using XTweb.Library;
using XTweb.Models;

namespace XTweb.Services
{
    public class VnPayService : IVnPayService
    {
        XuanTamDbContext _context = new XuanTamDbContext();
        private readonly IConfiguration _config;
        public VnPayService(IConfiguration config) {
            _config = config;
            
        }
        public string CreatePaymentURL(HttpContext context, VnPaymentRequestModel model)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_config["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString(); // 
            var vnpay = new VnPayLibrary(); // sử dụng thư viện VNPay

            // Tao URL thanh toan
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]); // Bắt Buộc
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]); // Bắt Buộc
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]); // Bắt buộc
            vnpay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());//Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss")); // Bắt buộc
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]); // Bắt buộc
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context)); // Lấy IP người dùng để thực hiện thanh toán
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]); // Bắt buộc
            vnpay.AddRequestData("vnp_OrderInfo", $"{model.customername}_{model.MalichHen}_{model.customerphone}"); // Thông tin hóa đơn người dùng cung cấp VNPay(Bắt buộc)
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other ( Kiểu thanh toán (Bắt buộc)
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnURL"]); //Đường dẫn trả về ( Bắt buộc)
            vnpay.AddRequestData("vnp_TxnRef",tick ); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            //Add Params of 2.1.0 Version
  

            var paymentURL = vnpay.CreateRequestUrl(_config["VnPay:BaseURL"], _config["VnPay:HashSecret"]);
            return paymentURL;
        }
        

        //Thuc hien payment
        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections) //Giai quyet du lieu gui di
            {
                if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_")) // Neu collections khac empty
                {
                    vnpay.AddResponseData(key, value.ToString()); // Do gia tri 
                }
            }

            // var cho phép biên dịch tự động xác định kiểu dữ liệu của biến dựa trên giá trị gán cho nó
            var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            long vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value; // tìm SecureHash từ chương trình của mình 
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]); // Check ket qua
            if(!checkSignature) 
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }
            int malichhen;
            string tenKH;
            var orderInfoParts = vnp_OrderInfo.ToString().Split('_');
            string customerName = orderInfoParts[0];
            string customerPhone = orderInfoParts[2];
            if (orderInfoParts.Length >= 2 && int.TryParse(orderInfoParts[1], out malichhen))
            {
                return new VnPaymentResponseModel
                {
                    Amount = vnp_Amount,
                    OrderInfo = customerName,
                    Success = true,
                    PaymentMethod = "VnPay",
                    OrderId = vnp_orderId,
                    TransactionId = vnp_TransactionId.ToString(),
                    Token = vnp_SecureHash,
                    VnPayResponseCode = vnp_ResponseCode.ToString(),
                    Tel = customerPhone,
                    malichhen = malichhen
                };
            }
            else
            {
                
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }
            /*VnPaymentResponseModel Response = new VnPaymentResponseModel()
            {
                Amount = vnp_Amount,
                OrderInfo = vnp_OrderInfo.ToString(),
                Success = true,
                PaymentMethod = "VnPay",
                OrderId = vnp_orderId,
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode.ToString(), // quan trong ! Ma code giao dich co thanh cong hay ko
                Tel = vnp_customerphone,
                malichhen = malichhen,
            };
            return Response;*/
        }
    }
}
