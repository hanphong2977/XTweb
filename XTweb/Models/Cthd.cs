using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class Cthd
{
    public int MaLichHen { get; set; }

    public long MaHoaDonDv { get; set; }

    public string TenKh { get; set; } = null!;

    public DateTime NgayDat { get; set; }

    public decimal TienThanhToan { get; set; }

    public string Pttt { get; set; } = null!;

    public string MaGd { get; set; } = null!;

    public string Sđt { get; set; } = null!;

    public string TinhTrangTt { get; set; } = null!;

    public virtual HoaDonDichVu MaHoaDonDvNavigation { get; set; } = null!;

    public virtual LichHen MaLichHenNavigation { get; set; } = null!;
}
