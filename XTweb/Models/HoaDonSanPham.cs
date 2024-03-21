using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class HoaDonSanPham
{
    public int MaHoaDon { get; set; }

    public int MaSanPham { get; set; }

    public DateTime NgayMua { get; set; }

    public double TongTien { get; set; }

    public int MaKhachHang { get; set; }

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
