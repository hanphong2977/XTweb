using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class CthdsanPham
{
    public int MaCthdsanPham { get; set; }

    public int MaSanPham { get; set; }

    public int MaHoaDonSanPham { get; set; }

    public int SoLuongMua { get; set; }

    public decimal Gia { get; set; }

    public virtual HoaDonSanPham MaHoaDonSanPhamNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
