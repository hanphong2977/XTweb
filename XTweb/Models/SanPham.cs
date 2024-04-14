using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class SanPham
{
    public int MaSanPham { get; set; }

    public string TenSanPham { get; set; } = null!;

    public string MoTaSanPham { get; set; } = null!;

    public double Gia { get; set; }

    public int SoLuong { get; set; }

    public int MaDanhMuc { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<CthdsanPham> CthdsanPhams { get; set; } = new List<CthdsanPham>();

    public virtual DanhMucSanPham MaDanhMucNavigation { get; set; } = null!;
}
