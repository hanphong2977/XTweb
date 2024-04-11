using System.ComponentModel.DataAnnotations;

namespace XTweb.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Họ Tên")]
        [Required(ErrorMessage ="Họ tên không được bỏ trống!!!")]

        public string hoten { get; set; }

        [Display(Name ="Số điện thoại")]
        [Required(ErrorMessage ="Số điện thoại không được bỏ trống!!!")]
        
        public string sdt { get; set; }


        [Display(Name = "Mật khẩu")]
        [StringLength(maximumLength:20 ,MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!!!")]
        public string password {  get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("password",ErrorMessage ="Xác nhận mật khẩu không đúng")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được trống!!!")]
        public string confirmpassword {  get; set; }

    }
}
