using System.ComponentModel.DataAnnotations;

namespace XTweb.Models
{
    public class LoginModel
    {
    
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!!!")]
        public string sdt {  get; set; }
        [Required(ErrorMessage ="Mật khẩu không được bỏ trống")]
        public string password {  get; set; }
    }
}
