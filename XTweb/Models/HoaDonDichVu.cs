using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class HoaDonDichVu
{
    public int MaHoaDon { get; set; }

    public int MaLichHen { get; set; }

    public DateTime NgayThanhToan { get; set; }

    public double TongTien { get; set; }

    public virtual LichHen MaLichHenNavigation { get; set; } = null!;
}
