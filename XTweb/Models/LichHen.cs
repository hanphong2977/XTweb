using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class LichHen
{
    public int MaLichHen { get; set; }

    public int MaKhachHang { get; set; }

    public int MaNhanVien { get; set; }

    public DateTime NgayHen { get; set; }

    public int MaDichVu { get; set; }

    public virtual ICollection<HoaDonDichVu> HoaDonDichVus { get; set; } = new List<HoaDonDichVu>();

    public virtual DichVu MaDichVuNavigation { get; set; } = null!;

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;

    public virtual NhanVien MaNhanVienNavigation { get; set; } = null!;
}
