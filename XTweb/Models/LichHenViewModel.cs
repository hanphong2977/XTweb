using System.ComponentModel.DataAnnotations;

namespace XTweb.Models
{
    public class LichHenViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ và tên")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Ngày giờ hẹn")]
        public DateTime NgayHen { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Dịch Vụ")]
        public int SelectedDichVuId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Nhân viên")]
        public int SelectedNhanVienId { get; set; }
    }
}
