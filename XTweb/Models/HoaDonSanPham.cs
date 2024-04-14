using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class HoaDonSanPham
{
    public int MaHoaDon { get; set; }

    public DateTime NgayMua { get; set; }

    public double TongTien { get; set; }

    public int MaKhachHang { get; set; }

    public string DiaChiGiaoHang { get; set; } = null!;

    public string LuuY { get; set; } = null!;

    public virtual ICollection<CthdsanPham> CthdsanPhams { get; set; } = new List<CthdsanPham>();

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
