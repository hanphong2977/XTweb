using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class LichHen
{
    public int MaLichHen { get; set; }

    public int MaKhachHang { get; set; }

    public DateTime NgayHen { get; set; }

    public virtual ICollection<HoaDonDichVu> HoaDonDichVus { get; set; } = new List<HoaDonDichVu>();

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
