using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class HoaDonDichVu
{
    public long MaHoaDon { get; set; }

    public double TongTien { get; set; }

    public virtual ICollection<Cthd> Cthds { get; set; } = new List<Cthd>();
}